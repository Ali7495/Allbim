using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PolicyRequestDetailRepository : Repository<PolicyRequestDetail>, IPolicyRequestDetailRepository
    {
        public PolicyRequestDetailRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<PolicyRequestDetail>> GetDetailsByPolicyRequestId(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.PolicyRequestId == id).ToListAsync(cancellationToken);
        }


        public async Task<PagedResult<PolicyRequestDetail>> GetCompanyPolicyRequestDetailsByPolicyCode(Guid code, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.PolicyRequest.Code == code)
                .Include(i => i.PolicyRequest.AgentSelected).ThenInclude(th => th.Person)
                .Include(i => i.PolicyRequest.Insurer).ThenInclude(th => th.Insurance)
                .Include(i => i.PolicyRequest.PolicyRequestStatus)
                .Include(i => i.PolicyRequest.RequestPerson)
                .Include(i => i.PolicyRequest.Reviewer)
                .Include(i => i.InsurerTerm)
                .GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);

        }
    }
}
