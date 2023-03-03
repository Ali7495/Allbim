using Common.Utilities;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Extensions;
using DAL.Contracts;
using DAL.Models;

namespace DAL.Repositories
{
    public class InsuredRequestVehicleRepository : Repository<InsuredRequestVehicle>, IInsuredRequestVehicleRepository
    {
        public InsuredRequestVehicleRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }


        public async Task<InsuredRequestVehicle> GetInsuredRequestVehicleByPolicyRequestId(long policyRequestId,  CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(x=>x.InsuredRequest).ThenInclude(y=>y.PolicyRequest).Where(p => p.InsuredRequest.PolicyRequest.Id == policyRequestId).FirstAsync(cancellationToken);
        }
        public async Task<InsuredRequestVehicle> GetInsuredRequestVehicleByPolicyRequestIdWithoutRelation(long policyRequestId,  CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(x=>x.InsuredRequest).Where(p => p.InsuredRequest.PolicyRequest.Id == policyRequestId).FirstAsync(cancellationToken);
        }

        public async Task<InsuredRequestVehicle> GetInsuredRequestVehicleByPolicyRequestIdNoTracking(long policyRequestId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.InsuredRequest.PolicyRequest.Id == policyRequestId).FirstAsync(cancellationToken);
        }
    }
}
