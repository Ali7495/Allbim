using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Inspection
{
    public class InspectionDateAndTimeViewModel
    {
        [JsonProperty("date_with_time")]
        public string DateWithTime { get; set; }


        [JsonProperty("jalali_with_time")]
        public string JalaliWithTime { get; set; }

    }
}
