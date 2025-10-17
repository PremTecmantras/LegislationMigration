using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class ReferencedLegislationsSilver
{
    public int ReferenceId { get; set; }

    public long? LegislationId { get; set; }

    public string? ReferencedLegislationTitle { get; set; }

    public long? ReferencedLegislationId { get; set; }

    public int? ReferencedLegislationTypeId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? DisplayName { get; set; }

    public virtual LegislationSilver? Legislation { get; set; }

    public virtual LegislationSilver? ReferencedLegislation { get; set; }

    public virtual ReferencedLegislationType? ReferencedLegislationType { get; set; }
}
