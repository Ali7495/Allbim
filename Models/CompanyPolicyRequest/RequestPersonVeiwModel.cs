using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyPolicyRequest
{
    public class RequestPersonVeiwModel
    {
        [JsonProperty("code")]
        public Guid? Code { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("national_code")]
        public string NationalCode { get; set; }
    }
}
