using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class IssuingAuthorityType
{
    public int IssuingAuthorityId { get; set; }

    public string? AuthorityNameEn { get; set; }

    public string? AuthorityNameAr { get; set; }

    public int AuthorityRank { get; set; }

    public string? Jurisdiction { get; set; }

    public string? LookupKeywords { get; set; }

    public int SourceId { get; set; }

    public virtual ICollection<DraftLegislation> DraftLegislations { get; set; } = new List<DraftLegislation>();

    public virtual ICollection<LegislationSilver> LegislationSilvers { get; set; } = new List<LegislationSilver>();

    public virtual ICollection<Legislation> Legislations { get; set; } = new List<Legislation>();

    public virtual Source Source { get; set; } = null!;
}
