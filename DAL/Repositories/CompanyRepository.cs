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
using Models.Center;

namespace DAL.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Company> GetByCode(Guid code, CancellationToken cancellationToken)
        {
            //return Table.Where(p => p.Code == code).Include(i => i.Article).ThenInclude(th => th.Comments).ThenInclude(th => th.Author).Include(i => i.Article).ThenInclude(th => th.Author).SingleOrDefaultAsync(cancellationToken);
            return await Table.Include(x=>x.Article).Where(p => p.Code == code).SingleOrDefaultAsync(cancellationToken);
        }
        public async Task<Company> GetByCodeNoTracking(Guid code, CancellationToken cancellationToken)
        {
            //return Table.Where(p => p.Code == code).Include(i => i.Article).ThenInclude(th => th.Comments).ThenInclude(th => th.Author).Include(i => i.Article).ThenInclude(th => th.Author).SingleOrDefaultAsync(cancellationToken);
            return await Table.AsNoTracking().Include(x=>x.Article).Where(p => p.Code == code).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Company> GetByIdNoTracking(long id, CancellationToken cancellationToken)
        {
            //return Table.Where(p => p.Code == code).Include(i => i.Article).ThenInclude(th => th.Comments).ThenInclude(th => th.Author).Include(i => i.Article).ThenInclude(th => th.Author).SingleOrDefaultAsync(cancellationToken);
            return await Table.AsNoTracking().Include(x => x.Article).Where(p => p.Id == id).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
