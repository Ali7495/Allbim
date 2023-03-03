using Common.Utilities;
using Logging.LogModels;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logging.Contracts
{
    public interface IHandledErrorRepository : ILoggingRepository<HandledErrorLog>
    {
        Task<PagedResult<HandledErrorLog>> GetPageableResult(PageAbleModel pageAbleModel, CancellationToken cancellationToken);
    }
}
