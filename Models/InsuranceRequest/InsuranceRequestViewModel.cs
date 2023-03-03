using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.InsuranceRequest
{
    public class InsuranceRequestViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonProperty("policyRequest_id")]
        [Required]
        public long PolicyRequestId { get; set; }

    }
}
