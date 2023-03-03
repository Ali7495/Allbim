using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Inspection
{
    public class InspectionDataViewModel
    {
        [JsonProperty("jalali_date")]
        public string JalaliDate { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("day_name")]
        public string DayName { get; set; }

        [JsonProperty("sessions")]
        public List<InspectionSessionViewModel> InspectionSessions { get; set; }

        [JsonProperty("dates_and_times")]
        public List<InspectionDateAndTimeViewModel> DatesAndTimes { get; set; }
    }
}
