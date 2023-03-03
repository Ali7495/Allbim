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
    public class PolicyRequestHolderRepository : Repository<PolicyRequestHolder>, IPolicyRequestHolderRepository
    {
        public PolicyRequestHolderRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PolicyRequestHolder> GetByPolicyRequestCode(Guid code, CancellationToken cancellationToken)
        {
            return await Table
                .Include(i => i.Person)
                    .ThenInclude(x=>x.PersonAddresses)
                    .ThenInclude(x=>x.Address)
                .Include(i => i.Company)
                .Include(x=>x.PolicyRequest)
                    .ThenInclude(x=>x.InsuredRequests)
                    .ThenInclude(x=>x.InsuredRequestVehicles)
                    .ThenInclude(x=>x.OwnerPerson)
                .Include(x=>x.PolicyRequest)
                    .ThenInclude(x=>x.InsuredRequests)
                    .ThenInclude(x=>x.InsuredRequestVehicles)
                    .ThenInclude(x=>x.OwnerCompany)
                .Include(x=>x.Address)
                .FirstOrDefaultAsync(f => f.PolicyRequest.Code == code, cancellationToken);
        }
        public async Task<PolicyRequestHolder> GetByPolicyRequestCodeWithoutRelation(Guid code, CancellationToken cancellationToken)
        {
            return await Table
                .FirstOrDefaultAsync(f => f.PolicyRequest.Code == code, cancellationToken);
        }
        public async Task<PolicyRequestHolder> GetByPolicyRequestCodeNoTracking(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking()
                .Include(i => i.Person)
                    .ThenInclude(x=>x.PersonAddresses)
                    .ThenInclude(x=>x.Address)
                .Include(i => i.Company)
                .Include(x=>x.PolicyRequest)
                    .ThenInclude(x=>x.InsuredRequests)
                    .ThenInclude(x=>x.InsuredRequestVehicles)
                    .ThenInclude(x=>x.OwnerPerson)
                .Include(x=>x.PolicyRequest)
                    .ThenInclude(x=>x.InsuredRequests)
                    .ThenInclude(x=>x.InsuredRequestVehicles)
                    .ThenInclude(x=>x.OwnerCompany)
                .Include(x=>x.Address)
                .FirstOrDefaultAsync(f => f.PolicyRequest.Code == code, cancellationToken);
        }

        public async Task<PolicyRequestHolder> GetByPolicyRequestCodeNoTrackingWithoutDetails(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking()
                .FirstOrDefaultAsync(f => f.PolicyRequest.Code == code, cancellationToken);
        }
    }
}
