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
    public interface ISystemErrorLogRepository : ILoggingRepository<SystemErrorLog>
    {
        Task<PagedResult<SystemErrorLog>> GetPageableResult(PageAbleModel pageAbleModel, CancellationToken cancellationToken);
    }
}
