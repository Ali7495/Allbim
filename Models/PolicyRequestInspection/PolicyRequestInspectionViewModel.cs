using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.PolicyRequestInspection
{
    public class PolicyRequestInspectionViewModel
    {
        [JsonPropertyName("code")]
        public Guid? Code { get; set; }

        [JsonPropertyName("address_code")]
        public Guid? AddressCode { get; set; }

        [JsonPropertyName("inspection_type_id")]
        public byte? InspectionTypeId { get; set; }

        [JsonPropertyName("inspection_session_date")]
        public string InspectionSessionDate { get; set; }

        [JsonPropertyName("inspection_session")]
        public string InspectionSession { get; set; }

        [JsonPropertyName("inspection_session_week_day")]
        public string InspectionSessionWeekDay { get; set; }
    }
}
