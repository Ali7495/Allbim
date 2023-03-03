using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PolicyRequestInspection
{
    public class PolicyRequestCenterScheduleViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("company_center_id")]
        public long? CompanyCenterId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("company_center")]
        public virtual PolicyRequestCenterViewModel CompanyCenter { get; set; }
    }
}
