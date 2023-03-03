using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.InsuranceRequest
{
    public class InsuranceRequestPersonViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("insured_id")]
        [Required]
        public long InsuredId { get; set; }

        [JsonPropertyName("code")]
        [Required]
        public Guid Code { get; set; }

        //[JsonPropertyName("person_id")]
        //[Required]
        //public long PersonId { get; set; }
    }
}
