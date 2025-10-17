using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class LegislationVersion
{
    public int VersionId { get; set; }

    public long? LegislationId { get; set; }

    public double? VersionNumber { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public string? ChangesSummary { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? DisplayName { get; set; }

    public virtual ICollection<DraftLegislation> DraftLegislations { get; set; } = new List<DraftLegislation>();

    public virtual Legislation? Legislation { get; set; }
}
