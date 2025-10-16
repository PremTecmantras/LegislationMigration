using LegislationMigration.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislationMigration.Services.Interfaces
{
    public interface IExtractService
    {
        Task<ExtractJobResponse> SubmitExtractJobAsync(string pdfPath, string language);
    }
}
