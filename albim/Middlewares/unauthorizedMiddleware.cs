using albim.Result;
using Common;
using Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using Albim.Resources;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Serialization;
using Logging;
using Logging.CmnModels;
using Nancy.Json;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using Common.Extensions;
using albim;
using Models.LoggingCmnModels;
using AutoMapper;
using DAL.Contracts;
using Logging.Contracts;
using Services.LogSystem;

namespace Albim.Middlewares
{
    public class UnauthorizedMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IStringLocalizer<Resource> _localizer;

        private IHandledErrorRepository _handledErrorRepository;
        private IOprationLogRepository _oprationLogRepository;
        private ISystemErrorLogRepository _systemErrorLogRepository;
        private IUserRoleRepository _userRoleRepository;
        private IMapper _mapper;
        private ILogsService _logsService;
        public UnauthorizedMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger, IWebHostEnvironment env, IStringLocalizer<Resource> localizer)
        {
            _next = next;
            _logger = logger;
            _env = env;
            _localizer = localizer;
        }
        public async Task Invoke(HttpContext context, IUserRoleRepository userRoleRepository, IHandledErrorRepository handledErrorRepository, ISystemErrorLogRepository systemErrorLogRepository, IOprationLogRepository oprationLogRepository, IMapper mapper,ILogsService logsService)
        {
            _handledErrorRepository = handledErrorRepository;
            _oprationLogRepository = oprationLogRepository;
            _systemErrorLogRepository = systemErrorLogRepository;
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
            _logsService = logsService;
            string message = null;
            HttpStatusCode httpStatusCode ;
            ApiResultStatusCode apiStatusCode = ApiResultStatusCode.ServerError;
            context.Request.EnableBuffering();
            var reader = new StreamReader(context.Request.Body);
            string FromBodyRequest = await reader.ReadToEndAsync();
            context.Request.Body.Seek(0, SeekOrigin.Begin);
            try
            {
                await _next(context);
                if (context.Response.StatusCode == 401)
                    throw new UnauthorizedAccessException(ApiResultStatusCode.DotNetUnAuthorized.ToDisplay());
            }
            catch (UnauthorizedAccessException exception)
            {
                _logger.LogError(exception, exception.Message);
                SetUnAuthorizeResponse(exception);
                await WriteToResponseAsync(exception);
            }
            void SetUnAuthorizeResponse(Exception exception)
            {
                httpStatusCode = HttpStatusCode.Unauthorized;
                apiStatusCode = ApiResultStatusCode.DotNetUnAuthorized;

                if (_env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace
                    };
                    if (exception is SecurityTokenExpiredException tokenException)
                        dic.Add("Expires", tokenException.Expires.ToString());

                    message = JsonConvert.SerializeObject(dic);
                }
            }
            async Task WriteToResponseAsync(Exception _ex)
            {
                if (context.Response.HasStarted)
                    throw new InvalidOperationException(
                        "The response has already started, the http status code middleware will not be executed.");
                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
                // localize message
                var localizedMessage = message != null ? _localizer[message] : null;
                var result = new ApiResult(false, apiStatusCode, message: localizedMessage);
                var json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    ContractResolver = contractResolver,
                    Formatting = Formatting.Indented
                });

                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "application/json";

                //string QueryString = context.Request.QueryString.ToString();
                //QueryString = QueryString.Replace("?", string.Empty);
                //Dictionary<string, string> values = new Dictionary<string, string>();
                //string[] nameValues = QueryString.Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
                //foreach (var nameValue in nameValues)
                //{
                //    string[] temp = nameValue.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                //    if (temp.Length == 2)
                //        values.Add(temp[0], temp[1]);
                //}
                //string Parameters = new JavaScriptSerializer().Serialize(values.ToDictionary(item => item.Key.ToString(), item => item.Value.ToString()));
                LoggerCLSViewModel _loggerCLS = new LoggerCLSViewModel
                {
                    MethodName = context.Request.Method,
                    QueryParams = context.Request.QueryString.ToString(),
                    RequestUrl = context.Request.Path,
                    ServiceName = context.Request.Path,
                    Parameters = FromBodyRequest,
                };

                CancellationToken cancellationToken = context?.RequestAborted ?? CancellationToken.None;
                await _logsService.LogError_V2(_ex, _loggerCLS,cancellationToken);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
