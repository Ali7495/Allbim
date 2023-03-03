using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PaymentGateway
{
    public class GetewayResultViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("terminal_id")]
        public string TerminalId { get; set; }
        [JsonProperty("allow_online")]
        public bool? AllowOnline { get; set; }
        [JsonProperty("allow_manual")]
        public bool? AllowManual { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        public virtual List<PaymentGatewayDetailResultViewModel> PaymentGatewayDetails { get; set; }
    }
}
