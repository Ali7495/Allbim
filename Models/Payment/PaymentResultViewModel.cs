using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Payment
{
    public class PaymentResultViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("payment_status")]
        public string PaymentSatus { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("payment_code")]
        public string PaymentCode { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("payment_gateway_id")]
        public long? PaymentGatewayId { get; set; }
    }
}
