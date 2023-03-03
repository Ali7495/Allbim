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
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        public ContactUsRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<ContactUs> GetLatestContactUs(CancellationToken cancellationToken)
        {
            return await Table.IgnoreQueryFilters().AsNoTracking().OrderByDescending(x=>x.CreatedDateTime).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<ContactUs>> GetAllContactUs(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
            
        }
    }
}
