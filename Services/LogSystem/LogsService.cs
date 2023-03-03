using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using Logging.CmnModels;
using Logging.Contracts;
using Logging.LogModels;
using Microsoft.Extensions.Options;
using Models.LoggingCmnModels;
using Models.LogsModels;
using Models.PageAble;
using Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.LogSystem
{
    public class LogsService : ILogsService
    {
        private readonly IHandledErrorRepository _handledErrorRepository;
        private readonly IOprationLogRepository _oprationLogRepository;
        private readonly ISystemErrorLogRepository _systemErrorLogRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;
        public LogsService(IUserRoleRepository userRoleRepository, IHandledErrorRepository handledErrorRepository, ISystemErrorLogRepository systemErrorLogRepository, IOprationLogRepository oprationLogRepository, IMapper mapper)
        {
            _handledErrorRepository = handledErrorRepository;
            _oprationLogRepository = oprationLogRepository;
            _systemErrorLogRepository = systemErrorLogRepository;
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }
        public async Task<PagedResult<HandledErrorLogOutputViewModel>> GetAllPagedResult_HandledError(PageAbleResult pageAbleResult, long UserRoleID, long UserId, CancellationToken cancellationToken)
        {
            bool ValidateRoleResult = await ValidateRole(UserId, UserRoleID, cancellationToken);
            if (!ValidateRoleResult)
                throw new BadRequestException("شما مجاز به فراخوانی این سرویس نمی باشید");
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            pageAbleModel = pageAbleModelSetData(pageAbleModel);
            PagedResult<HandledErrorLog> Result = await _handledErrorRepository.GetPageableResult(pageAbleModel, cancellationToken);
            return _mapper.Map<PagedResult<HandledErrorLogOutputViewModel>>(Result);
        }

        public async Task<PagedResult<OprationLogOutputViewModel>> GetAllPagedResult_Operation(PageAbleResult pageAbleResult, long UserRoleID, long UserId, CancellationToken cancellationToken)
        {
            bool ValidateRoleResult = await ValidateRole(UserId, UserRoleID, cancellationToken);
            if (!ValidateRoleResult)
                throw new BadRequestException("شما مجاز به فراخوانی این سرویس نمی باشید");
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            pageAbleModel = pageAbleModelSetData(pageAbleModel);
            PagedResult<OperationLog> Result = await _oprationLogRepository.GetPageableResult(pageAbleModel, cancellationToken);
            return _mapper.Map<PagedResult<OprationLogOutputViewModel>>(Result);
        }

        public async Task<PagedResult<SystemErrorLogOutputViewModel>> GetAllPagedResult_SystemError(PageAbleResult pageAbleResult, long UserRoleID, long UserId, CancellationToken cancellationToken)
        {
            bool ValidateRoleResult = await ValidateRole(UserId, UserRoleID, cancellationToken);
            if (!ValidateRoleResult)
                throw new BadRequestException("شما مجاز به فراخوانی این سرویس نمی باشید");
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            pageAbleModel = pageAbleModelSetData(pageAbleModel);
            PagedResult<SystemErrorLog> Result = await _systemErrorLogRepository.GetPageableResult(pageAbleModel, cancellationToken);
            return _mapper.Map<PagedResult<SystemErrorLogOutputViewModel>>(Result);
        }
        public async Task<bool> ValidateRole(long UserId, long RoleID, CancellationToken cancellationToken)
        {
            var userRole = await _userRoleRepository.GetUserRole(UserId, RoleID, cancellationToken);
            if (userRole == null)
                throw new BadRequestException("این کاربر با این نقش وجود ندارد");

            if (userRole.Role.Name == "Admin")
                return true;
            else
                return false;
        }
        public PageAbleModel pageAbleModelSetData(PageAbleModel _pageAbleModel)
        {
            if (_pageAbleModel.SortField == null)
                _pageAbleModel.SortField = "ID";
            if (_pageAbleModel.SortOrder == null)
                _pageAbleModel.SortOrder = "desc";
            return _pageAbleModel;
        }
        public async Task<bool> LogError_V2(Exception ex, LoggerCLSViewModel _loggerCLS,CancellationToken cancellationToken)
        {
            #region Definitions
            Type _Extype = ex.GetType();
            #endregion
            if (_Extype == typeof(Common.Exceptions.ITException) || _Extype == typeof(Common.Exceptions.LogicException) || _Extype == typeof(Common.Exceptions.NotFoundException) || _Extype == typeof(Common.Exceptions.CustomException) || _Extype == typeof(Common.Exceptions.BadRequestException) || _Extype == typeof(Common.Exceptions.AppException))
            {
                HandledErrorLog errorLog = new HandledErrorLog()
                {
                    CreatedDateTime = DateTime.Now,
                    ServiceName = _loggerCLS.ServiceName,
                    MethodName = _loggerCLS.MethodName,
                    RequestPayload = _loggerCLS.Parameters,
                    ErrorCode = _Extype.FullName,
                    ErrorMessage = ex.Message,
                    RequestQueryParams = _loggerCLS.QueryParams
                };
                await _handledErrorRepository.AddAsync(errorLog,cancellationToken);
            }
            else
            {
                SystemErrorLog errorLog = new SystemErrorLog()
                {
                    CreatedDateTime = DateTime.Now,
                    ExceptionStr = ex.ToString(),
                    ServiceName = _loggerCLS.ServiceName,
                    RequestPayload = _loggerCLS.Parameters,
                    RequestQueryParams = _loggerCLS.QueryParams,
                    RequestUrl = _loggerCLS.RequestUrl
                };
                await _systemErrorLogRepository.AddAsync(errorLog,cancellationToken);
            }
            return true;
        }
        public async Task<bool> LogData_V2(LoggerCLSViewModel _loggerCLS,CancellationToken cancellationToken)
        {
            OperationLog log = new OperationLog()
                {
                    ExecuteTime = _loggerCLS.ExecuteTime == null ? null : _loggerCLS.ExecuteTime.ToString(),
                    MethodName = _loggerCLS.MethodName,
                    ServiceName = _loggerCLS.ServiceName,
                    FormBodyParameters = _loggerCLS.Parameters,
                    AuthenticatedUser = _loggerCLS.uthenticatedUser,
                    CreateDateTime = _loggerCLS.CreateDateTime,
                    QueryStringParameters = _loggerCLS.QueryParams,
                    RequestUrl = _loggerCLS.RequestUrl,
                    Response = _loggerCLS.Response,
                    StatusCode = _loggerCLS.StatusCode
                };
                await _oprationLogRepository.AddAsync(log,cancellationToken);
                return true;
        }

    }
}
