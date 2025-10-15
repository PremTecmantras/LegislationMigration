using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? AppendixId { get; set; }

    public long? LegislationId { get; set; }

    public int? ArticleId { get; set; }

    public string CommentBody { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? DisplayName { get; set; }

    public virtual Appendix? Appendix { get; set; }

    public virtual Article? Article { get; set; }

    public virtual Legislation? Legislation { get; set; }
}
