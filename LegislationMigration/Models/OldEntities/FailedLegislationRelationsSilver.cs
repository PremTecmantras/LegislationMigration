using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class FailedLegislationRelationsSilver
{
    public long Id { get; set; }

    public long LegislationId { get; set; }

    public string ReferencedLegislationTitle { get; set; } = null!;

    public string RelationType { get; set; } = null!;

    public bool IsProcessed { get; set; }

    public virtual LegislationSilver Legislation { get; set; } = null!;
}
