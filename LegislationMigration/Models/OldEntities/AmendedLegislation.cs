using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class AmendedLegislation
{
    public long Id { get; set; }

    public long? OriginalLegislationId { get; set; }

    public long? ModifiedLegislationId { get; set; }

    public string? OriginalLegislationTitle { get; set; }

    public int? OriginalArticleId { get; set; }

    public int? ModifiedArticleId { get; set; }

    public string? AmendmentUri { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? DisplayName { get; set; }

    public int? AmendmentTypeId { get; set; }

    public string? AmendmentText { get; set; }

    public string? OriginalArticleNumber { get; set; }

    public virtual AmendmentType? AmendmentType { get; set; }

    public virtual Article? OriginalArticle { get; set; }

    public virtual Legislation? OriginalLegislation { get; set; }
}
