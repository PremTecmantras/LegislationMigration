using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class ExtractedTable
{
    public int TableId { get; set; }

    public long? LegislationId { get; set; }

    public int? ArticleId { get; set; }

    public int? AppendixId { get; set; }

    public string? Content { get; set; }

    public int? NumRows { get; set; }

    public int? NumCols { get; set; }

    public string? TableHeading { get; set; }

    public int? TableOrder { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? DisplayName { get; set; }

    public virtual Appendix? Appendix { get; set; }

    public virtual Article? Article { get; set; }

    public virtual ICollection<ExtractedTableCell> ExtractedTableCells { get; set; } = new List<ExtractedTableCell>();

    public virtual Legislation? Legislation { get; set; }
}
