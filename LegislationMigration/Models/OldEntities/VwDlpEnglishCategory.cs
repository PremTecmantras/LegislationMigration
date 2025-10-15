using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class VwDlpEnglishCategory
{
    public int CategoryId { get; set; }

    public int SubCategoryId { get; set; }

    public string Category { get; set; } = null!;

    public string? SubCategory { get; set; }

    public string? PdffileName { get; set; }

    public string? Title { get; set; }

    public short? OfficialGazetteNo { get; set; }
}
