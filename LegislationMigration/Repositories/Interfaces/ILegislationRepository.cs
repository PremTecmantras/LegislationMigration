using LegislationMigration.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislationMigration.Repositories.Interfaces
{
    public interface ILegislationRepository
    {
        Task<List<Legislation>> GetPendingLegislationsAsync();
        Task UpdateJobStatusAsync(int legislationId, string jobId, string status);
    }
}
