using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PaymentGateway
{
    public class PaymentGatewayDetailResultViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("payment_gateway_id")]
        public long PaymentGatewayId { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
