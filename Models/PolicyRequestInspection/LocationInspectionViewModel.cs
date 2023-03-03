using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PolicyRequestInspection
{
    public class LocationInspectionViewModel
    {
        [JsonProperty("address_code")]
        public Guid? AddressCode { get; set; }

        [JsonProperty("inspection_session_date")]
        public string InspectionSessionDate { get; set; }

        [JsonProperty("inspection_session_id")]
        public long? InspectionSessionId { get; set; }
    }
}
