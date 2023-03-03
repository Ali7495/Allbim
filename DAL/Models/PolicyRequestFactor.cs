using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PolicyRequestFactor
    {
        public PolicyRequestFactor()
        {
            PolicyRequestFactorDetails = new HashSet<PolicyRequestFactorDetail>();
        }

        public long Id { get; set; }
        public long? PaymentId { get; set; }
        public long? PolicyRequestId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual PolicyRequest PolicyRequest { get; set; }
        public virtual ICollection<PolicyRequestFactorDetail> PolicyRequestFactorDetails { get; set; }
    }
}
