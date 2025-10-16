using LegislationMigration.Data;
using LegislationMigration.Models.DTOs;
using LegislationMigration.Models.Entities;
using LegislationMigration.Models.NewEntities;
using LegislationMigration.Repositories.Interfaces;
using LegislationMigration.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace LegislationMigration.Services.Implementations
{
    public class LegislationReprocessService : IReprocessService
    {
        private readonly IExtractService _extractService;
        private readonly IJobStatusService _jobStatusService;
        private readonly ILogger<LegislationReprocessService> _logger;

        private readonly IDbContextFactory<MyDbContext> _oldFactory;
        private readonly IDbContextFactory<NewDbContext> _newFactory;

        public LegislationReprocessService(
        IExtractService extractService,
        IJobStatusService statusService,
        ILogger<LegislationReprocessService> logger,
        IDbContextFactory<MyDbContext> oldDb,
        IDbContextFactory<NewDbContext> newDb)
        {
            _extractService = extractService ?? throw new ArgumentNullException(nameof(extractService));
            _jobStatusService = statusService ?? throw new ArgumentNullException(nameof(statusService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
           
            _oldFactory = oldDb ?? throw new ArgumentNullException(nameof(oldDb));
            _newFactory = newDb ?? throw new ArgumentNullException(nameof(newDb));
        }

        public async Task ReprocessLegislationAsync(string pdfFolderPath)
        {
            var allFolders = Directory.EnumerateDirectories(pdfFolderPath, "*", SearchOption.TopDirectoryOnly).ToList();
            if (!allFolders.Any())
            {
                _logger.LogWarning("No folders found in {Path}", pdfFolderPath);
                return;
            }

            _logger.LogInformation("Total folders to process: {Count}", allFolders.Count);

            using var oldDb = await _oldFactory.CreateDbContextAsync();

            const int batchSize = 10;

            // Enumerate all top-level directories, but process in chunks of 10
            //var allFolders = Directory.EnumerateDirectories(pdfFolderPath, "*", SearchOption.TopDirectoryOnly).ToList();

            if (!allFolders.Any())
            {
                _logger.LogWarning("No folders found in {Path}", pdfFolderPath);
                return;
            }

            _logger.LogInformation("Total folders to process: {Count}", allFolders.Count);

            // ✅ Process 10 folders at a time
            for (int i = 0; i < allFolders.Count; i += batchSize)
            {
                var folderBatch = allFolders.Skip(i).Take(batchSize).ToList();
                _logger.LogInformation("Processing folder batch {BatchNum} ({Count} folders)", (i / batchSize) + 1, folderBatch.Count);

                var pdfBatch = new List<(string PdfPath, string PdfName)>();

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

                        var pdfName = Path.GetFileName(selectedPdf);

                        pdfBatch.Add((selectedPdf, pdfName));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error reading folder {Folder}", folder);
                    }
                }
                var pdfNames = pdfBatch.Select(p => p.PdfName).ToList();
                var matchedOldRecords = await oldDb.Legislations
                    .Where(l => pdfNames.Contains(l.SourceFileName))
                    .ToListAsync();

                var filteredPdfBatch = pdfBatch
                    .Where(p => matchedOldRecords.Any(r => r.SourceFileName == p.PdfName))
                    .Select(p => p.PdfPath)
                    .ToList();

                if (!filteredPdfBatch.Any())
                {
                    _logger.LogInformation("No matching old records found for this batch.");
                    continue;
                }
                // 🔹 STEP 2: Process the selected PDFs in this batch
                await ProcessPdfBatchAsync(filteredPdfBatch);

                _logger.LogInformation("✅ Finished processing batch {BatchNum}.", (i / batchSize) + 1);
            }

            _logger.LogInformation("🎉 All folder batches processed successfully.");
        }

        public async Task ProcessPdfBatchAsync(List<string> pdfBatch)
        {
            try
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
                            .FirstOrDefaultAsync();

                        if (oldRecord == null)
                            continue;

                        // Check if already successful
                        var existing = await newDb.Legislations.FirstOrDefaultAsync(x => x.SourceFileName == pdfName);

                        if (existing != null && existing.JobStatus == "completed")
                        {
                            _logger.LogInformation("Skipping {Pdf}, already completed.", pdfName);
                            continue;
                        }

                        ExtractJobResponse? jobResponse = null;
                        string language = "en";

                        if (oldRecord.LanguageId == 1)
                            language = "en";
                        else if (oldRecord.LanguageId == 2)
                            language = "ar";

                        if (existing == null)
                        {
                            jobResponse = await _extractService.SubmitExtractJobAsync(pdfPath, language);
                            if (jobResponse == null) continue;

                            existing = new Models.NewEntities.Legislation
                            {
                                LegislationId = oldRecord.LegislationId,
                                Title = oldRecord.Title,
                                StatusId = oldRecord.StatusId,
                                DateOfIssuance = oldRecord.DateOfIssuance,
                                HijriDate = oldRecord.HijriDate,
                                IssuingAuthorityId = oldRecord.IssuingAuthorityId,
                                LegislationTypeId = oldRecord.LegislationTypeId,
                                OfficialGazetteNumber = oldRecord.OfficialGazetteNumber,
                                SourceFileName = pdfName,
                                PdfUrl = oldRecord.PdfUrl,
                                CreatedBy = "system",
                                CreatedAt = oldRecord.CreatedAt,
                                SourceId = oldRecord.SourceId,
                                LanguageId = oldRecord.LanguageId,
                                Aisummary = oldRecord.Aisummary,
                                DisplayName = oldRecord.DisplayName,
                                Json = oldRecord.Json,
                                CategoryId = oldRecord.CategoryId,
                                SubCategoryId = oldRecord.SubCategoryId,
                                Embeddings = oldRecord.Embeddings,
                                Active = true,
                                Number = oldRecord.Number,
                                Version = oldRecord.Version,
                                ParentLegislationId = oldRecord.ParentLegislationId,
                                JobId = jobResponse.JobId,
                                JobStatus = jobResponse.Status
                            };
                            await using var transaction = await newDb.Database.BeginTransactionAsync();

                            await newDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Legislations ON");

                            await newDb.Legislations.AddAsync(existing);
                            await newDb.SaveChangesAsync();

                            await newDb.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Legislations OFF");

                            await transaction.CommitAsync();
                        }
                       
                        while (true)
                        {
                            var jobId = existing.JobId ?? jobResponse?.JobId;
                            if (string.IsNullOrEmpty(jobId)) break;

                            var status = await _jobStatusService.GetJobStatusAsync(jobId);
                            if (status == "completed")
                            {
                                existing.JobStatus = status;
                                newDb.Legislations.Update(existing);
                                await newDb.SaveChangesAsync();
                                _logger.LogInformation("Job completed for {Pdf} (JobId {JobId})", pdfName, jobId);
                                break;
                            }

                            _logger.LogInformation("Job pending for {Pdf} (JobId {JobId}). Retrying in 2 minutes...", pdfName, jobId);
                            await Task.Delay(TimeSpan.FromMinutes(2));
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error submitting job for {Pdf}", pdfPath);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}