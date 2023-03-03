using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Place
{
    public class PlaceResultViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }
        [JsonProperty("description")]

        public string Description { get; set; }

    }
    public class PlaceInputViewModel
    {

        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }
        [JsonProperty("description")]

        public string Description { get; set; }

    }
    
    
    
}
