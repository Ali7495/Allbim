using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Person
{
    public class AddressForPersonViewModel
    {

        [JsonProperty("city_id")]
        public long? CityId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
