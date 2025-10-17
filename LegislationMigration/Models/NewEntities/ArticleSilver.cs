using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class ArticleSilver
{
    public int ArticleId { get; set; }

    public long LegislationId { get; set; }

    public string ArticleTitle { get; set; } = null!;

    public int? ArticleNumber { get; set; }

    public string ArticleBody { get; set; } = null!;

    public string? DisplayName { get; set; }

    public bool Active { get; set; }

    public string? Aisummary { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool HasRelatedArticles { get; set; }

    public virtual ICollection<AmendedLegislationsSilver> AmendedLegislationsSilvers { get; set; } = new List<AmendedLegislationsSilver>();

    public virtual ICollection<DomainSilver> DomainSilvers { get; set; } = new List<DomainSilver>();

    public virtual LegislationSilver Legislation { get; set; } = null!;
}
