using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InsuredRequestVehicle
    {
        public InsuredRequestVehicle()
        {
            InsuredRequestVehicleDetails = new HashSet<InsuredRequestVehicleDetail>();
        }

        public long Id { get; set; }
        public long InsuredRequestId { get; set; }
        public long? OwnerPersonId { get; set; }
        public long? OwnerCompanyId { get; set; }
        public long VehicleId { get; set; }

        public virtual InsuredRequest InsuredRequest { get; set; }
        public virtual Company OwnerCompany { get; set; }
        public virtual Person OwnerPerson { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<InsuredRequestVehicleDetail> InsuredRequestVehicleDetails { get; set; }
    }
}
