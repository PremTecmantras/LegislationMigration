using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class ConflictIntensityType
{
    public int Id { get; set; }

    public string? IntensityEn { get; set; }

    public string? IntensityAr { get; set; }

    public virtual ICollection<LegislationConflict> LegislationConflicts { get; set; } = new List<LegislationConflict>();
}
