using LegislationMigration.Models.DTOs;
using LegislationMigration.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LegislationMigration.Services.Implementations
{
    public class ExtractService : IExtractService
    {
        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;
        private readonly ILogger<LegislationReprocessService> _logger;
        public ExtractService(IHttpClientFactory factory, IConfiguration config, ILogger<LegislationReprocessService> logger)
        {
            _client = factory;
            _config = config;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<ExtractJobResponse> SubmitExtractJobAsync(string pdfPath, string language)
        {
            try
            {
                using var client = _client.CreateClient();
                var apiUrl = _config["AIService:BaseApiUrl"];

                using var content = new MultipartFormDataContent();
                using var fileStream = File.OpenRead(pdfPath);
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                content.Add(fileContent, "pdf", Path.GetFileName(pdfPath));
                
                

                var response = await client.PostAsync($"{apiUrl}extract?language={language}", content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Failed to submit extract job for {Pdf}. StatusCode: {StatusCode}", pdfPath, response.StatusCode);
                    return null;
                }

                var jobResponse = JsonConvert.DeserializeObject<ExtractJobResponse>(result);

                if (jobResponse == null)
                {
                    _logger.LogWarning("JobResponse was null for {Pdf}", pdfPath);
                    return null;
                }

                _logger.LogInformation("Submitted Job {JobId} for {Pdf}", jobResponse.JobId, pdfPath);
                return jobResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting job for {Pdf}", pdfPath);
                return null;
            }
        }
    }
}