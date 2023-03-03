using Models.CompanyFactor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Payment
{
    public class PaymentFactorViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("policy_request_factor_details")]
        public List<CompanyFactorDetailResultViewModel> PolicyRequestFactorDetails { get; set; }

        [JsonProperty("policy_request")]
        public PaymentPolicyRequestViewModel PolicyRequest { get; set; }

        [JsonProperty("payment")]
        public PaymentResultWithAllRelationViewModel Payment { get; set; }
    }
}
