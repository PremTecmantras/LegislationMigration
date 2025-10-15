using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class LegislationMapping
{
    public int Id { get; set; }

    public long LegislationId { get; set; }

    public string TargetLegislationName { get; set; } = null!;

    public int SourceId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? DisplayName { get; set; }

    public virtual Legislation Legislation { get; set; } = null!;

    public virtual Source Source { get; set; } = null!;
}
