using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class ReferencedLegislationType
{
    public int Id { get; set; }

    public string RelationshipTypeEn { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? LookupKeywords { get; set; }

    public string? RelationshipTypeAr { get; set; }

    public virtual ICollection<ReferencedLegislation> ReferencedLegislations { get; set; } = new List<ReferencedLegislation>();

    public virtual ICollection<ReferencedLegislationsSilver> ReferencedLegislationsSilvers { get; set; } = new List<ReferencedLegislationsSilver>();
}
