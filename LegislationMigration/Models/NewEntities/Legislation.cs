using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LegislationMigration.Models.NewEntities;

public partial class Legislation
{
    [Key]
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

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual Category? Category { get; set; }

    public virtual IssuingAuthorityType IssuingAuthority { get; set; } = null!;

    public virtual Language Language { get; set; } = null!;

    public virtual LegislationType LegislationType { get; set; } = null!;

    public virtual Source? Source { get; set; }

    public virtual LegislationStatusType Status { get; set; } = null!;

    public virtual SubCategory? SubCategory { get; set; }
}
