using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class Language
{
    public int LanguageId { get; set; }

    public string LanguageCode { get; set; } = null!;

    public string LanguageNameEn { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<DomainType> DomainTypes { get; set; } = new List<DomainType>();

    public virtual ICollection<DraftLegislation> DraftLegislations { get; set; } = new List<DraftLegislation>();

    public virtual ICollection<FileMapping> FileMappings { get; set; } = new List<FileMapping>();

    public virtual ICollection<LegislationSilver> LegislationSilvers { get; set; } = new List<LegislationSilver>();

    public virtual ICollection<Legislation> Legislations { get; set; } = new List<Legislation>();

    public virtual ICollection<LegislationsDataSourceRefTable> LegislationsDataSourceRefTables { get; set; } = new List<LegislationsDataSourceRefTable>();

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
