using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class Document
{
    public Guid Id { get; set; }

    public string? FileName { get; set; }

    public string? MetaData { get; set; }
}
