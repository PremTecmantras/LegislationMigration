using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class Article
{
    public int ArticleId { get; set; }

    public long LegislationId { get; set; }

    public string ArticleTitle { get; set; } = null!;

    public string? ArticleNumberText { get; set; }

    public string OldArticleBody { get; set; } = null!;

    public string? DisplayName { get; set; }

    public bool Active { get; set; }

    public string? Aisummary { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool HasRelatedArticles { get; set; }

    public string? ArticleBody { get; set; }

    public virtual ICollection<AmendedLegislation> AmendedLegislations { get; set; } = new List<AmendedLegislation>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Domain> Domains { get; set; } = new List<Domain>();

    public virtual ICollection<ExtractedImage> ExtractedImages { get; set; } = new List<ExtractedImage>();

    public virtual ICollection<ExtractedTable> ExtractedTables { get; set; } = new List<ExtractedTable>();

    public virtual Legislation Legislation { get; set; } = null!;

    public virtual ICollection<LegislationConflict> LegislationConflicts { get; set; } = new List<LegislationConflict>();
}
