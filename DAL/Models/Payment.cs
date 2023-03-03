using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Payment
    {
        public Payment()
        {
            OnlinePayments = new HashSet<OnlinePayment>();
            PolicyRequestFactors = new HashSet<PolicyRequestFactor>();
        }

        public long Id { get; set; }
        public decimal Price { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string PaymentCode { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public long PaymentStatusId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public long? PaymentGatewayId { get; set; }

        public virtual PaymentGateway PaymentGateway { get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; }
        public virtual ICollection<OnlinePayment> OnlinePayments { get; set; }
        public virtual ICollection<PolicyRequestFactor> PolicyRequestFactors { get; set; }
    }
}
