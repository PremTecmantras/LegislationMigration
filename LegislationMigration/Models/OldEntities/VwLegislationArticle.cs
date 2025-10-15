using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class VwLegislationArticle
{
    public int ArticleId { get; set; }

    public string ArticleTitle { get; set; } = null!;

    public int? ArticleNumber { get; set; }

    public string ArticleBody { get; set; } = null!;

    public long LegislationId { get; set; }

    public string Title { get; set; } = null!;

    public int StatusId { get; set; }

    public DateTime DateOfIssuance { get; set; }

    public string? HijriDate { get; set; }

    public int IssuingAuthorityId { get; set; }

    public string? OfficialGazetteNumber { get; set; }

    public string? SourceFileName { get; set; }

    public string CreatedBy { get; set; } = null!;

    public int LegislationTypeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? SourceId { get; set; }

    public int LanguageId { get; set; }

    public string? Aisummary { get; set; }

    public string? DisplayName { get; set; }
}
