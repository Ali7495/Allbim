using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.InsurerTerm
{
    public class InsurerResultCompanyViewModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        //[JsonProperty("description")]
        //public string Description { get; set; }
    }
}
