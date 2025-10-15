using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class SubCategory
{
    public int SubCategoryId { get; set; }

    public string CategoryNameEn { get; set; } = null!;

    public string CategoryNameAr { get; set; } = null!;

    public int CategoryId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public int? SourceId { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int LanguageId { get; set; }

    public string? Lookup { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<DraftLegislation> DraftLegislations { get; set; } = new List<DraftLegislation>();

    public virtual Language Language { get; set; } = null!;

    public virtual ICollection<LegislationSilver> LegislationSilvers { get; set; } = new List<LegislationSilver>();

    public virtual ICollection<Legislation> Legislations { get; set; } = new List<Legislation>();

    public virtual Source? Source { get; set; }
}
