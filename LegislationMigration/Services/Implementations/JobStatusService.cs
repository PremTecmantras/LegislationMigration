using LegislationMigration.Models.DTOs;
using LegislationMigration.Services.Interfaces;
using Microsoft.Extensions.Configuration;
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
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        public JobStatusService(IHttpClientFactory factory, IConfiguration config)
        {
            _client = factory.CreateClient();
            _config = config;
        }
        public async Task<string> GetJobStatusAsync(string jobId)
        {
            var baseUrl = _config["AIService:ExtractionUrl"];
            var statusUrl = $"{baseUrl}status/{jobId}";

            var response = await _client.GetAsync(statusUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var status = JsonConvert.DeserializeObject<JobStatusResponse>(json);
            return status.Status;
        }
    }
}
