using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class DomainType
{
    public int Id { get; set; }

    public string DomainName { get; set; } = null!;

    public int LanguageId { get; set; }

    public virtual ICollection<DomainSilver> DomainSilvers { get; set; } = new List<DomainSilver>();

    public virtual ICollection<Domain> Domains { get; set; } = new List<Domain>();

    public virtual Language Language { get; set; } = null!;
}
