using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyFactor
{
    public class CompanyFactorViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("policy_request")]
        public CompanyPolicyRequestResultViewModel PolicyRequest { get; set; }
        [JsonProperty("payment")]
        public CompanyPaymentViewModel Payment { get; set; }
        [JsonProperty("policy_request_factor_details")]
        public List<CompanyFactorDetailResultViewModel> PolicyRequestFactorDetails { get; set; }
    }
}
