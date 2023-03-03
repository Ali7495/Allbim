using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Agent
{
    public class AgentTownShipViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("province_id")]
        public long ProvinceId { get; set; }
        [JsonProperty("province")]
        public virtual AgentProvinceViewModel Province { get; set; }
    }
}
