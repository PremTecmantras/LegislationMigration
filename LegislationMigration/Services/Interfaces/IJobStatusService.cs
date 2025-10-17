using LegislationMigration.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegislationMigration.Services.Interfaces
{
    public interface IJobStatusService
    {
        Task<JobStatusResponse> GetJobStatusAsync(string jobId);
    }
}
