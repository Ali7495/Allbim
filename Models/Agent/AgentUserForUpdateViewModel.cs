using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Agent
{
    public class AgentUserForUpdateViewModel
    {

        [JsonProperty("email")]
        [Required]
        public string Email { get; set; }
        [JsonProperty("username")]
        [Required]
        public string Username { get; set; }

        [JsonProperty("Password")]
        [Required]
        public string Password { get; set; }
    }
}
