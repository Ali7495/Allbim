using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CompanyCenterScheduleRepository : Repository<CompanyCenterSchedule>, ICompanyCenterScheduleRepository
    {
        public CompanyCenterScheduleRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<CompanyCenterSchedule>> GetAllCenterSchedules(long centerId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(s => s.CompanyCenterId == centerId).Include(i => i.CompanyCenter).ToListAsync(cancellationToken);
        }

        public async Task<CompanyCenterSchedule> GetCenterScheduleByCenterId(long centerId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(s => s.CompanyCenterId == centerId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
