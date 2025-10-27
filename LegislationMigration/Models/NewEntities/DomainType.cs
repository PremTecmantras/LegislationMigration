using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LegislationMigration.Models.NewEntities;

public partial class DomainType
{
    [Key]
    public int Id { get; set; }

    public string DomainName { get; set; } = null!;

    public int LanguageId { get; set; }

    public virtual ICollection<DomainSilver> DomainSilvers { get; set; } = new List<DomainSilver>();

    public virtual ICollection<Domain> Domains { get; set; } = new List<Domain>();

    public virtual Language Language { get; set; } = null!;
}
