using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Place
{
    public class PlaceAddressResultViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonProperty("address_id")]
        [Required]
        public long AddressId { get; set; }
        [JsonProperty("place_id")]
        [Required]
        public long PlaceId { get; set; }

    }    
    public class PlaceAddressInputViewModel
    {

        [JsonProperty("address_id")]
        [Required]
        public long AddressId { get; set; }
        [JsonProperty("place_id")]
        [Required]
        public long PlaceId { get; set; }

    }
    
    
    
}
