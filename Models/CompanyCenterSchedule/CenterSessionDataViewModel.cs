using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyCenterSchedule
{
    public class CenterSessionDataViewModel
    {
        [JsonProperty("jalali_date")]
        public string JalaliDate { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("day_name")]
        public string DayName { get; set; }

        [JsonProperty("center_schedules")]
        public List<CenterScheduleViewModel> CenterSchedules { get; set; }
    }
}
