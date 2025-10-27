using LegislationMigration.Models.DTOs;
using LegislationMigration.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislationMigration.Services.Implementations
{
    public class JobStatusService : IJobStatusService
    {
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _config;
        private readonly ILogger<JobStatusService> _logger;
        public JobStatusService(IHttpClientFactory factory, IConfiguration config, ILogger<JobStatusService> logger)
        {
            _factory = factory;
            _config = config;
            _logger = logger;
        }

        public async Task<JobStatusResponse> GetJobStatusAsync(string jobId)
        {
            try
            {
                string testJsonDir = @"E:\Prem\Reprocessed json files"; // same folder you save JSON to
                string jsonFilePath = Path.Combine(testJsonDir, "مرسوم رقم (47) لسنة 2023 بتشكيل مجلس إدارة مؤسسة تنظيم الصناعة الأمنية.json");

                    _logger.LogInformation("Loading JobStatusResponse from file: {FilePath}", jsonFilePath);

                    string json = await File.ReadAllTextAsync(jsonFilePath);
                    var response = JsonConvert.DeserializeObject<JobStatusResponse>(json);

                    if (response == null)
                        throw new InvalidDataException($"Failed to deserialize JSON file {jsonFilePath}");

                    return response;

                //using var client = _factory.CreateClient();
                //var baseUrl = _config["AIService:ExtractionUrl"];
                //var response = await client.GetAsync($"{baseUrl}status/{jobId}");
                //response.EnsureSuccessStatusCode();

                //var json = await response.Content.ReadAsStringAsync();
                //var status = JsonConvert.DeserializeObject<JobStatusResponse>(json);

                //return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching status for JobId {JobId}", jobId);
                return null;
            }
        }
    }
}
