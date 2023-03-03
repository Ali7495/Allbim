using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Person
{
    public class CompanyForPersonResultViewModel
    {
        [JsonProperty("company_code")]
        public Guid CompanyCode { get; set; }
        [JsonProperty("person_code")]
        public Guid PesonCode { get; set; }
        [JsonProperty("position")]
        public string Position { get; set; }
    }
}
