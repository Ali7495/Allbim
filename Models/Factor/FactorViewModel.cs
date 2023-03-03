using Models.PolicyRequest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Factor
{
    public class FactorViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("payment_id")]
        public long? PaymentId { get; set; }
        [JsonProperty("policy_request_id")]
        public long? PolicyRequestId { get; set; }
        [JsonProperty("policy_request")]
        public PolicyRequestViewModel PolicyRequest { get; set; }
        [JsonProperty("payment")]
        public PaymentViewModel Payment { get; set; }
    }
}
