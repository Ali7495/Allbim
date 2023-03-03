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
    public interface IInsurerTermDetailRepository : IRepository<InsurerTermDetail>
    {
        Task<PagedResult<InsurerTermDetail>> GetAllTermDetails(long termId, PageAbleModel pageAbleModel,
            CancellationToken cancellationToken);

        Task<List<InsurerTermDetail>> GetAllTermDetailList(long termId,
            CancellationToken cancellationToken);

        Task<InsurerTermDetail> GetTermDetailNoTracking(long id,
            CancellationToken cancellationToken);
    }
}
