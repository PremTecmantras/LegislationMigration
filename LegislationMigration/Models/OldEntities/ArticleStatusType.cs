using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class ArticleStatusType
{
    public int Id { get; set; }

    public string ArticleStatusEn { get; set; } = null!;

    public string? ArticleStatusAr { get; set; }
}
