using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class OwnershipInvolvement
{
    public int OwnershipId { get; set; }

    public long? LegislationId { get; set; }

    public string? OwnershipEntity { get; set; }

    public string? InvolvedEntities { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? DisplayName { get; set; }

    public virtual Legislation? Legislation { get; set; }
}
