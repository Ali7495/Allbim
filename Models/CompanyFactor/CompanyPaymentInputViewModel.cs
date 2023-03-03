using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyFactor
{
    public class CompanyPaymentInputViewModel
    {
        [JsonProperty("payment_gateway_id")]
        public long? PaymentGatewayId { get; set; }
        [JsonProperty("payment_code")]
        public string PaymentCode { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
