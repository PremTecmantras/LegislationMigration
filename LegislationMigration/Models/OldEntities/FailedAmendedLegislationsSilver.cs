using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class FailedAmendedLegislationsSilver
{
    public long Id { get; set; }

    public long LegislationId { get; set; }

    public string Action { get; set; } = null!;

    public int ArticleNumber { get; set; }

    public string TargetLegislation { get; set; } = null!;

    public bool IsProcessed { get; set; }

    public virtual LegislationSilver Legislation { get; set; } = null!;
}
