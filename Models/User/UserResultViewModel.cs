using Models.Person;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.User
{
    public class UserResultViewModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }

        [JsonProperty("person_code")]
        public Guid PersonCode { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }


        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("person")]
        public virtual UserPersonViewModel Person { get; set; }
        [JsonProperty("role")]
        public virtual PersonRoleResultViewModel Role { get; set; }
    }
}
