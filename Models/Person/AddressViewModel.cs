using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using Models.City;
using Models.CompanyCenter;

namespace Models.Person
{
    public class AddressViewModel
    {
        
        [JsonProperty("code")]
        public Guid? Code { get; set; }

        [JsonProperty("name")] public string Name { get; set; } = null;

        [JsonProperty("city_id")]
        public long CityId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("zoneNumber")]
        public string ZoneNumber { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }
        
        [JsonProperty("City")]
        public virtual CenterCityViewModel City { get; set; }
    }
    public class AddressInputViewModel
    {
        [JsonProperty("name")] 
        public string Name { get; set; } = null;

        [JsonProperty("city_id")]
        public long CityId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("zoneNumber")]
        public string ZoneNumber { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("address_type_id")]
        public long AddressTypeId { get; set; }

    }
}
