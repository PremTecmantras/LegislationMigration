using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class VwLegislationRerferenceAmmendment
{
    public int ArticleId { get; set; }

    public int? ReferenceId { get; set; }

    public long? AmmendmentId { get; set; }

    public long LegislationId { get; set; }

    public string Title { get; set; } = null!;

    public int StatusId { get; set; }

    public DateTime DateOfIssuance { get; set; }

    public string? HijriDate { get; set; }

    public int IssuingAuthorityId { get; set; }

    public int LegislationTypeId { get; set; }

    public string? OfficialGazetteNumber { get; set; }

    public string? PdfUrl { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? SourceFileName { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? SourceId { get; set; }

    public string? Aisummary { get; set; }

    public int LanguageId { get; set; }

    public string? DisplayName { get; set; }

    public int? CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public string? CategoryNameEn { get; set; }

    public string SubCategoryNameEn { get; set; } = null!;

    public string? CategoryNameAr { get; set; }

    public string SubCategoryNameAr { get; set; } = null!;

    public string SourceNameEn { get; set; } = null!;

    public string? SourceNameAr { get; set; }

    public string LanguageNameEn { get; set; } = null!;

    public string StatusNameEn { get; set; } = null!;

    public string? StatusNameAr { get; set; }

    public string LegislationTypeEn { get; set; } = null!;

    public string? LegislationTypeAr { get; set; }

    public string? AuthorityNameEn { get; set; }

    public string? AuthorityNameAr { get; set; }

    public string? ReferencedLegislationTitle { get; set; }

    public string? RelationshipTypeEn { get; set; }

    public string? RelationshipTypeAr { get; set; }

    public string? AmendmentText { get; set; }

    public string? OriginalArticleNumber { get; set; }

    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    public string? ModifiedArticleNumber { get; set; }

    public string ArticleTitle { get; set; } = null!;

    public int? ArticleNumber { get; set; }

    public string ArticleBody { get; set; } = null!;

    public string? DisplayName1 { get; set; }

    public string? Appendix { get; set; }
}
