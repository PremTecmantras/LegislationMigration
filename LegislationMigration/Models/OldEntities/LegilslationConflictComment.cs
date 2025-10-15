using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class LegilslationConflictComment
{
    public int Id { get; set; }

    public string? Comment { get; set; }

    public long? LegislationConflictId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public virtual LegislationConflict? LegislationConflict { get; set; }
}
