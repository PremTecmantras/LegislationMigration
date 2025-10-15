using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class ExtractedTableCell
{
    public int CellId { get; set; }

    public int TableId { get; set; }

    public string? CellText { get; set; }

    public int? RowIndex { get; set; }

    public int? RowSpan { get; set; }

    public int? ColSpan { get; set; }

    public int? ColIndex { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? DisplayName { get; set; }

    public virtual ExtractedTable Table { get; set; } = null!;
}
