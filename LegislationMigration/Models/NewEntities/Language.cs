using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class Language
{
    public int LanguageId { get; set; }

    public string LanguageCode { get; set; } = null!;

    public string LanguageNameEn { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Legislation> Legislations { get; set; } = new List<Legislation>();

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
