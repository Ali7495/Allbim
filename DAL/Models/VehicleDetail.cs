using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class VehicleDetail
    {
        public long Id { get; set; }
        public long VehicleId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int CreatedYear { get; set; }
        public string Description { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
