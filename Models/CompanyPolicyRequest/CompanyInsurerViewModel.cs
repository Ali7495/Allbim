using Models.Insurer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyPolicyRequest
{
    public class CompanyInsurerViewModel
    {
        [JsonProperty("insurance_id")]
        public long InsuranceId { get; set; }
        [JsonProperty("code")]
        public Guid CompanyCode { get; set; }

        [JsonProperty("company")]
        public virtual InsurerCompanyResultViewModel Company { get; set; }
        [JsonProperty("insurance")]
        public virtual CompanyRequestInsuranceViewModel Insurance { get; set; }
        
    }
}
