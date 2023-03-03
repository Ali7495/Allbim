using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Insurance
{
    public class InsuranceDetailsViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        [JsonProperty("faq")]
        public List<InsuranceFAQViewModel> FAQ { get; set; }

        [JsonProperty("companies")]
        public virtual List<InsuranceCompanyViewModel> Companies { get; set; }
    }
}
