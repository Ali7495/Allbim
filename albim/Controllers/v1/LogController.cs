using albim.Controllers;
using albim.Result;
using Common.Exceptions;
using Common.Extensions;
using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.LogsModels;
using Models.PageAble;
using Services.LogSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class LogController : BaseController
    {
        #region Property
        private readonly ILogsService _logsService;
        #endregion
        #region Constructor
        public LogController(ILogsService logsService)
        {
            _logsService = logsService;
        }
        #endregion
        #region Logs Actions
        [HttpGet("HandledError")]
        public async Task<ApiResult<PagedResult<HandledErrorLogOutputViewModel>>> AllHandledErrorPagedResult([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long UserId = long.Parse(HttpContext.User.Identity.GetUserId());
            var claims = HttpContext.User.Claims.ToList();
            var UserRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (UserRole == null)
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            long RoleId = long.Parse(UserRole);
            var HandledErrorLogResult = await _logsService.GetAllPagedResult_HandledError(pageAbleResult,RoleId , UserId, cancellationToken);
            return HandledErrorLogResult;
        }
        [HttpGet("Operation")]
        public async Task<ApiResult<PagedResult<OprationLogOutputViewModel>>> AllOperationLogPagedResult([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long UserId = long.Parse(HttpContext.User.Identity.GetUserId());
            var claims = HttpContext.User.Claims.ToList();
            var UserRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (UserRole == null)
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            long RoleId = long.Parse(UserRole);
            var OperationLogResult = await _logsService.GetAllPagedResult_Operation(pageAbleResult,RoleId , UserId, cancellationToken);
            return OperationLogResult;
        }
        [HttpGet("SystemError")]
        public async Task<ApiResult<PagedResult<SystemErrorLogOutputViewModel>>> AllSystemErrorPagedResult([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long UserId = long.Parse(HttpContext.User.Identity.GetUserId());
            var claims = HttpContext.User.Claims.ToList();
            var UserRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (UserRole == null)
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            long RoleId = long.Parse(UserRole);
            var SystemErrorLogResult = await _logsService.GetAllPagedResult_SystemError(pageAbleResult, RoleId, UserId, cancellationToken);
            return SystemErrorLogResult;
        }
        #endregion
    }
}
