using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ReminderPeriod
    {
        public ReminderPeriod()
        {
            Reminders = new HashSet<Reminder>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }

        public virtual ICollection<Reminder> Reminders { get; set; }
    }
}
