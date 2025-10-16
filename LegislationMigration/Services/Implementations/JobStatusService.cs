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

        public async Task<string> GetJobStatusAsync(string jobId)
        {
            try
            {
                using var client = _factory.CreateClient();
                var baseUrl = _config["AIService:ExtractionUrl"];
                var response = await client.GetAsync($"{baseUrl}status/{jobId}");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<JobStatusResponse>(json);

                return status?.Status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching status for JobId {JobId}", jobId);
                return null;
            }
        }
    }
}
