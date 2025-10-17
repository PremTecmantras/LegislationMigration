using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class FailedLegislationRelation
{
    public long Id { get; set; }

    public long LegislationId { get; set; }

    public string ReferencedLegislationTitle { get; set; } = null!;

    public string RelationType { get; set; } = null!;

    public bool IsProcessed { get; set; }

    public virtual Legislation Legislation { get; set; } = null!;
}
