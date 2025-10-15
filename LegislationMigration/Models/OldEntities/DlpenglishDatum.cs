using System;
using System.Collections.Generic;

namespace LegislationMigration.Models.Entities;

public partial class DlpenglishDatum
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public short? OfficialGazetteNo { get; set; }

    public string? IssuingAuthority { get; set; }

    public DateOnly? DateOfIssuance { get; set; }

    public string? Status { get; set; }

    public string? ViewAsPdf { get; set; }

    public string? ViewAsHtml { get; set; }

    public string? ArabicLegislation { get; set; }

    public string? Description1 { get; set; }

    public string? Description2 { get; set; }

    public string? PrimaryMenu { get; set; }

    public string? SecondaryMenu { get; set; }

    public string? PdffileName { get; set; }

    public bool? FoundLocally { get; set; }

    public long? LegislationId { get; set; }

    public int? LegislationNumber { get; set; }

    public virtual Legislation? Legislation { get; set; }
}
