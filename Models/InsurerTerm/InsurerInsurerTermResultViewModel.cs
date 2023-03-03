using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.InsurerTerm
{
    public class InsurerInsurerTermResultViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("company")]
        public virtual InsurerResultCompanyViewModel Company { get; set; }
        [JsonProperty("insurance")]
        public virtual InsurerResultInsuranceViewModel Insurance { get; set; }
    }
}
