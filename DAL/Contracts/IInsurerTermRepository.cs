using Common.Utilities;
using DAL.Models;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IInsurerTermRepository : IRepository<InsurerTerm>
    {
        Task<List<InsurerTerm>> GetAllByCode(Guid code, long id, CancellationToken cancellationToken);
        Task<InsurerTerm> GetWithDetailsById(long id, CancellationToken cancellationToken);
        Task<int> GetCountByInsurerId(long id, CancellationToken cancellationToken);
        Task<PagedResult<InsurerTerm>> GetAllInsurerTerms(long insuranceId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
    }
}
