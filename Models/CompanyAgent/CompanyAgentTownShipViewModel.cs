using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyAgent
{
    public class CompanyAgentTownShipViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("province_id")]
        public long ProvinceId { get; set; }
        [JsonProperty("province")]
        public virtual CompanyAgentProvinceViewModel Province { get; set; }
    }
}
