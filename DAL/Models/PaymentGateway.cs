using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PaymentGateway
    {
        public PaymentGateway()
        {
            PaymentGatewayDetails = new HashSet<PaymentGatewayDetail>();
            Payments = new HashSet<Payment>();
        }

        public long Id { get; set; }
        public string TerminalId { get; set; }
        public bool IsDeleted { get; set; }
        public bool? AllowOnline { get; set; }
        public bool? AllowManual { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PaymentGatewayDetail> PaymentGatewayDetails { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
