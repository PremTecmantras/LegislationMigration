using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class VwAmmendedLegislation
{
    public long LegislationId { get; set; }

    public string Title { get; set; } = null!;

    public int StatusId { get; set; }

    public DateTime DateOfIssuance { get; set; }

    public string? HijriDate { get; set; }

    public int IssuingAuthorityId { get; set; }

    public int LegislationTypeId { get; set; }

    public string? OfficialGazetteNumber { get; set; }

    public string? SourceFileName { get; set; }

    public string? PdfUrl { get; set; }

    public string? Aisummary { get; set; }

    public int LanguageId { get; set; }

    public int? SourceId { get; set; }

    public string? DisplayName { get; set; }

    public int? CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public string? OriginalLegislationTitle { get; set; }

    public string NameEn { get; set; } = null!;

    public string? NameAr { get; set; }

    public string? AmendmentText { get; set; }

    public string LegislationTypeNameEn { get; set; } = null!;

    public string? LegislationTypeNameAr { get; set; }

    public string StatusNameEn { get; set; } = null!;

    public string? StatusNameAr { get; set; }

    public string SourceNameEn { get; set; } = null!;

    public string? SourceNameAr { get; set; }

    public string? StatusNameArabic { get; set; }

    public string? OriginalArticleTitle { get; set; }

    public string? OriginalArticleBody { get; set; }

    public string? OriginalArticleNumber { get; set; }

    public string? ModifiedArticleTitle { get; set; }

    public string? ModifiedArticleBody { get; set; }
}
