using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class ReferencedLegislation
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

    public virtual Legislation? Legislation { get; set; }

    public virtual Legislation? ReferencedLegislationNavigation { get; set; }

    public virtual ReferencedLegislationType? ReferencedLegislationType { get; set; }
}
