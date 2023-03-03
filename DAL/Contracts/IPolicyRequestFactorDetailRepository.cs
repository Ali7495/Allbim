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
    public interface IPolicyRequestFactorDetailRepository : IRepository<PolicyRequestFactorDetail>
    {
        Task<PolicyRequestFactorDetail> GetByIdNoTracking(long id, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequestFactorDetail>> GetFactorDetialsPaging(long factorId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
    }
}
