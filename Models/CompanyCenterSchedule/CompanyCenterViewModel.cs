using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyCenterSchedule
{
    public class CompanyCenterViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("company_code")]
        public Guid Code { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("city_id")]
        public long? CityId { get; set; }

        [JsonProperty("center_session_data")]
        public List<CenterSessionDataViewModel> CenterSessionData { get; set; }
    }
}
