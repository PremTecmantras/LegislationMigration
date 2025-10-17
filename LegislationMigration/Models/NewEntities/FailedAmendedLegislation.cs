using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class FailedAmendedLegislation
{
    public long Id { get; set; }

    public long LegislationId { get; set; }

    public string Action { get; set; } = null!;

    public int ArticleNumber { get; set; }

    public string TargetLegislation { get; set; } = null!;

    public bool IsProcessed { get; set; }

    public virtual Legislation Legislation { get; set; } = null!;
}
