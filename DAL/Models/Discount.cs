using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Discount
    {
        public long Id { get; set; }
        public long? PersonId { get; set; }
        public long? InsuranceId { get; set; }
        public long? InsurerId { get; set; }
        public int? Value { get; set; }
        public DateTime? ExpirationDateTime { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Insurance Insurance { get; set; }
        public virtual Insurer Insurer { get; set; }
        public virtual Person Person { get; set; }
    }
}
