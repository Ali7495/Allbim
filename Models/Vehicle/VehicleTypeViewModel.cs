using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Vehicle
{
    public class VehicleTypeViewModel
    {

        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        [Required]
        public string Description { get; set; }

    }
}
