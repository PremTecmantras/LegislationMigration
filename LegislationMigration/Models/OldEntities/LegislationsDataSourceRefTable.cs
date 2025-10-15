using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class LegislationsDataSourceRefTable
{
    public int Id { get; set; }

    public string DirPath { get; set; } = null!;

    public string? PdfdirPath { get; set; }

    public int LanguageId { get; set; }

    public int SourceId { get; set; }

    public int LegislationStatusTypeId { get; set; }

    public bool Imported { get; set; }

    public virtual Language Language { get; set; } = null!;

    public virtual LegislationStatusType LegislationStatusType { get; set; } = null!;

    public virtual Source Source { get; set; } = null!;
}
