using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class VehicleBrand
    {
        public VehicleBrand()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long VehicleTypeId { get; set; }

        public virtual VehicleType VehicleType { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
