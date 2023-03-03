using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Person
{
    public class PersonUserResultViewModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }
        [JsonProperty("person_code")]
        public Guid PersonCode { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
