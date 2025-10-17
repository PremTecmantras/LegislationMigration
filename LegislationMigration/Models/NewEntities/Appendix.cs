using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class Appendix
{
    public int AppendixId { get; set; }

    public long? LegislationId { get; set; }

    public string? Content { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? DisplayName { get; set; }

    public string? ContentCleaned { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<ExtractedImage> ExtractedImages { get; set; } = new List<ExtractedImage>();

    public virtual ICollection<ExtractedTable> ExtractedTables { get; set; } = new List<ExtractedTable>();

    public virtual Legislation? Legislation { get; set; }
}
