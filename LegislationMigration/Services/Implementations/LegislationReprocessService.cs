using LegislationMigration.Data;
using LegislationMigration.Models.DTOs;
using LegislationMigration.Models.Entities;
using LegislationMigration.Models.NewEntities;
using LegislationMigration.Repositories.Interfaces;
using LegislationMigration.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LegislationMigration.Services.Implementations
{
    public class LegislationReprocessService : IReprocessService
    {
        private readonly ILegislationRepository _repo;
        private readonly IExtractService _extractService;
        private readonly IJobStatusService _statusService;
        private readonly ILogger<LegislationReprocessService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IDbContextFactory<MyDbContext> _oldFactory;
        private readonly IDbContextFactory<NewDbContext> _newFactory;

        public LegislationReprocessService(
        ILegislationRepository repo,
        IExtractService extractService,
        IJobStatusService statusService,
        ILogger<LegislationReprocessService> logger,
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration,
        IDbContextFactory<MyDbContext> oldDb,
        IDbContextFactory<NewDbContext> newDb)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _extractService = extractService ?? throw new ArgumentNullException(nameof(extractService));
            _statusService = statusService ?? throw new ArgumentNullException(nameof(statusService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _oldFactory = oldDb ?? throw new ArgumentNullException(nameof(oldDb));
            _newFactory = newDb ?? throw new ArgumentNullException(nameof(newDb));
        }

        public async Task ReprocessLegislationAsync(string pdfFolderPath, string language)
        {
            const int batchSize = 10;

            // Enumerate all top-level directories, but process in chunks of 10
            var allFolders = Directory.EnumerateDirectories(pdfFolderPath, "*", SearchOption.TopDirectoryOnly).ToList();

            if (!allFolders.Any())
            {
                _logger.LogWarning("No folders found in {Path}", pdfFolderPath);
                return;
            }

            _logger.LogInformation("Total folders to process: {Count}", allFolders.Count);

            using var client = _httpClientFactory.CreateClient();
            var apiUrl = _configuration["AIService:BaseApiUrl"];

            // ✅ Process 10 folders at a time
            for (int i = 0; i < allFolders.Count; i += batchSize)
            {
                var folderBatch = allFolders.Skip(i).Take(batchSize).ToList();
                _logger.LogInformation("Processing folder batch {BatchNum} ({Count} folders)", (i / batchSize) + 1, folderBatch.Count);

                var pdfBatch = new List<string>();

                // 🔹 STEP 1: Collect 1 PDF from each folder in this batch
                foreach (var folder in folderBatch)
                {
                    try
                    {
                        var folderName = Path.GetFileName(folder) ?? string.Empty;
                        var pdfFiles = Directory.GetFiles(folder, "*.pdf", SearchOption.TopDirectoryOnly);
                        if (pdfFiles.Length == 0)
                            continue;

                        string selectedPdf = pdfFiles.Length == 1
                            ? pdfFiles[0]
                            : pdfFiles
                                .Select(p => new { Path = p, NameNoExt = Path.GetFileNameWithoutExtension(p) })
                                .FirstOrDefault(x => string.Equals(
                                    x.NameNoExt?.Trim(),
                                    folderName.Trim(),
                                    StringComparison.OrdinalIgnoreCase))?.Path
                                ?? pdfFiles.OrderBy(p => p).First();

                        pdfBatch.Add(selectedPdf);
                        _logger.LogInformation("Selected PDF: {Pdf}", selectedPdf);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error reading folder {Folder}", folder);
                    }
                }

                if (pdfBatch.Count == 0)
                {
                    _logger.LogWarning("No PDFs found in this batch.");
                    continue;
                }

                // 🔹 STEP 2: Process the selected PDFs in this batch
                await ProcessPdfBatchAsync(pdfBatch, client, apiUrl, language);

                _logger.LogInformation("✅ Finished processing batch {BatchNum}.", (i / batchSize) + 1);
            }

            _logger.LogInformation("🎉 All folder batches processed successfully.");
        }

        public async Task ProcessPdfBatchAsync(List<string> pdfBatch, HttpClient client, string apiUrl, string language)
        {
            using var oldDb = await _oldFactory.CreateDbContextAsync();
            using var newDb = await _newFactory.CreateDbContextAsync();

            var jobMap = new Dictionary<string, string>(); // pdf → jobId

            // 1️⃣ Submit extract jobs
            foreach (var pdfPath in pdfBatch)
            {
                try
                {
                    string pdfName = Path.GetFileName(pdfPath);

                    // Check in old DB
                    var oldRecord = await oldDb.Legislations
                        .Where(l => l.SourceFileName == pdfName)
                        .Select(l => new { l.LegislationId, l.SourceFileName, l.LanguageId })
                        .FirstOrDefaultAsync();

                    if (oldRecord == null)
                        continue;

                    // Check if already successful
                    Models.NewEntities.Legislation existing = await newDb.Legislations.FirstOrDefaultAsync(x => x.SourceFileName == pdfName);

                    if (existing != null && existing.JobStatus == "Success")
                        continue;

                    using var content = new MultipartFormDataContent();
                    using var fileStream = File.OpenRead(pdfPath);
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                    content.Add(fileContent, "pdf", Path.GetFileName(pdfPath));

                    var response = await client.PostAsync($"{apiUrl}extract?language={oldRecord.LanguageId}", content);
                    var result = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                        continue;

                    var jobResponse = JsonConvert.DeserializeObject<ExtractJobResponse>(result);
                    if (jobResponse == null)
                        continue;

                    if (existing == null)
                    {
                        existing = new Models.NewEntities.Legislation
                        {
                            LegislationId = oldRecord.LegislationId,
                            SourceFileName = pdfName,
                            JobId = jobResponse.JobId,
                            JobStatus = jobResponse.Status,
                            CreatedBy = "system",
                            CreatedAt = DateTime.UtcNow
                        };
                        await newDb.Legislations.AddAsync(existing);
                    }
                    else
                    {
                        existing.JobId = jobResponse.JobId;
                        existing.JobStatus = jobResponse.Status;
                        newDb.Legislations.Update(existing);
                    }

                    await newDb.SaveChangesAsync();
                    jobMap[pdfPath] = jobResponse.JobId;


                    _logger.LogInformation("Submitted Job {JobId} for {Pdf}", jobResponse.JobId, pdfName);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error submitting job for {Pdf}", pdfPath);
                }
            }

            // Wait 2 minutes then check status
            _logger.LogInformation("⏳ Waiting 2 minutes before checking statuses...");
            await Task.Delay(TimeSpan.FromMinutes(2));

            foreach (var (pdfPath, jobId) in jobMap)
            {
                try
                {
                    var statusResponse = await client.GetAsync($"{apiUrl}status/{jobId}");
                    var statusText = await statusResponse.Content.ReadAsStringAsync();
                    var statusObj = JsonConvert.DeserializeObject<JobStatusResponse>(statusText);

                    var pdfName = Path.GetFileName(pdfPath);
                    var record = await newDb.Legislations.FirstOrDefaultAsync(x => x.SourceFileName == pdfName);
                    Models.Entities.Legislation existing = await oldDb.Legislations.FirstOrDefaultAsync(x => x.SourceFileName == pdfName);

                    if (record != null && statusObj.Status == "completed")
                    {
                        record.JobStatus = statusObj.Status;
                        var newRecord = new Models.NewEntities.Legislation
                        {
                            LegislationId = existing.LegislationId,
                            Title = statusObj.Result.Title,
                            StatusId = existing.StatusId,
                            DateOfIssuance = existing.DateOfIssuance,
                            HijriDate = existing.HijriDate,
                            IssuingAuthorityId = existing.IssuingAuthorityId,
                            LegislationTypeId = existing.LegislationTypeId,
                            OfficialGazetteNumber = existing.OfficialGazetteNumber,
                            SourceFileName = statusObj.Result.PdfFileName,
                            PdfUrl = existing.PdfUrl,
                            CreatedBy = "system",
                            CreatedAt = statusObj.CreatedAt,
                            SourceId = existing.SourceId,
                            LanguageId = existing.LanguageId,
                            Aisummary = existing.Aisummary,
                            DisplayName = existing.DisplayName,
                            Json = statusText,
                            CategoryId = existing.CategoryId,
                            SubCategoryId = existing.SubCategoryId,
                            Embeddings = existing.Embeddings,
                            Active = true,
                            Number = existing.Number,
                            Version = existing.Version,
                            ParentLegislationId = existing.ParentLegislationId,
                            JobId = jobId,
                            JobStatus = statusObj.Status,
                        };
                        newDb.Legislations.Update(newRecord);
                        await newDb.SaveChangesAsync();
                    }
                    else if (statusObj.Status != "completed")
                    {
                        await Task.Delay(TimeSpan.FromMinutes(2));
                    }

                        _logger.LogInformation("Checked status for {Pdf} (Job {JobId}): {Status}", pdfName, jobId, statusObj?.Status);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error checking status for job {JobId}", jobId);
                }
            }
        }
        //public async Task ReprocessLegislationAsync2(string pdfFolderPath, string language)
        //{
        //    const int batchSize = 10;
        //    var allPdfPaths = new List<string>();

        //    foreach (var folder in Directory.EnumerateDirectories(pdfFolderPath, "*", SearchOption.TopDirectoryOnly))
        //    {
        //        var folderName = Path.GetFileName(folder) ?? string.Empty;
        //        try
        //        {
        //            var pdfFiles = Directory.GetFiles(folder, "*.pdf", SearchOption.TopDirectoryOnly);
        //            if (pdfFiles.Length == 0)
        //            {
        //                _logger.LogWarning("⚠️ No PDFs found in folder: {Folder}", folder);
        //                continue;
        //            }

        //            string selectedPdf = pdfFiles.Length == 1
        //                ? pdfFiles[0]
        //                : pdfFiles
        //                    .Select(p => new { Path = p, NameNoExt = Path.GetFileNameWithoutExtension(p) })
        //                    .FirstOrDefault(x => string.Equals(
        //                        x.NameNoExt?.Trim(),
        //                        folderName.Trim(),
        //                        StringComparison.OrdinalIgnoreCase))?.Path
        //                    ?? pdfFiles.OrderBy(p => p).First();

        //            allPdfPaths.Add(selectedPdf);
        //            _logger.LogInformation("Selected PDF: {Pdf} (folder: {Folder})", selectedPdf, folder);
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, " Error scanning folder: {Folder}", folder);
        //        }
        //    }

        //    if (!allPdfPaths.Any())
        //    {
        //        _logger.LogWarning("No PDF files found to process.");
        //        return;
        //    }

        //    _logger.LogInformation("Total PDFs to process: {Count}", allPdfPaths.Count);

        //    using var client = _httpClientFactory.CreateClient();
        //    var apiUrl = _configuration["AIService:BaseApiUrl"]; // e.g., "https://localhost:5001/api/ai/"

        //    for (int i = 0; i < allPdfPaths.Count; i += batchSize)
        //    {
        //        var batch = allPdfPaths.Skip(i).Take(batchSize).ToList();
        //        var jobMap = new Dictionary<string, string>(); // pdfPath → jobId

        //        // 1️⃣ Submit extract jobs
        //        foreach (var pdfPath in batch)
        //        {
        //            try
        //            {
        //                using var content = new MultipartFormDataContent();
        //                using var fileStream = File.OpenRead(pdfPath);
        //                var fileContent = new StreamContent(fileStream);
        //                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
        //                content.Add(fileContent, "pdf", Path.GetFileName(pdfPath));

        //                var response = await client.PostAsync($"{apiUrl}extract?language={language}", content);
        //                var result = await response.Content.ReadAsStringAsync();

        //                if (!response.IsSuccessStatusCode)
        //                {
        //                    _logger.LogError("❌ API failed for {Pdf} with status {Status} and message: {Result}", pdfPath, response.StatusCode, result);
        //                    continue;
        //                }

        //                var jobResponse = JsonConvert.DeserializeObject<ExtractJobResponse>(result);
        //                jobMap[pdfPath] = jobResponse?.JobId ?? string.Empty;
        //                _logger.LogInformation("✅ Submitted job {JobId} for {Pdf}", jobResponse?.JobId, Path.GetFileName(pdfPath));
        //            }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError(ex, "❌ Error submitting extract job for {Pdf}", pdfPath);
        //            }
        //        }

        //        // 2️⃣ Wait 2 minutes before checking
        //        _logger.LogInformation("⏳ Waiting 2 minutes before checking job statuses...");
        //        await Task.Delay(TimeSpan.FromMinutes(2));

        //        // 3️⃣ Check statuses
        //        foreach (var (pdfPath, jobId) in jobMap)
        //        {
        //            try
        //            {
        //                if (string.IsNullOrEmpty(jobId))
        //                    continue;

        //                var statusResponse = await client.GetAsync($"{apiUrl}status/{jobId}");
        //                var statusText = await statusResponse.Content.ReadAsStringAsync();
        //                var status = JsonConvert.DeserializeObject<JobStatusResponse>(statusText)?.Status;

        //                if (status?.Equals("completed", StringComparison.OrdinalIgnoreCase) == true)
        //                    _logger.LogInformation("✅ {Pdf} processed successfully (Job {JobId})", Path.GetFileName(pdfPath), jobId);
        //                else
        //                    _logger.LogWarning("⚠️ {Pdf} not yet complete (Status: {Status})", Path.GetFileName(pdfPath), status);
        //            }
        //            catch (Exception ex)
        //            {
        //                _logger.LogError(ex, "❌ Error checking job status for {Pdf}", pdfPath);
        //            }
        //        }
        //    }

        //    _logger.LogInformation("🎉 Reprocessing completed for all PDFs.");
        //}
    }
}