using Models.City;
using Models.Insurance;
using System;
using System.Text.Json.Serialization;

namespace Models.Reminder
{
    public class ReminderInputViewModel
    {
        [JsonPropertyName("insurance_id")]

        public long? InsuranceId { get; set; }
        [JsonPropertyName("reminderPeriod_id")]

        public long? ReminderPeriodId { get; set; }
        [JsonPropertyName("description")]

        public string Description { get; set; }
        [JsonPropertyName("dueDate")]

        public DateTime? DueDate { get; set; }
        [JsonPropertyName("city_id")]

        public long? CityId { get; set; }

    }
    public class ReminderResultViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("insurance_id")]

        public long? InsuranceId { get; set; }
        [JsonPropertyName("reminderPeriod_id")]

        public long? ReminderPeriodId { get; set; }
        [JsonPropertyName("description")]

        public string Description { get; set; }
        [JsonPropertyName("dueDate")]

        public DateTime? DueDate { get; set; }
        [JsonPropertyName("city_id")]

        public long? CityId { get; set; }

        public virtual CityResultViewModel City { get; set; }
        public virtual ReminderPeriodResultViewModel ReminderPeriod { get; set; }
        public virtual InsuranceViewModel Insurance { get; set; }
        
    }

}
