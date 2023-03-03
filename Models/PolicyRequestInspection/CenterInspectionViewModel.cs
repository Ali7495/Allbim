using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PolicyRequestInspection
{
    public class CenterInspectionViewModel
    {
        [JsonProperty("inspection_session_date")]
        public string InspectionSessionDate { get; set; }

        [JsonProperty("company_center_schedule_id")]
        public long? CompanyCenterScheduleId { get; set; }
    }
}
