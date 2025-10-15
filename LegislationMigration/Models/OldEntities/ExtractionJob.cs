using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class ExtractionJob
{
    public string JobId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Filename { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? Result { get; set; }

    public string? Error { get; set; }

    public string? Traceback { get; set; }

    public DateTime? ExpiresAt { get; set; }
}
