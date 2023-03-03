using Models.CompanyFactor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PolicyRequestFactor
{
    public class PolicyFactorResultViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("payment")]
        public CompanyPaymentResultViewModel Payment { get; set; }
        [JsonProperty("policy_request_factor_details")]
        public List<CompanyFactorDetailResultViewModel> PolicyRequestFactorDetails { get; set; }
    }
}
