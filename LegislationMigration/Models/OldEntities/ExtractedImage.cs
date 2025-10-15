using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class ExtractedImage
{
    public int ImageId { get; set; }

    public long? LegislationId { get; set; }

    public int? ArticleId { get; set; }

    public int? AppendixId { get; set; }

    public string? ImageSourceFileName { get; set; }

    public int ImageOrder { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? DisplayName { get; set; }

    public virtual Appendix? Appendix { get; set; }

    public virtual Article? Article { get; set; }

    public virtual Legislation? Legislation { get; set; }
}
