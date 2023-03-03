using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using Models.PageAble;

namespace DAL.Contracts
{
    public interface ICompanyCenterRepository : IRepository<CompanyCenter>
    {
        Task<CompanyCenter> GetCentersWithAllData(long id, CancellationToken cancellationToken);

        Task<PagedResult<CompanyCenter>> GetAllCentersByCompanyCode(Guid code, PageAbleModel pageAbleModel,
            CancellationToken cancellationToken);

        Task<CompanyCenter> GetCenterWithSchedules(long id, CancellationToken cancellationToken);

        Task<List<CompanyCenter>> GetCentersByCityAndCompayId(long companyId, long cityId, CancellationToken cancellationToken);
    }
}
