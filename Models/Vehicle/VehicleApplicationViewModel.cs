using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Vehicle
{
    public class VehicleApplicationResultViewModel
    {


        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("vehicleType_id")]
        public long VehicleTypeId { get; set; }
        [JsonPropertyName("name")]

        public string Name { get; set; }


    }
    public class VehicleApplicationInputViewModel
    {
        

        [JsonPropertyName("vehicleType_id")]
        public long VehicleTypeId { get; set; }
        [JsonPropertyName("name")]

        public string Name { get; set; }


    }
}
