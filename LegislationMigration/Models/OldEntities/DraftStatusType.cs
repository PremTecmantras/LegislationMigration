using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class DraftStatusType
{
    public int DraftStatusId { get; set; }

    public string StatusNameEn { get; set; } = null!;

    public string StatusDescription { get; set; } = null!;

    public string? StatusNameAr { get; set; }

    public virtual ICollection<DraftLegislation> DraftLegislations { get; set; } = new List<DraftLegislation>();
}
