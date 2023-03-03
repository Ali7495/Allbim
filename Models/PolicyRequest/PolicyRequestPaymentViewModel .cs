using Models.Payment;
using Models.PolicyRequestFactor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PolicyRequest
{
    public class PolicyRequestPaymentViewModel
    {
        [JsonProperty("insurer")]
        public string Insurer { get; set; }
        [JsonProperty("payment_details")]
        public List<PolicyRequestPaymentDetailViewModel> PaymentDetailViewModels { get; set; }

        [JsonProperty("factors")]
        public List<PolicyFactorResultViewModel> Factors { get; set; }

        [JsonProperty("payment_info")]
        public PaymentResultViewModel PaymentInfo { get; set; }

        
    }
}
