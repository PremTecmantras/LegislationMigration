using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class ErrorLog
{
    public Guid Id { get; set; }

    public string Type { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? StackTrace { get; set; }

    public string? InnerException { get; set; }

    public string? ExtraInfo { get; set; }

    public DateTime DateCreated { get; set; }
}
