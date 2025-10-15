using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class LegislationStatusType
{
    public int StatusId { get; set; }

    public string StatusNameEn { get; set; } = null!;

    public string StatusDescription { get; set; } = null!;

    public string? StatusNameAr { get; set; }

    public virtual ICollection<Legislation> Legislations { get; set; } = new List<Legislation>();
}
