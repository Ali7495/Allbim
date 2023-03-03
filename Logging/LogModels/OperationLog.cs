using System;
using System.Collections.Generic;

#nullable disable

namespace Logging.LogModels
{
    public partial class OperationLog
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
}
