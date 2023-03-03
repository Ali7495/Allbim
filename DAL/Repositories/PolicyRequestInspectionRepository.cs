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
    public class PolicyRequestInspectionRepository : Repository<PolicyRequestInspection>, IPolicyRequestInspectionRepository
    {
        public PolicyRequestInspectionRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PolicyRequestInspection> GetByPolicyRequestCode(Guid code, CancellationToken cancellationToken)
        {
            return await Table.Include(x => x.PolicyRequest)
                .Include(x => x.InspectionAddress)
                .Include(x=> x.InspectionSession)
                .Include(x=> x.CompanyCenterSchedule).ThenInclude(th=> th.CompanyCenter)
                .Where(p => p.PolicyRequest.Code == code).SingleOrDefaultAsync(cancellationToken);
        }
        public async Task<PolicyRequestInspection> GetByPolicyRequestCodeNoTracking(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(x => x.PolicyRequest)
                .Include(x => x.InspectionAddress)
                .Include(x=> x.InspectionSession)
                .Include(x=> x.CompanyCenterSchedule).ThenInclude(th=> th.CompanyCenter)
                .Where(p => p.PolicyRequest.Code == code).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
