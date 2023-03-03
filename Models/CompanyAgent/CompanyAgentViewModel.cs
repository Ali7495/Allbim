using Models.Agent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyAgent
{
    public class CompanyAgentViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("city_id")]
        public long CityId { get; set; }


        [JsonProperty("person")]
        public virtual CompanyAgentPersonViewModel Person { get; set; }
        [JsonProperty("company")]
        public virtual CompanyOfAgentViewModel Company { get; set; }
        [JsonProperty("city")]
        public virtual CompanyAgentCityViewModel City { get; set; }
    }
}
