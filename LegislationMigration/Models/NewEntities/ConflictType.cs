using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class ConflictType
{
    public int Id { get; set; }

    public string? ConflictDefinitionEn { get; set; }

    public string TypeName { get; set; } = null!;

    public string? ConflictDefinitionAr { get; set; }

    public virtual ICollection<LegislationConflict> LegislationConflicts { get; set; } = new List<LegislationConflict>();
}
