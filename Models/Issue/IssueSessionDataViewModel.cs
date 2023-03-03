using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Issue
{
    public class IssueSessionDataViewModel
    {
        [JsonProperty("jalali_date")]
        public string JalaliDate { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("day_name")]
        public string DayName { get; set; }

        [JsonProperty("sessions")]
        public List<IssueSessionsViewModel> IssueSessions { get; set; }
    }
}
