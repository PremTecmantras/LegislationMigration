using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class LegislationStatusType
{
    public int StatusId { get; set; }

    public string StatusNameEn { get; set; } = null!;

    public string StatusDescription { get; set; } = null!;

    public string? StatusNameAr { get; set; }

    public virtual ICollection<LegislationSilver> LegislationSilvers { get; set; } = new List<LegislationSilver>();

    public virtual ICollection<Legislation> Legislations { get; set; } = new List<Legislation>();

    public virtual ICollection<LegislationsDataSourceRefTable> LegislationsDataSourceRefTables { get; set; } = new List<LegislationsDataSourceRefTable>();
}
