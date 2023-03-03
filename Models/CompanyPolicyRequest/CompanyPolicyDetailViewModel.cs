using Models.InsurerTerm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyPolicyRequest
{
    public class CompanyPolicyDetailViewModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }
        [JsonProperty("type")]
        public byte Type { get; set; }
        [JsonProperty("field")]
        public string Field { get; set; }
        [JsonProperty("criteria")]
        public string Criteria { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("discount")]
        public string Discount { get; set; }
        [JsonProperty("calculation_type")]
        public string CalculationType { get; set; }
        [JsonProperty("user_input")]
        public string UserInput { get; set; }
        [JsonProperty("insurer_term_id")]
        public long? InsurerTermId { get; set; }
        [JsonProperty("is_cumulative")]
        public bool IsCumulative { get; set; }

        [JsonProperty("insurer_term")]
        public virtual InsurerTermResultViewModel InsurerTerm { get; set; }
        [JsonProperty("policy_request")]
        public virtual CompanyPolicyRequestViewModel PolicyRequest { get; set; }
    }
}
