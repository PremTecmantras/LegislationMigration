using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislationMigration.Services.Interfaces
{
    public interface IJobStatusService
    {
        Task<string> GetJobStatusAsync(string jobId);
    }
}
