using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class VwLegislationInfo
{
    public int? ArticleId { get; set; }

    public long LegislationId { get; set; }

    public DateTime DateOfIssuance { get; set; }

    public string LegislationTitle { get; set; } = null!;

    public string? ArticleTitle { get; set; }

    public int? ArticleNumber { get; set; }

    public string? ArticleBody { get; set; }

    public string? CategoryName { get; set; }

    public string? SubCategoryName { get; set; }

    public string? AuthorityNameEn { get; set; }

    public string? AuthorityNameAr { get; set; }

    public string? OfficialGazetteNumber { get; set; }

    public string SourceNameEn { get; set; } = null!;

    public string? SourceNameAr { get; set; }

    public string StatusNameEn { get; set; } = null!;

    public string? StatusNameAr { get; set; }

    public string LanguageNameEn { get; set; } = null!;

    public string? LegislationTypeNameEn { get; set; }

    public string? LegislationTypeNameAr { get; set; }

    public string? SourceFileName { get; set; }

    public string? AppendixContent { get; set; }

    public string? StatusNameArabic { get; set; }

    public string? CategoryNameArabic { get; set; }

    public string? SubCategoryNameArabic { get; set; }
}
