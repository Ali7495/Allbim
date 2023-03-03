using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InspectionSession
    {
        public InspectionSession()
        {
            PolicyRequestInspections = new HashSet<PolicyRequestInspection>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<PolicyRequestInspection> PolicyRequestInspections { get; set; }
    }
}
