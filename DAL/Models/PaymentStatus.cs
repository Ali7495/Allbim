using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PaymentStatus
    {
        public PaymentStatus()
        {
            Payments = new HashSet<Payment>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
