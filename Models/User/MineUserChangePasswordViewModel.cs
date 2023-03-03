using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.User
{
    public class MineUserChangePasswordViewModel
    {
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("confirm_password")]
        public string ConfirmPassword { get; set; }
        [JsonProperty("old_password")]
        public string OldPassword { get; set; }
    }
}
