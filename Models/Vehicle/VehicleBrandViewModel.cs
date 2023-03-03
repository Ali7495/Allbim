using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Vehicle
{
    public class VehicleBrandResultViewModel
    {


        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("vehicle_type_id"), Required]
        public long VehicleTypeId { get; set; }

        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [Required]
        public string Description { get; set; }
        
    }
    public class VehicleBrandInputViewModel
    {


        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("vehicle_type_id"), Required]
        public long VehicleTypeId { get; set; }

        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [Required]
        public string Description { get; set; }
        
    }
}
