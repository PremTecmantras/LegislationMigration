using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class FederalMetaArabic
{
    public short Id { get; set; }

    public string Title { get; set; } = null!;

    public string PdfTitle { get; set; } = null!;

    public short? Number { get; set; }

    public short Year { get; set; }

    public string DetailUrl { get; set; } = null!;

    public string Language { get; set; } = null!;

    public string IssuedDate { get; set; } = null!;

    public string? EffectiveDate { get; set; }

    public string? OfficialGazetteDate { get; set; }

    public string OfficialGazetteNo { get; set; } = null!;

    public string LegislationState { get; set; } = null!;

    public string PrimaryLawType { get; set; } = null!;

    public string PrimarySector { get; set; } = null!;

    public string DownloadInfo { get; set; } = null!;

    public string RelatedLegislationTitle { get; set; } = null!;

    public string RelatedLegislationNumber { get; set; } = null!;

    public string RelatedLegislationYear { get; set; } = null!;

    public string OriginalAmendingRepealing { get; set; } = null!;

    public string? AmendedLegislation { get; set; }

    public string? RepealedLegislation { get; set; }

    public string Source { get; set; } = null!;

    public string Identifier { get; set; } = null!;

    public string? DownloadUrl { get; set; }

    public string? GeneratedFileName { get; set; }

    public string? PdfFileName { get; set; }
}
