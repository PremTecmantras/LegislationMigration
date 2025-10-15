using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class ConflictIntensityTypesNew
{
    public int Id { get; set; }

    public string? IntensityEn { get; set; }

    public string? IntensityAr { get; set; }
}
