using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.InsuranceRequest
{
    public class InsuredRequestRelatedPersonViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonProperty("insuredRequest_id")]
        [Required]
        public long InsuredRequestId { get; set; }
        //[JsonProperty("person_id")]
        //[Required]
        //public long PersonId { get; set; }

        [JsonProperty("code")]
        [Required]
        public Guid Code { get; set; }


    }
}
