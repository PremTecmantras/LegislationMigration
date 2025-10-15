using LegislationMigration.Models.DTOs;
using LegislationMigration.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LegislationMigration.Services.Implementations
{
    public class ExtractService : IExtractService
    {
        private readonly IHttpClientFactory _client;
        private readonly IConfiguration _config;
        public ExtractService(IHttpClientFactory factory, IConfiguration config)
        {
            _client = factory;
            _config = config;
        }
        public async Task<string> SubmitExtractJobAsync(string pdfPath, string language)
        {
            //var baseUrl = _config["AIService:ExtractionUrl"];
            //var extractUrl = $"{baseUrl}extract?language={language}";

            //using var content = new MultipartFormDataContent();
            //using var fileStream = File.OpenRead(pdfPath);
            //var fileContent = new StreamContent(fileStream);
            //fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            //content.Add(fileContent, "pdf", Path.GetFileName(pdfPath));

            //var response = await _client.PostAsync(extractUrl, content);
            //response.EnsureSuccessStatusCode();

            //var json = await response.Content.ReadAsStringAsync();
            //var job = JsonConvert.DeserializeObject<ExtractJobResponse>(json);
            //return job.JobId;

            var baseUrl = _config["AIService:ExtractionUrl"];
            var extractUrl = $"{baseUrl}extract?language={language}";

            using var client = _client.CreateClient(); // ✅ ensure same factory-based client as in controller
            using var content = new MultipartFormDataContent();

            await using var fileStream = File.OpenRead(pdfPath);
            var fileName = Path.GetFileName(pdfPath);

            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            // ✅ Properly encode and attach file name
            var encodedFileName = Uri.EscapeDataString(fileName);

            // ✅ Add the file as form-data exactly like browser would
            content.Add(fileContent, "pdf", encodedFileName);

            // (optional but helps Python/Flask API parse multipart boundaries)
            content.Headers.ContentType.MediaType = "multipart/form-data";

            // Send the POST
            var response = await client.PostAsync(extractUrl, content);

            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Extract API failed: {(int)response.StatusCode} {response.ReasonPhrase}\nResponse: {result}");
            }

            var job = JsonConvert.DeserializeObject<ExtractJobResponse>(result);
            if (job == null || string.IsNullOrEmpty(job.JobId))
                throw new Exception($"Invalid extract response: {result}");

            return job.JobId;
        }
    }
}
