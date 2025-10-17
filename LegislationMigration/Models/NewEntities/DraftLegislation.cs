using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class DraftLegislation
{
    public long DraftLegislationId { get; set; }

    public int? VersionId { get; set; }

    public int DraftStatusId { get; set; }

    public int DraftNumber { get; set; }

    public string? DraftTitle { get; set; }

    public string? DraftContent { get; set; }

    public int LegislationTypeId { get; set; }

    public int IssuingAuthorityId { get; set; }

    public int LanguageId { get; set; }

    public string? SourceFileName { get; set; }

    public string? Notes { get; set; }

    public DateTime InitiatedAt { get; set; }

    public DateTime? FinalizedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? DisplayName { get; set; }

    public string? Json { get; set; }

    public int? CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual DraftStatusType DraftStatus { get; set; } = null!;

    public virtual IssuingAuthorityType IssuingAuthority { get; set; } = null!;

    public virtual Language Language { get; set; } = null!;

    public virtual LegislationType LegislationType { get; set; } = null!;

    public virtual SubCategory? SubCategory { get; set; }

    public virtual LegislationVersion? Version { get; set; }
}
