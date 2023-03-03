using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Insurer
{
    public class InsurerCompanyResultViewModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
    }
}
