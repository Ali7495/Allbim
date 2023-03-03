using Models.Inspection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PolicyRequestInspection
{
    public class PolicyRequestInspectionResultViewModel
    {
        [JsonProperty("code")]
        public Guid? Code { get; set; }

        [JsonProperty("address_code")]
        public Guid? AddressCode { get; set; }

        [JsonProperty("inspection_type_id")]
        public byte InspectionTypeId { get; set; }

        [JsonProperty("inspection_session_date")]
        public string InspectionSessionDate { get; set; }

        [JsonProperty("company_center_schedule_id")]
        public long? CompanyCenterScheduleId { get; set; }

        [JsonProperty("inspection_session_id")]
        public long? InspectionSessionId { get; set; }




        [JsonProperty("company_center_schedule")]
        public virtual PolicyRequestCenterScheduleViewModel CompanyCenterSchedule { get; set; }
        [JsonProperty("inspection_address")]
        public virtual PolicyRequestAddressViewModel InspectionAddress { get; set; }
        [JsonProperty("inspection_session")]
        public virtual InspectionSessionViewModel InspectionSession { get; set; }
        [JsonProperty("policy_request")]
        public virtual PolicyRequestForInspectionViewModel PolicyRequest { get; set; }
    }
}
