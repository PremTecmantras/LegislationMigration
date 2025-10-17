using LegislationMigration.Data;
using LegislationMigration.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LegislationMigration.Services.Implementations
{
    public class LegislationReprocessService : IReprocessService
    {
        private readonly IExtractService _extractService;
        private readonly IJobStatusService _jobStatusService;
        private readonly ILogger<LegislationReprocessService> _logger;

        private readonly IDbContextFactory<NewDbContext> _Factory;

        public LegislationReprocessService(
        IExtractService extractService,
        IJobStatusService statusService,
        ILogger<LegislationReprocessService> logger,
        IDbContextFactory<NewDbContext> newDb)
        {
            _extractService = extractService ?? throw new ArgumentNullException(nameof(extractService));
            _jobStatusService = statusService ?? throw new ArgumentNullException(nameof(statusService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
           
            _Factory = newDb ?? throw new ArgumentNullException(nameof(newDb));
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

            using var Db = await _Factory.CreateDbContextAsync();

            var allPdfFiles = new List<(string PdfPath, string PdfName)>();

            // ✅ Process 10 folders at a time
            foreach (var folder in allFolders)
            {
                try
                {
                    var folderName = Path.GetFileName(folder) ?? string.Empty;
                    var pdfFiles = Directory.GetFiles(folder, "*.pdf", SearchOption.TopDirectoryOnly);
                    if (!pdfFiles.Any()) continue;

                    string selectedPdf = pdfFiles.Length == 1
                        ? pdfFiles[0]
                        : pdfFiles
                            .Select(p => new { Path = p, NameNoExt = Path.GetFileNameWithoutExtension(p) })
                            .FirstOrDefault(x => string.Equals(x.NameNoExt?.Trim(), folderName.Trim(), StringComparison.OrdinalIgnoreCase))
                            ?.Path ?? pdfFiles.OrderBy(p => p).First();

                    var pdfName = Path.GetFileName(selectedPdf);
                    allPdfFiles.Add((selectedPdf, pdfName));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error reading folder {Folder}", folder);
                }
            }

            if (!allPdfFiles.Any())
            {
                _logger.LogWarning("No PDFs found in any folder.");
                return;
            }

            // Step 2: Filter PDFs that exist in the database and are not completed
            var pdfNames = allPdfFiles.Select(p => p.PdfName).ToList();
            var matchedRecords = await Db.Legislations
                .Where(l => pdfNames.Contains(l.SourceFileName) && (l.JobStatus != "completed"))
                .ToListAsync();

            var filteredPdfFiles = allPdfFiles
                .Where(p => matchedRecords.Any(r => r.SourceFileName == p.PdfName))
                .Select(p => p.PdfPath)
                .ToList();

            if (!filteredPdfFiles.Any())
            {
                _logger.LogInformation("No matching records found in DB for any PDFs.");
                return;
            }

            // Step 3: Process in batches of 10
            const int batchSize = 10;
            for (int i = 0; i < filteredPdfFiles.Count; i += batchSize)
            {
                var batch = filteredPdfFiles.Skip(i).Take(batchSize).ToList();
                _logger.LogInformation("Processing batch {BatchNum} with {Count} PDFs.", (i / batchSize) + 1, batch.Count);

                await ProcessPdfBatchAsync(batch);

                _logger.LogInformation("Finished processing batch {BatchNum}.", (i / batchSize) + 1);
            }

            _logger.LogInformation("All folder batches processed successfully.");
        }

        public async Task ProcessPdfBatchAsync(List<string> pdfBatch)
        {
            try
            {

                //using var oldDb = await _oldFactory.CreateDbContextAsync();
                using var Db = await _Factory.CreateDbContextAsync();

                // 1️⃣ Submit extract jobs
                foreach (var pdfPath in pdfBatch)
                {
                    string pdfName = Path.GetFileName(pdfPath);
                    try
                    {
                        // Check if already successful
                        var existing = await Db.Legislations
                            .FirstOrDefaultAsync(x => x.SourceFileName == pdfName);

                        string language = existing.LanguageId switch
                        {
                            1 => "en",
                            2 => "ar",
                            _ => "en"
                        };

                        if (existing == null)
                        {
                            _logger.LogWarning("PDF {Pdf} not found in database, skipping.", pdfName);
                            continue;
                        }

                        if (string.IsNullOrEmpty(existing.JobStatus))
                        {
                            var response = await _extractService.SubmitExtractJobAsync(pdfPath, language);

                            existing.JobId = response.JobId;
                            existing.JobStatus = response.Status;

                            Db.Legislations.Update(existing);
                            await Db.SaveChangesAsync();
                        }
                        await Task.Delay(TimeSpan.FromMinutes(2));

                        while (existing.JobStatus == "pending" || existing.JobStatus == "processing")
                        {
                            var Response = await _jobStatusService.GetJobStatusAsync(existing.JobId ?? string.Empty);

                            //TODO: Mapping fields from Response to existing legislation record
                            existing.JobStatus = Response.Status;
                            existing.JobId = Response.JobId;

                            Db.Legislations.Update(existing);
                            await Db.SaveChangesAsync();

                            if (existing.JobStatus == "pending" || existing.JobStatus == "processing")
                            {
                                _logger.LogInformation("Job still pending/processing for {Pdf}, retrying in 2 minutes...", pdfName);
                                await Task.Delay(TimeSpan.FromMinutes(2));
                            }

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