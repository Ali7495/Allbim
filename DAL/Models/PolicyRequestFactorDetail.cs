using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PolicyRequestFactorDetail
    {
        public long Id { get; set; }
        public long? PolicyRequestFactorId { get; set; }
        public decimal Amount { get; set; }
        public byte? CalculationTypeId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual PolicyRequestFactor PolicyRequestFactor { get; set; }
    }
}
