using Models.Agent;
using Models.Person;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyPolicyRequest
{
    public class CompanyPolicyRequestViewModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }
        [JsonProperty("request_person_code")]
        public Guid RequestPersonCode { get; set; }
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
        [JsonProperty("reviewer_code")]
        public Guid? ReviewerCode { get; set; }
        [JsonProperty("agent_selection_type_id")]
        public byte AgentSelectionTypeId { get; set; }
        [JsonProperty("agent_selected_code")]
        public Guid? AgentSelectedCode { get; set; }

        public virtual AgentViewModel AgentSelected { get; set; }
        public virtual CompanyInsurerViewModel Insurer { get; set; }
        public virtual CompanyPolicyStatusViewModel PolicyRequestStatus { get; set; }
        public virtual RequestPersonVeiwModel RequestPerson { get; set; }
        public virtual RequestPersonVeiwModel Reviewer { get; set; }
    }
}
