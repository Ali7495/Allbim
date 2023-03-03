using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Agent
{
    public class AgentCityViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("township_id")]
        public long TownShipId { get; set; }
        [JsonProperty("township")]
        public virtual AgentTownShipViewModel TownShip { get; set; }
        [JsonProperty("company_agents")]
        public virtual List<AgentViewModel> CompanyAgents { get; set; }
    }
}
