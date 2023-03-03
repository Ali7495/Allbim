using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class OnlinePayment
    {
        public long Id { get; set; }
        public long PaymentId { get; set; }
        public bool PaymentSettle { get; set; }
        public bool PaymentVerify { get; set; }
        public string RefId { get; set; }
        public long? SaleOrderId { get; set; }
        public string SaleReferenceId { get; set; }
        public DateTime? SettleDate { get; set; }
        public DateTime? VerifyDate { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
