using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BodySupplementInfo
{
    public class BodySupplementAddressViewModel
    {
        [JsonProperty("address_code")]
        public Guid Code { get; set; }
        [JsonProperty("city_id")]
        public long CityId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("mobile")]
        public string Mobile { get; set; }



    }
}
