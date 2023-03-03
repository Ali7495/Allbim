using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            InsuredRequestVehicles = new HashSet<InsuredRequestVehicle>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long VehicleBrandId { get; set; }
        public long VehicleRuleCategoryId { get; set; }

        public virtual VehicleBrand VehicleBrand { get; set; }
        public virtual VehicleRuleCategory VehicleRuleCategory { get; set; }
        public virtual ICollection<InsuredRequestVehicle> InsuredRequestVehicles { get; set; }
    }
}
