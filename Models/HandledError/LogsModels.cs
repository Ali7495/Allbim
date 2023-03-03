using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.LogsModels
{
    public class HandledErrorLogOutputViewModel
    {
        public long Id { get; set; }
        public string ServiceName { get; set; }
        public string MethodName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string RequestUrl { get; set; }
        public string RequestQueryParams { get; set; }
        public string RequestPayload { get; set; }
        public long? AuthenticatedUser { get; set; }
    }
    public class OprationLogOutputViewModel
    {
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string MethodName { get; set; }
        public string ServiceName { get; set; }
        public string FormBodyParameters { get; set; }
        public string ExecuteTime { get; set; }
        public string StatusCode { get; set; }
        public string RequestUrl { get; set; }
        public string QueryStringParameters { get; set; }
        public string Response { get; set; }
        public string AuthenticatedUser { get; set; }
    }
    public class SystemErrorLogOutputViewModel
    {
        public long Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ServiceName { get; set; }
        public string RequestUrl { get; set; }
        public string RequestQueryParams { get; set; }
        public string RequestPayload { get; set; }
        public string ExceptionStr { get; set; }
        public long? AuthenticatedUser { get; set; }
    }
}
