using LegislationMigration.Data;
using LegislationMigration.Models.Entities;
using LegislationMigration.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislationMigration.Repositories.Implementations
{
    public class LegislationRepository : ILegislationRepository
    {
        private readonly MyDbContext _context;
        public LegislationRepository(MyDbContext context)
        {
            _context = context;
        }

        public Task<List<Legislation>> GetPendingLegislationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateJobStatusAsync(int legislationId, string jobId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
