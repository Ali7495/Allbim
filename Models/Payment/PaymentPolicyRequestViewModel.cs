using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Payment
{
    public class PaymentPolicyRequestViewModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }
        [JsonProperty("request_person_code")]
        public Guid RequestPersonCode { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("company_code")]
        public Guid CompanyCode { get; set; }
        [JsonProperty("policy_number")]
        public string PolicyNumber { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("company")]
        public PaymentCompanyViewModel Company { get; set; }
    }
}
