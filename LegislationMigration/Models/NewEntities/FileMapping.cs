using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class FileMapping
{
    public int Id { get; set; }

    public string OldFileName { get; set; } = null!;

    public string NewFileName { get; set; } = null!;

    public int LanguageId { get; set; }

    public int SourceId { get; set; }

    public virtual Language Language { get; set; } = null!;

    public virtual Source Source { get; set; } = null!;
}
