using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InsuredVehicle
    {
        public long Id { get; set; }
        public long InsuredId { get; set; }
        public long VehicleId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Insured Insured { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
