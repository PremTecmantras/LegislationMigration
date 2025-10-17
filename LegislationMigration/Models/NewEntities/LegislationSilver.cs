using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class LegislationSilver
{
    public long LegislationId { get; set; }

    public string? Title { get; set; }

    public int? StatusId { get; set; }

    public DateTime? DateOfIssuance { get; set; }

    public string? HijriDate { get; set; }

    public int? IssuingAuthorityId { get; set; }

    public int? LegislationTypeId { get; set; }

    public string? OfficialGazetteNumber { get; set; }

    public string? SourceFileName { get; set; }

    public string? PdfUrl { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int SourceId { get; set; }

    public int LanguageId { get; set; }

    public string? Aisummary { get; set; }

    public string? DisplayName { get; set; }

    public string? Json { get; set; }

    public int? CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public string? Embeddings { get; set; }

    public bool? Active { get; set; }

    public int? Number { get; set; }

    public decimal? Version { get; set; }

    public int? ParentLegislationId { get; set; }

    public virtual ICollection<AmendedLegislationsSilver> AmendedLegislationsSilvers { get; set; } = new List<AmendedLegislationsSilver>();

    public virtual ICollection<ArticleSilver> ArticleSilvers { get; set; } = new List<ArticleSilver>();

    public virtual Category? Category { get; set; }

    public virtual IssuingAuthorityType? IssuingAuthority { get; set; }

    public virtual Language Language { get; set; } = null!;

    public virtual LegislationType? LegislationType { get; set; }

    public virtual ICollection<ReferencedLegislationsSilver> ReferencedLegislationsSilverLegislations { get; set; } = new List<ReferencedLegislationsSilver>();

    public virtual ICollection<ReferencedLegislationsSilver> ReferencedLegislationsSilverReferencedLegislations { get; set; } = new List<ReferencedLegislationsSilver>();

    public virtual Source Source { get; set; } = null!;

    public virtual LegislationStatusType? Status { get; set; }

    public virtual SubCategory? SubCategory { get; set; }
}
