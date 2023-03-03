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
    public class InsurerTermDetailRepository : Repository<InsurerTermDetail>, IInsurerTermDetailRepository
    {
        public InsurerTermDetailRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<InsurerTermDetail>> GetAllTermDetailList(long termId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(i => i.InsurerTermId == termId).ToListAsync(cancellationToken);
        }

        public async Task<PagedResult<InsurerTermDetail>> GetAllTermDetails(long termId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(i => i.InsurerTermId == termId).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<InsurerTermDetail> GetTermDetailNoTracking(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(i => i.Id == id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
