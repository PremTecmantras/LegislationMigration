using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class Source
{
    public int SourceId { get; set; }

    public string NameEn { get; set; } = null!;

    public string SourceUrl { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool Active { get; set; }

    public string? NameAr { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<IssuingAuthorityType> IssuingAuthorityTypes { get; set; } = new List<IssuingAuthorityType>();

    public virtual ICollection<LegislationType> LegislationTypes { get; set; } = new List<LegislationType>();

    public virtual ICollection<Legislation> Legislations { get; set; } = new List<Legislation>();

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
