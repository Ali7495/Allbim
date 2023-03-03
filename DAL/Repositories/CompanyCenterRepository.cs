using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using Models.PageAble;
using Models.QueryParams;

namespace DAL.Repositories
{
    public class CompanyCenterRepository : Repository<CompanyCenter>, ICompanyCenterRepository
    {
        public CompanyCenterRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedResult<CompanyCenter>> GetAllCentersByCompanyCode(Guid code,PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(c => c.Company.Code == code)
                .Include(i => i.Company)
                .Include(i => i.City).ThenInclude(th => th.TownShip).ThenInclude(th => th.Province)
                .GetNewPagedAsync(cancellationToken,pageAbleModel,null,null);
       
        }

        public async Task<CompanyCenter> GetCentersWithAllData(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(c => c.Id == id).Include(i => i.Company).Include(i => i.City).ThenInclude(th => th.TownShip).ThenInclude(th => th.Province).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<CompanyCenter> GetCenterWithSchedules(long id, CancellationToken cancellationToken)
        {
            return await Table.Where(c => c.Id == id)
                .Include(i => i.Company)
                .Include(i => i.City).ThenInclude(th => th.TownShip).ThenInclude(th => th.Province)
                .Include(x=>x.CompanyCenterSchedules)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<CompanyCenter>> GetCentersByCityAndCompayId(long companyId, long cityId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(x => x.CompanyId == companyId && x.CityId == cityId).Include(i=> i.CompanyCenterSchedules).ToListAsync();
        }
    }
}
