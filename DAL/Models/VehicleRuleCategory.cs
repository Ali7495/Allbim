using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class VehicleRuleCategory
    {
        public VehicleRuleCategory()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
