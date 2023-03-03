using System;

namespace Models.PolicyRequest
{
    public class PaymentViewModel
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public long PaymentStatusId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string PaymentCode { get; set; }
        public string Description { get; set; }
    }
}
