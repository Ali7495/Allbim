using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyFactor
{
    public class CompanyPolicyFactorInputViewModel
    {
        [JsonProperty("payment")]
        public virtual CompanyPaymentInputViewModel Payment { get; set; }
        [JsonProperty("policy_request_factor_detail")]
        public virtual List<CompanyFactorDetailInputViewModel> PolicyRequestFactorDetails { get; set; }
    }
}
