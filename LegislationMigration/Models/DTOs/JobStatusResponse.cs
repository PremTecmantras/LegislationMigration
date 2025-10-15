using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislationMigration.Models.DTOs
{
    public class JobStatusResponse
    {
        [JsonProperty("job_id")]
        public string JobId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("started_at")]
        public DateTime? StartedAt { get; set; }

        [JsonProperty("completed_at")]
        public DateTime? CompletedAt { get; set; }

        [JsonProperty("expires_at")]
        public DateTime? ExpiresAt { get; set; }

        [JsonProperty("result")]
        public LegislationApiResponse? Result { get; set; }
    }

    public class LegislationApiResponse
    {
        public string Title { get; set; }
        public string PdfFileName { get; set; }
        public List<ApiArticleDTO> Articles { get; set; }
        public List<ApiLegislationRelationDTO> LegislationRelations { get; set; }
        public List<ApiArticleModificationDTO> ArticleModifications { get; set; }
    }

    public class ApiArticleDTO
    {
        [JsonProperty("Article Number")]
        public int ArticleNumber { get; set; }
        public string Text { get; set; }
        public string Heading { get; set; }

        public List<string> DomainsEN { get; set; }
        public List<string> DomainsAR { get; set; }
        public List<string> RationalesEN { get; set; }
        public List<string> RationalesAR { get; set; }
    }

    public class ApiLegislationRelationDTO
    {
        public string ReferencedLegislation { get; set; }
        public string RelationType { get; set; }
    }

    public class ApiArticleModificationDTO
    {
        public string Action { get; set; }

        //[JsonProperty("Article_Number")]
        //public string Article_Number { get; set; }

        public string Amending_Article_Number { get; set; }
        public string Target_Article_Number { get; set; }

        //[JsonProperty("TargetLegislation")]
        //public string TargetLegislation { get; set; }  

        [JsonProperty("Target_Legislation")]
        public string Target_Legislation { get; set; }
    }
}
