using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.InsuranceRequest
{
    public class InsuranceRequestPlaceViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("insured_id")]
        [Required]
        public long InsuredId { get; set; }

        [JsonPropertyName("place_id")]
        [Required]
        public long PlaceId { get; set; }
    }
}
