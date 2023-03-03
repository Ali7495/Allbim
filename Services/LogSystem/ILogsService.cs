using Common.Utilities;
using Models.LogsModels;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Models.LoggingCmnModels;

namespace Services.LogSystem
{
    public interface ILogsService
    {
        Task<PagedResult<HandledErrorLogOutputViewModel>> GetAllPagedResult_HandledError(PageAbleResult pageAbleResult, long UserRoleID, long UserId, CancellationToken cancellationToken);
        Task<PagedResult<OprationLogOutputViewModel>> GetAllPagedResult_Operation(PageAbleResult pageAbleResult, long UserRoleID, long UserId, CancellationToken cancellationToken);
        Task<PagedResult<SystemErrorLogOutputViewModel>> GetAllPagedResult_SystemError(PageAbleResult pageAbleResult,long UserRoleID, long UserId, CancellationToken cancellationToken);
        Task<bool> LogError_V2(Exception ex, LoggerCLSViewModel _loggerCLS,CancellationToken cancellationToken);
        Task<bool> LogData_V2(LoggerCLSViewModel _loggerCLS,CancellationToken cancellationToken);
    }
}
