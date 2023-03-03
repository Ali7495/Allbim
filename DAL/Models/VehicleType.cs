using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            VehicleApplications = new HashSet<VehicleApplication>();
            VehicleBrands = new HashSet<VehicleBrand>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<VehicleApplication> VehicleApplications { get; set; }
        public virtual ICollection<VehicleBrand> VehicleBrands { get; set; }
    }
}
