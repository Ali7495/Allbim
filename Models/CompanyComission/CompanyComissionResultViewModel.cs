using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyComission
{
    public class CompanyComissionResultViewModel
    {
        [JsonProperty("policy_code")]
        public Guid PolicyCode { get; set; }
    }
}
