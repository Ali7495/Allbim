using Models.Person;
using Models.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Agent
{
    public class CopmanyAgentAndPersonViewModel
    {
        
        [JsonProperty("city_id")]
        public long CityId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }


        [JsonProperty("person")]
        public AgentPersonInputViewModel Person { get; set; }
        [JsonProperty("user")]
        public AgentUserForUpdateViewModel User { get; set; }
    }
}
