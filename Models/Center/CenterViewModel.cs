using Models.City;
using System.Text.Json.Serialization;

namespace Models.Center
{
    public class CenterResultViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("city_Id")]
        public long? CityId { get; set; }

        // public virtual CityResultViewModel City { get; set; }

    }
    public class CenterInputViewModel
    {
      
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("city_Id")]
        public long? CityId { get; set; }

        // public virtual CityResultViewModel City { get; set; }

    }

}
