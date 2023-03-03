using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class CompanyCenter
    {
        public CompanyCenter()
        {
            CompanyCenterSchedules = new HashSet<CompanyCenterSchedule>();
        }

        public long Id { get; set; }
        public long? CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<CompanyCenterSchedule> CompanyCenterSchedules { get; set; }
    }
}
