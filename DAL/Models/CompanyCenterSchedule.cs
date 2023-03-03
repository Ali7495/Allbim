using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class CompanyCenterSchedule
    {
        public CompanyCenterSchedule()
        {
            PolicyRequestInspections = new HashSet<PolicyRequestInspection>();
        }

        public long Id { get; set; }
        public long? CompanyCenterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual CompanyCenter CompanyCenter { get; set; }
        public virtual ICollection<PolicyRequestInspection> PolicyRequestInspections { get; set; }
    }
}
