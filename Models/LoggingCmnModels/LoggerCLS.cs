using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.LoggingCmnModels
{
    public class LoggerCLSViewModel
    {
        public string MethodName { get; set; }
        public string ServiceName { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public TimeSpan? ExecuteTime { get; set; }
        public string StatusCode { get; set; }
        public string RequestUrl { get; set; }
        public string QueryParams { get; set; }
        public string Payload { get; set; }
        public string Response { get; set; }
        public string uthenticatedUser { get; set; }
        public string Parameters { get; set; }
    }
}
