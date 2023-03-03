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
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        public DiscountRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedResult<Discount>> GetAllDiscounts(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(i => i.Person).Include(i => i.Insurance).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }
    }
}
