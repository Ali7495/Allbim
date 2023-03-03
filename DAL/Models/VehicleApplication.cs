using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class VehicleApplication
    {
        public long Id { get; set; }
        public long VehicleTypeId { get; set; }
        public string Name { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}
