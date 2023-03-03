using Models.CentralRuleType;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.InsuranceTermType
{
    public class InsuranceFieldViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("insurance_id")]
        public long InsuranceId { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

        //public virtual List<InsuranceTermTypeViewModel> InsuranceTermTypes { get; set; }
        //public virtual List<CentralRuleTypeViewModel> CentralRuleTypes { get; set; }
    }
}
