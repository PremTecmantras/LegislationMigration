using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class VwDlpArabicCategory
{
    public int CategoryId { get; set; }

    public int SubCategoryId { get; set; }

    public string Category { get; set; } = null!;

    public string? SubCategory { get; set; }

    public string? PdffileName { get; set; }

    public string? Title { get; set; }

    public string? OfficialGazetteNo { get; set; }

    public string? Description1 { get; set; }

    public string? PdfUrl { get; set; }
}
