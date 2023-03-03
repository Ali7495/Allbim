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
using Models.LoggingCmnModels;
using AutoMapper;
using DAL.Contracts;
using Logging.Contracts;
using Services.LogSystem;

namespace albim
{
    /// <summary>
    /// 
    /// </summary>
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private readonly IStringLocalizer<Resource> _localizer;

        private IMapper _mapper;
        private ILogsService _logsService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        public CustomExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env,
            ILogger<CustomExceptionHandlerMiddleware> logger, IStringLocalizer<Resource> localizer)
        {
            _next = next;
            _env = env;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task Invoke(HttpContext context,
        IMapper mapper, ILogsService logsService)
        {

            _mapper = mapper;
            _logsService = logsService;

            string message = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            ApiResultStatusCode apiStatusCode = ApiResultStatusCode.ServerError;
            context.Request.EnableBuffering();
            var reader = new StreamReader(context.Request.Body);
            string FromBodyRequest = await reader.ReadToEndAsync();
            context.Request.Body.Seek(0, SeekOrigin.Begin);
            try
            {
                await _next(context);
                // if (context.Response.StatusCode == 401)
                // throw new UnauthorizedAccessException(ApiResultStatusCode.DotNetUnAuthorized.ToDisplay());
            }
            catch (BadRequestException exception)
            {
                _logger.LogError(exception, exception.Message);
                httpStatusCode = exception.HttpStatusCode;
                apiStatusCode = exception.ApiStatusCode;
                SetExceptionResponse(exception);
                await WriteToResponseAsync(exception);
            }
            catch (AppException exception)
            {
                _logger.LogError(exception, exception.Message);
                httpStatusCode = exception.HttpStatusCode;
                apiStatusCode = exception.ApiStatusCode;
                SetAppExceptionResponse(exception);


                await WriteToResponseAsync(exception);
            }
            catch (SecurityTokenExpiredException exception)
            {
                _logger.LogError(exception, exception.Message);
                SetUnAuthorizeResponse(exception);
                await WriteToResponseAsync(exception);
            }
            catch (UnauthorizedAccessException exception)
            {
                _logger.LogError(exception, exception.Message);
                SetUnAuthorizeResponse(exception);
                await WriteToResponseAsync(exception);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                SetExceptionResponse(exception);
                await WriteToResponseAsync(exception);
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


                LoggerCLSViewModel _loggerCLS = new LoggerCLSViewModel
                {
                    MethodName = context.Request.Method,
                    QueryParams = context.Request.QueryString.ToString(),
                    RequestUrl = context.Request.Path,
                    ServiceName = context.Request.Path,
                    Parameters = FromBodyRequest,
                };
                CancellationToken cancellationToken = context?.RequestAborted ?? CancellationToken.None;
                await _logsService.LogError_V2(_ex, _loggerCLS, cancellationToken);

                await context.Response.WriteAsync(json);
            }

            void SetUnAuthorizeResponse(Exception exception)
            {
                httpStatusCode = HttpStatusCode.Unauthorized;
                apiStatusCode = ApiResultStatusCode.UnAuthorized;

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
            void SetExceptionResponse(Exception exception)
            {
                if (_env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace,
                    };
                    if (exception.InnerException != null)
                    {
                        dic.Add("InnerException.Exception", exception.InnerException.Message);
                        dic.Add("InnerException.StackTrace", exception.InnerException.StackTrace);
                    }

                    message = JsonConvert.SerializeObject(dic);
                }
                else
                {
                    message = exception.Message;
                }
            }
            void SetAppExceptionResponse(AppException exception)
            {
                if (_env.IsDevelopment())
                {
                    var dic = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace,
                    };
                    if (exception.InnerException != null)
                    {
                        dic.Add("InnerException.Exception", exception.InnerException.Message);
                        dic.Add("InnerException.StackTrace", exception.InnerException.StackTrace);
                    }

                    if (exception.AdditionalData != null)
                        dic.Add("AdditionalData", JsonConvert.SerializeObject(exception.AdditionalData));

                    message = JsonConvert.SerializeObject(dic);
                }
                else
                {
                    message = exception.Message;
                }
            }
        }
    }
}