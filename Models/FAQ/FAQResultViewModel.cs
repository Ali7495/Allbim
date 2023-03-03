using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.FAQ
{
    public class FAQResultViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("question")]
        public string Question { get; set; }
        [JsonProperty("answer")]
        public string Answer { get; set; }

        [JsonProperty("insurance")]
        public virtual FAQInsuranceResultViewModel Insurance { get; set; }
    }
}
