using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class Domain
{
    public int DomainId { get; set; }

    public string DomainName { get; set; } = null!;

    public string Rationale { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? DomainTypeId { get; set; }

    public int ArticleId { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual DomainType? DomainType { get; set; }
}
