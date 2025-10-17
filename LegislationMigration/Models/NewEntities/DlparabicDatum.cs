using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.NewEntities;

public partial class DlparabicDatum
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public string? OfficialGazetteNo { get; set; }

    public string? IssuingAuthority { get; set; }

    public string? DateOfIssuance { get; set; }

    public string? Status { get; set; }

    public string? PdfUrl { get; set; }

    public string? HtmlUrl { get; set; }

    public string? EnglishUrl { get; set; }

    public string? Description2 { get; set; }

    public string? Description1 { get; set; }

    public string? PrimaryMenu { get; set; }

    public string? SecondaryMenu { get; set; }

    public string? PdffileName { get; set; }

    public string? Translated { get; set; }

    public bool? FoundLocally { get; set; }

    public long? LegislationId { get; set; }

    public int? LegislationNumber { get; set; }

    public virtual Legislation? Legislation { get; set; }
}
