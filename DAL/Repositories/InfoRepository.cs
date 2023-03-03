using DAL.Contracts;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using Microsoft.EntityFrameworkCore;
using Models.PageAble;

namespace DAL.Repositories
{
    public class InfoRepository : Repository<Info>, IInfoRepository
    {
        public InfoRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }
        
        
        public async Task<PagedResult<Info>> GetAllPaging(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }
    }
}
