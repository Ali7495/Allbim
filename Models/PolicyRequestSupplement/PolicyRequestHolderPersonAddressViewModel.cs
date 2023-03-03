using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models.PolicyRequestSupplement
{
    public class PolicyRequestHolderPersonAddressViewModel
    {
        
        [JsonProperty("code")]
        public Guid? Code { get; set; }

        // [JsonProperty("name")] public string Name { get; set; } = null;

        [JsonProperty("city_id")]
        public long CityId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        // [JsonProperty("zoneNumber")]
        // public string ZoneNumber { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }
    }
}
