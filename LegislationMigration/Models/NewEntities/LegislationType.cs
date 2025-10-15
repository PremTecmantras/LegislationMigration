using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class LegislationType
{
    public int LegislationTypeId { get; set; }

    public string NameEn { get; set; } = null!;

    public string? NameAr { get; set; }

    public int SourceId { get; set; }

    public string? Lookup { get; set; }

    public virtual ICollection<Legislation> Legislations { get; set; } = new List<Legislation>();

    public virtual Source Source { get; set; } = null!;
}
