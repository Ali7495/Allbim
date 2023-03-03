using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyAgent
{
    public class CompanyAgentUserViewModel
    {
        [JsonProperty("person_code")]
        [Required]
        public Guid PersonCode { get; set; }

        [JsonProperty("code")]
        public Guid Code { get; set; }

        [JsonProperty("email")]
        [Required]
        public string Email { get; set; }
        [JsonProperty("username")]
        [Required]
        public string Username { get; set; }
    }
}
