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
    public class PolicyRequestFactorDetailRepository : Repository<PolicyRequestFactorDetail>, IPolicyRequestFactorDetailRepository
    {
        public PolicyRequestFactorDetailRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PolicyRequestFactorDetail> GetByIdNoTracking(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(d => d.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<PolicyRequestFactorDetail>> GetFactorDetialsPaging(long factorId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(d => d.PolicyRequestFactorId == factorId).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }
    }
}
