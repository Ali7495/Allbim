using Models.Company;
using Models.Person;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Agent
{
    public class AgentViewModel
    {
        
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("city_id")]
        public long CityId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("person")]
        public virtual AgentPersonViewModel Person { get; set; }
        [JsonProperty("company")]
        public virtual AgentCompanyViewModel Company { get; set; }
        [JsonProperty("city")]
        public virtual AgentCityViewModel City { get; set; }
    }
}
