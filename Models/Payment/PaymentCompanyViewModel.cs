using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Payment
{
    public class PaymentCompanyViewModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
    }
}
