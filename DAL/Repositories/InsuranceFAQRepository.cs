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
    public class InsuranceFAQRepository : Repository<InsuranceFaq>, IInsuranceFAQRepository
    {
        public InsuranceFAQRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedResult<InsuranceFaq>> GetAllFAQ(long insuranceId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f => f.InsuranceId == insuranceId && f.IsDeleted == false).Include(i => i.Insurance).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<InsuranceFaq> GetByIdNoTracking(long faqId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f => f.Id == faqId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
