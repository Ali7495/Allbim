using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PolicyRequestInspection
{
    public class PolicyRequestAddressViewModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("city_id")]
        public long CityId { get; set; }
        [JsonProperty("mobile")]
        public string Mobile { get; set; }
    }
}
