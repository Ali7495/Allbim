using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PolicyRequestInspection
{
    public class PolicyRequestForInspectionViewModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }
        [JsonProperty("person_code")]
        public Guid PersonCode { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("insurer_id")]
        public long InsurerId { get; set; }
        [JsonProperty("policy_number")]
        public string PolicyNumber { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("policy_request_status_id")]
        public long? PolicyRequestStatusId { get; set; }
    }
}
