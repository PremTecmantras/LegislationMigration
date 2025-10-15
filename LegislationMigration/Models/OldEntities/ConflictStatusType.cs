using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class ConflictStatusType
{
    public int Id { get; set; }

    public string NameEn { get; set; } = null!;

    public string? NameAr { get; set; }

    public virtual ICollection<LegislationConflict> LegislationConflicts { get; set; } = new List<LegislationConflict>();
}
