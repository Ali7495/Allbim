using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PaymentGatewayDetail
    {
        public long Id { get; set; }
        public long PaymentGatewayId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }

        public virtual PaymentGateway PaymentGateway { get; set; }
    }
}
