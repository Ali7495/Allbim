using Common.Utilities;
using Models.Factor;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.FactorServices
{
    public interface IFactorServices
    {
        Task<PagedResult<FactorViewModel>> GetAllFactorsMine(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<FactorViewModel> GetFactorMine(long userId, long id, CancellationToken cancellationToken);
    }
}
