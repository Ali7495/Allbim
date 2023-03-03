using Models.Person;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyUser
{
    public class UpdatedUserResultViewModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("role")]
        public virtual PersonRoleResultViewModel Role { get; set; }
    }
}
