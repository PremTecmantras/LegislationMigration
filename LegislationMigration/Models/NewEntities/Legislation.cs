using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class Legislation
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

    public string? Embeddings { get; set; }

    public bool Active { get; set; }

    public int? Number { get; set; }

    public decimal? Version { get; set; }

    public int? ParentLegislationId { get; set; }

    public string? JobId { get; set; }

    public string? JobStatus { get; set; }

    public virtual ICollection<AmendedLegislation> AmendedLegislations { get; set; } = new List<AmendedLegislation>();

    public virtual ICollection<Appendix> Appendices { get; set; } = new List<Appendix>();

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<DlparabicDatum> DlparabicData { get; set; } = new List<DlparabicDatum>();

    public virtual ICollection<DlpenglishDatum> DlpenglishData { get; set; } = new List<DlpenglishDatum>();

    public virtual ICollection<ExtractedImage> ExtractedImages { get; set; } = new List<ExtractedImage>();

    public virtual ICollection<ExtractedTable> ExtractedTables { get; set; } = new List<ExtractedTable>();

    public virtual ICollection<FailedAmendedLegislation> FailedAmendedLegislations { get; set; } = new List<FailedAmendedLegislation>();

    public virtual ICollection<FailedLegislationRelation> FailedLegislationRelations { get; set; } = new List<FailedLegislationRelation>();

    public virtual IssuingAuthorityType IssuingAuthority { get; set; } = null!;

    public virtual Language Language { get; set; } = null!;

    public virtual ICollection<LegislationConflict> LegislationConflicts { get; set; } = new List<LegislationConflict>();

    public virtual ICollection<LegislationMapping> LegislationMappings { get; set; } = new List<LegislationMapping>();

    public virtual LegislationType LegislationType { get; set; } = null!;

    public virtual ICollection<LegislationVersion> LegislationVersions { get; set; } = new List<LegislationVersion>();

    public virtual ICollection<OwnershipInvolvement> OwnershipInvolvements { get; set; } = new List<OwnershipInvolvement>();

    public virtual ICollection<ReferencedLegislation> ReferencedLegislationLegislations { get; set; } = new List<ReferencedLegislation>();

    public virtual ICollection<ReferencedLegislation> ReferencedLegislationReferencedLegislationNavigations { get; set; } = new List<ReferencedLegislation>();

    public virtual Source? Source { get; set; }

    public virtual LegislationStatusType Status { get; set; } = null!;

    public virtual SubCategory? SubCategory { get; set; }
}
