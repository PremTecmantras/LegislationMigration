using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class VwLegislationBase
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

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? SourceId { get; set; }

    public int LanguageId { get; set; }

    public string? Aisummary { get; set; }

    public string? DisplayName { get; set; }

    public string? Json { get; set; }

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

    public string LegislationTypeEn { get; set; } = null!;

    public string? LegislationTypeAr { get; set; }

    public string? AuthorityNameEn { get; set; }

    public string? AuthorityNameAr { get; set; }

    public string? Appendix { get; set; }

    public string? StatusNameAr { get; set; }
}
