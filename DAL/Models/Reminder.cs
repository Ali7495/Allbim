using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Reminder
    {
        public long Id { get; set; }
        public long? InsuranceId { get; set; }
        public long? ReminderPeriodId { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public long? CityId { get; set; }
        public long? PersonId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual City City { get; set; }
        public virtual Insurance Insurance { get; set; }
        public virtual Person Person { get; set; }
        public virtual ReminderPeriod ReminderPeriod { get; set; }
    }
}
