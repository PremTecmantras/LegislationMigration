using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class LegislationConflict
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public int? ConflictTypeId { get; set; }

    public double? Confidence { get; set; }

    public string? LlmConflictReason { get; set; }

    public int? ConflictStatusId { get; set; }

    public bool IsReviewed { get; set; }

    public string? ConflictedLegislationId { get; set; }

    public long? ConflictedArticleId { get; set; }

    public long? LegilationId { get; set; }

    public int? ArticleId { get; set; }

    public string? DisplayName { get; set; }

    public int? IntensityLevelTypeId { get; set; }

    public string? IntensityRationale { get; set; }

    public virtual Article? Article { get; set; }

    public virtual ConflictStatusType? ConflictStatus { get; set; }

    public virtual ConflictType? ConflictType { get; set; }

    public virtual ConflictIntensityType? IntensityLevelType { get; set; }

    public virtual Legislation? Legilation { get; set; }

    public virtual ICollection<LegilslationConflictComment> LegilslationConflictComments { get; set; } = new List<LegilslationConflictComment>();
}
