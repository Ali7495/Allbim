using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyAgent
{
    public class CompanyAgentCityViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("township_id")]
        public long TownShipId { get; set; }
        [JsonProperty("township")]
        public virtual CompanyAgentTownShipViewModel TownShip { get; set; }
    }
}
