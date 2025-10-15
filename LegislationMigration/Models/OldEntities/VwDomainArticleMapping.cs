using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class VwDomainArticleMapping
{
    public int DomainArticleId { get; set; }

    public int? DomainTypeId { get; set; }

    public string DomainTypeName { get; set; } = null!;

    public string Rationale { get; set; } = null!;

    public int ArticleId { get; set; }

    public int LanguageId { get; set; }
}
