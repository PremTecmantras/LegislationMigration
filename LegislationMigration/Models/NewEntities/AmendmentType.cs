using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class AmendmentType
{
    public int Id { get; set; }

    public string NameEn { get; set; } = null!;

    public string? LookupKeywords { get; set; }

    public string? NameAr { get; set; }

    public virtual ICollection<AmendedLegislation> AmendedLegislations { get; set; } = new List<AmendedLegislation>();

    public virtual ICollection<AmendedLegislationsSilver> AmendedLegislationsSilvers { get; set; } = new List<AmendedLegislationsSilver>();
}
