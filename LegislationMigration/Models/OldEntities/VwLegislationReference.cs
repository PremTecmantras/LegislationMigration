using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class VwLegislationReference
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

    public int? CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public string LegislationTypeEn { get; set; } = null!;

    public string? LegislationTypeAr { get; set; }

    public string SourceNameEn { get; set; } = null!;

    public string? SourceNameAr { get; set; }

    public string? ReferencedLegislationTitle { get; set; }

    public string RelationshipTypeEn { get; set; } = null!;

    public string? RelationshipTypeAr { get; set; }

    public string SubCategoryName { get; set; } = null!;

    public string? CategoryName { get; set; }
}
