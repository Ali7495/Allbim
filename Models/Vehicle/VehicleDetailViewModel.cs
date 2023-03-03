using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Vehicle
{
    public class VehicleDetailViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("vehicle_id"), Required]
        public long VehicleId { get; set; }

        [JsonPropertyName("name")]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [Required]
        public string Description { get; set; }

        [JsonPropertyName("created_year")]
        [Required]
        public int CreatedYear { get; set; }
    }
}
