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
    public class PolicyRequestIssueRepository : Repository<PolicyRequestIssue>, IPolicyRequestIssueRepository
    {
        public PolicyRequestIssueRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<PolicyRequestIssue> GetByPolicyRequestCode(Guid code, CancellationToken cancellationToken)
        {
            return await Table.Include(x=>x.PolicyRequest).Include(x=> x.IssueSession).Include(x=>x.ReceiverAddress).Where(p => p.PolicyRequest.Code == code).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
