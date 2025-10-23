using LegislationMigration.Data;
using LegislationMigration.Models.NewEntities;
using LegislationMigration.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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
            _extractService = extractService;
            _jobStatusService = statusService;
            _logger = logger;
            _Factory = newDb;
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

                await ProcessPdfBatchAsync(batch, Db);

                _logger.LogInformation("Finished processing batch {BatchNum}.", (i / batchSize) + 1);
            }

            _logger.LogInformation("All folder batches processed successfully.");
        }

        public async Task ProcessPdfBatchAsync(List<string> pdfBatch, NewDbContext Db)
        {
            string jsonOutputDir = "E:\\Prem\\Reprocessed json files";

            // Ensure output folder exists
            Directory.CreateDirectory(jsonOutputDir);

            try
            {
                var batchJobs = new List<(string PdfPath, string PdfName, string JobId)>();
                // 1️⃣ Submit extract jobs
                foreach (var pdfPath in pdfBatch)
                {
                    string pdfName = Path.GetFileName(pdfPath);
                    try
                    {
                        // Check if already successfull
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
                        }
                        batchJobs.Add((pdfPath, pdfName, existing.JobId!));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error submitting job for {Pdf}", pdfPath);
                    }
                }
                await Db.SaveChangesAsync();
                await CheckProcessingLegislationsAsync(Db);

                //await Task.Delay(TimeSpan.FromMinutes(2));

                bool anyPending = true;
                while (anyPending)
                {
                    anyPending = false;

                    foreach (var job in batchJobs)
                    {
                        try
                        {
                            var existing = await Db.Legislations.FirstOrDefaultAsync(x => x.SourceFileName == job.PdfName);

                            if (existing == null || string.IsNullOrEmpty(existing.JobId)) continue;

                            if (existing.JobStatus == "completed") continue;

                                var response = await _jobStatusService.GetJobStatusAsync(existing.JobId);

                                existing.JobStatus = response.Status;

                                Db.Legislations.Update(existing);

                            if (response.Status == "completed" && response.Result != null)
                            {

                                string jsonFileName = $"{Path.GetFileNameWithoutExtension(job.PdfName)}.json";
                                string jsonFilePath = Path.Combine(jsonOutputDir, jsonFileName);
                                string jsonContent = JsonConvert.SerializeObject(response, Formatting.Indented);

                                await File.WriteAllTextAsync(jsonFilePath, jsonContent);
                                _logger.LogInformation("Saved JSON response to {FilePath}", jsonFilePath);

                                var existingArticles = await Db.Articles.Where(a => a.LegislationId == existing.LegislationId).ToListAsync();

                                if (existingArticles.Any())
                                {
                                    Db.Articles.RemoveRange(existingArticles);
                                }

                                var newArticles = response.Result.Articles?.Select(a => new Article
                                {
                                    LegislationId = existing.LegislationId,
                                    ArticleNumberText = a.ArticleNumber.ToString(),
                                    ArticleTitle = a.Heading,
                                    OldArticleBody = a.Text,
                                    DisplayName = GetCleanDisplayName(a.Heading),
                                    Active = true,
                                    Aisummary = null,
                                    CreatedBy = "System",
                                    CreatedAt = DateTime.Now,
                                    HasRelatedArticles = false
                                }).ToList();

                                if (newArticles?.Any() == true)
                                {
                                    await Db.Articles.AddRangeAsync(newArticles);
                                    _logger.LogInformation("Inserted {Count} articles for {Pdf}", newArticles.Count, job.PdfName);
                                }

                                foreach(var art in response.Result.Articles)
                                {
                                    var savedArticle = await Db.Articles.FirstOrDefaultAsync(a => a.ArticleNumberText == art.ArticleNumber.ToString());
                                    if(savedArticle == null) continue;

                                    var isEnglish = existing.LanguageId == 1;
                                    var domains = isEnglish ? art.DomainsEN : art.DomainsAR;
                                    var rationales = isEnglish ? art.RationalesEN : art.RationalesAR;

                                    if (domains != null && rationales != null && domains.Count == rationales.Count)
                                    {
                                        for (int i = 0; i < domains.Count; i++)
                                        {
                                            var domainName = domains[i];
                                            var rationale = rationales[i];
                                            var domainType = await Db.DomainTypes
                                                .FirstOrDefaultAsync(d => d.DomainName == domainName &&
                                                                          d.LanguageId == existing.LanguageId);

                                            if (domainType == null)
                                            {
                                                domainType = new DomainType
                                                {
                                                    DomainName = domainName,
                                                    LanguageId = existing.LanguageId
                                                };

                                                Db.DomainTypes.Add(domainType);
                                            }

                                            var domain = new Domain
                                            {
                                                DomainName = domainName,
                                                Rationale = rationale,
                                                CreatedBy = "System",
                                                CreatedAt = DateTime.Now,
                                                DomainTypeId = domainType.Id,
                                                ArticleId = savedArticle.ArticleId
                                            };

                                            Db.Domains.Add(domain);
                                        }
                                    }
                                }
                                await Db.SaveChangesAsync();
                            }
                            if (existing.JobStatus != "completed")
                                    anyPending = true;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error checking job status for {Pdf}", job.PdfName);
                            anyPending = true; // retry on failure
                        }
                    }

                    if (anyPending)
                    {
                        _logger.LogInformation("Some jobs are still pending/processing. Waiting 2 minutes before next check...");
                        //await CheckProcessingLegislationsAsync(Db);
                        await Task.Delay(TimeSpan.FromMinutes(2));
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

        public static string GetCleanDisplayName(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) return string.Empty;
            string cleaned = Regex.Replace(title, @"[^\w\s]", "").Trim();
            var words = cleaned.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string displayName = "";
            foreach (var word in words)
            {
                if ((displayName + " " + word).Trim().Length <= 10)
                    displayName = (displayName + " " + word).Trim();
                else break;
            }
            return displayName;
        }
        public async Task CheckProcessingLegislationsAsync(NewDbContext db)
        {
          //  using var db = await _Factory.CreateDbContextAsync();

            var processingLegislations = await db.Legislations
                .Where(l => l.JobStatus == "processing" && l.JobId != null)
                .Select(l => new { l.LegislationId, l.JobId, l.SourceFileName })
                .ToListAsync();

            if (!processingLegislations.Any())
            {
                _logger.LogInformation("No processing legislations found.");
                return ;
            }

            _logger.LogInformation("Found {Count} processing legislations.", processingLegislations.Count);

            //await Task.Delay(TimeSpan.FromMinutes(1));
            var ids = processingLegislations.Select(x => x.LegislationId).ToList();
            await UpdateProcessingJobStatusesAsync(ids, db);
        }
        public async Task UpdateProcessingJobStatusesAsync(List<long> legislationIds, NewDbContext db)
        {
            //using var db = await _Factory.CreateDbContextAsync();

            var legislations = await db.Legislations
                .Where(l => legislationIds.Contains(l.LegislationId))
                .ToListAsync();

            if (!legislations.Any())
            {
                _logger.LogInformation("No legislations found for provided IDs.");
                return;
            }

            _logger.LogInformation("Updating status for {Count} processing jobs in parallel...", legislations.Count);
            await Task.Delay(TimeSpan.FromMinutes(2));

            var tasks = legislations.Select(async legislation =>
            {
                try
                {
                    if (string.IsNullOrEmpty(legislation.JobId))
                        return;

                    var response = await _jobStatusService.GetJobStatusAsync(legislation.JobId);

                    bool notCompleted = true;
                    while (notCompleted)
                    {
                        notCompleted = false;
                        if (response.Status != "completed")
                        {
                            await Task.Delay(TimeSpan.FromMinutes(1));
                            notCompleted = true;
                        }
                    }

                    legislation.JobStatus = response.Status;
                    db.Legislations.Update(legislation);

                    await db.SaveChangesAsync();

                    _logger.LogInformation("Updated {Id} ({Name}) to status {Status}",
                        legislation.LegislationId, legislation.SourceFileName, response.Status);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating status for LegislationId {Id}", legislation.LegislationId);
                }
            });

            await Task.WhenAll(tasks);

            _logger.LogInformation("Parallel job status updates completed.");
        }

    }
}