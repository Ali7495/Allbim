using System;
using System.Collections.Generic;

#nullable disable

namespace Logging.LogModels
{
    public partial class SystemErrorLog
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
