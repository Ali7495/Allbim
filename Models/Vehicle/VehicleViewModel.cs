using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Vehicle
{
    public class VehicleResultViewModel
    {


        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("vehicle_brand_id"), Required]
        public long VehicleBrandId { get; set; }

        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [Required]
        public string Description { get; set; }
        
        [JsonPropertyName("vehicle_brand")]
        public virtual VehicleBrandResultViewModel VehicleBrand { get; set; }

    } 
    public class VehicleInputViewModel
    {


        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("vehicle_brand_id"), Required]
        public long VehicleBrandId { get; set; }

        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [Required]
        public string Description { get; set; }
        
        [JsonPropertyName("vehicle_brand")]
        public virtual VehicleBrandResultViewModel VehicleBrand { get; set; }

    }
}
