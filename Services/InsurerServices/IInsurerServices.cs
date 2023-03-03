using Common.Utilities;
using Models.Center;
using Models.Insurer;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.InsurerServices
{
    public interface IInsurerServices
    {
        Task<PagedResult<InsurerResultViewModel>> GetInsurersByCompany(Guid code, PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<InsurerResultViewModel> GetInsurer(Guid code, long id, CancellationToken cancellationToken);
        Task<InsurerResultViewModel> CreateInsurer(Guid code, long id, CancellationToken cancellationToken);
        Task<string> DeleteInsurer(Guid code, long id, CancellationToken cancellationToken);


        Task<InsurerResultViewModel> CreateInsurerMine(long userId, long insuranceId, CancellationToken cancellationToken);
        Task<InsurerResultViewModel> GetInsurerMine(long userId, long insuranceId, CancellationToken cancellationToken);
        Task<PagedResult<InsurerResultViewModel>> GetInsurersByCompanyMine(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<string> DeleteInsurerMine(long userId, long insuranceId, CancellationToken cancellationToken);
    }
}
