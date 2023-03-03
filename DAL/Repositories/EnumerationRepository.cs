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
    public class EnumerationRepository : Repository<Enumeration>, IEnumerationRepository
    {
        public EnumerationRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedResult<Enumeration>> GetAllEnumerationPaging(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }
    }
}
