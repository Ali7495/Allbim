using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Extensions;
using DAL.Contracts;
using Logging;
using Logging.CmnModels;
using Logging.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;
using Models.LoggingCmnModels;
using Nancy.Json;
using Services.LogSystem;

namespace albim
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private TimeSpan _start;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context, ILogsService _logsService)
        {
            _start = new TimeSpan(DateTime.Now.Ticks);
            await LogRequest(context, _logsService);
            await LogResponse(context, _logsService);


            //
            // TimeSpan Start = new TimeSpan(DateTime.Now.Ticks);
            // context.Request.EnableBuffering();
            // var Requestreader = new StreamReader(context.Request.Body);
            // string FromBodyRequest = await Requestreader.ReadToEndAsync();
            // context.Request.Body.Seek(0, SeekOrigin.Begin);
            // await _next.Invoke(context);
            // TimeSpan End = new TimeSpan(DateTime.Now.Ticks);
            // LoggerCLSViewModel _loggerCLS = await LoggerCLSViewModel(context, End - Start, FromBodyRequest, "");
            // CancellationToken cancellationToken = context?.RequestAborted ?? CancellationToken.None;
            // await _logsService.LogData_V2(_loggerCLS, cancellationToken);
        }

        private async Task LogRequest(HttpContext context, ILogsService _logsService)
        {
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            LoggerCLSViewModel _loggerCLS = new LoggerCLSViewModel
            {
                ExecuteTime = null,
                MethodName = context.Request.Method,
                QueryParams = context.Request.QueryString.ToString(),
                RequestUrl = context.Request.Path,
                Response = null,
                ServiceName = context.Request.Path,
                // StatusCode = context.Response.StatusCode.ToString(),
                StatusCode = null,
                uthenticatedUser = context.User?.GetId(),
                Parameters = ReadStreamInChunks(requestStream),
                // Parameters = ""
            };
            CancellationToken cancellationToken = context?.RequestAborted ?? CancellationToken.None;
            await _logsService.LogData_V2(_loggerCLS, cancellationToken);
            context.Request.Body.Position = 0;
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk,
                    0,
                    readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }

        private async Task LogResponse(HttpContext context, ILogsService _logsService)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await _next(context);
            TimeSpan End = new TimeSpan(DateTime.Now.Ticks);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            LoggerCLSViewModel _loggerCLS = new LoggerCLSViewModel
            {
                ExecuteTime = End - _start,
                MethodName = context.Request.Method,
                QueryParams = context.Request.QueryString.ToString(),
                RequestUrl = context.Request.Path,
                Response = text,
                ServiceName = context.Request.Path,
                StatusCode = context.Response.StatusCode.ToString(),
                uthenticatedUser = context.User?.GetId(),
                Parameters = null,
            };
            CancellationToken cancellationToken = context?.RequestAborted ?? CancellationToken.None;
            await _logsService.LogData_V2(_loggerCLS, cancellationToken);
            await responseBody.CopyToAsync(originalBodyStream);
        }


        // private async Task<LoggerCLSViewModel> LoggerCLSViewModel(HttpContext context, TimeSpan ExecuteTime, string FromBodyRequest, string RequestResponse)
        // {
        //     LoggerCLSViewModel _loggerCLS = new LoggerCLSViewModel
        //     {
        //         ExecuteTime = ExecuteTime,
        //         MethodName = context.Request.Method,
        //         QueryParams = context.Request.QueryString.ToString(),
        //         RequestUrl = context.Request.Path,
        //         Response = RequestResponse,
        //         ServiceName = context.Request.Path,
        //         StatusCode = context.Response.StatusCode.ToString(),
        //         uthenticatedUser = context.User?.GetId(),
        //         Parameters = FromBodyRequest,
        //     };
        //     return await Task.FromResult<LoggerCLSViewModel>(_loggerCLS);
        // }
        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;


            request.EnableBuffering();


            var buffer = new byte[Convert.ToInt32(request.ContentLength)];


            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);


            string text = await new StreamReader(response.Body).ReadToEndAsync();


            response.Body.Seek(0, SeekOrigin.Begin);


            // return $"{response.StatusCode}: {text}";
            return text;
        }
    }
}