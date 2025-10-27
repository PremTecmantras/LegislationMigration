using LegislationMigration.Data;
using LegislationMigration.Models.NewEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislationMigration.Services.Interfaces
{
    public interface IReprocessService
    {
        Task ReprocessLegislationAsync(string pdfFolderPath);
        Task ProcessPdfBatchAsync(List<string> pdfBatch, List<Legislation> legislation, NewDbContext Db);
    }
}
