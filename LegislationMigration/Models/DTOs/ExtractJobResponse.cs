using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislationMigration.Models.DTOs
{
    public class ExtractJobResponse
    {
        [JsonProperty("job_id")]
        public string JobId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
