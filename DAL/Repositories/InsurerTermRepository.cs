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
    public class InsurerTermRepository : Repository<InsurerTerm>, IInsurerTermRepository
    {
        public InsurerTermRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<InsurerTerm>> GetAllByCode(Guid code, long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(i => i.Insurer.Company.Code == code && i.Insurer.InsuranceId == id).Include(i => i.Insurer).Include(i=> i.InsuranceTermType).ToListAsync(cancellationToken);
        }

        public async Task<PagedResult<InsurerTerm>> GetAllInsurerTerms(long insuranceId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(w => w.Insurer.InsuranceId == insuranceId).Include(i => i.InsuranceTermType).ThenInclude(th => th.InsuranceField).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<int> GetCountByInsurerId(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(i => i.InsurerId == id).CountAsync(cancellationToken);
        }

        public async Task<InsurerTerm> GetWithDetailsById(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(i => i.Id == id).Include(i => i.Insurer).ThenInclude(th => th.Insurance).Include(i => i.Insurer).ThenInclude(th => th.Company).Include(i=> i.InsuranceTermType).ThenInclude(th=> th.InsuranceField).FirstOrDefaultAsync(cancellationToken);
        }


    }
}
