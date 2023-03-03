using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PolicyRequestInspection
{
    public class PolicyRequestInspectionInputViewModel
    {
        [JsonProperty("inspection_type_id")]
        public byte InspectionTypeId { get; set; }

        [JsonProperty("center_inspection")]
        public CenterInspectionViewModel CenterInspection { get; set; }
        [JsonProperty("location_inspection")]
        public LocationInspectionViewModel LocationInspection { get; set; }
    }
}
