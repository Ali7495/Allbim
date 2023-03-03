using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Reminder
{
   public  class ReminderPeriodResultViewModel
    {
        [JsonPropertyName("id")]

        public long Id { get; set; }
        [JsonPropertyName("name")]

        public string Name { get; set; }
        [JsonPropertyName("isDelete")]

        public bool IsDelete { get; set; }
    }  public  class ReminderPeriodInputViewModel
    {
        [JsonPropertyName("name")]

        public string Name { get; set; }
        [JsonPropertyName("isDelete")]

        public bool IsDelete { get; set; }
    }
}
