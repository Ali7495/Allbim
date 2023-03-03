using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Person
{
    public class CompanyForPersonViewModel
    {
        [JsonProperty("company_code")]
        public Guid? CompanyCode { get; set; }
        [JsonProperty("position")]
        public string Position { get; set; }
    }
}
