using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface ICompanyCenterScheduleRepository : IRepository<CompanyCenterSchedule>
    {
        Task<List<CompanyCenterSchedule>> GetAllCenterSchedules(long centerId, CancellationToken cancellationToken);
        Task<CompanyCenterSchedule> GetCenterScheduleByCenterId(long centerId, CancellationToken cancellationToken);
    }
}
