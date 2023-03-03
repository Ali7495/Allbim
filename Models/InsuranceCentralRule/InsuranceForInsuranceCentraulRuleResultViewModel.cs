using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.InsuranceCentralRule
{
    public class InsuranceForInsuranceCentraulRuleResultViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        //[JsonProperty("description")]
        //public string Description { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }

        //[JsonProperty("summery")]
        //public string Summary { get; set; }
    }
}
