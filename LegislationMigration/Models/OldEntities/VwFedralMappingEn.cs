using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class VwFedralMappingEn
{
    public long LegislationId { get; set; }

    public string Title { get; set; } = null!;

    public int StatusId { get; set; }

    public DateTime DateOfIssuance { get; set; }

    public short FedralId { get; set; }

    public string OldFileName { get; set; } = null!;

    public string NewFileName { get; set; } = null!;

    public string? GeneratedFileName { get; set; }

    public string? DownloadUrl { get; set; }

    public string? PdfFileName { get; set; }

    public string Identifier { get; set; } = null!;

    public string Source { get; set; } = null!;

    public string? RepealedLegislation { get; set; }

    public string? AmendedLegislation { get; set; }

    public string OriginalAmendingRepealing { get; set; } = null!;

    public string RelatedLegislationYear { get; set; } = null!;

    public string RelatedLegislationNumber { get; set; } = null!;

    public string RelatedLegislationTitle { get; set; } = null!;

    public string DownloadInfo { get; set; } = null!;

    public string PrimarySector { get; set; } = null!;

    public string PrimaryLawType { get; set; } = null!;

    public string LegislationState { get; set; } = null!;

    public string OfficialGazetteNo { get; set; } = null!;

    public DateOnly? OfficialGazetteDate { get; set; }

    public DateOnly? EffectiveDate { get; set; }

    public DateOnly IssuedDate { get; set; }

    public string Language { get; set; } = null!;

    public string DetailUrl { get; set; } = null!;

    public short? Year { get; set; }

    public short Number { get; set; }

    public string TitleMeta { get; set; } = null!;
}
