using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InsurerTerm
    {
        public InsurerTerm()
        {
            InsurerTermDetails = new HashSet<InsurerTermDetail>();
            PolicyRequestDetails = new HashSet<PolicyRequestDetail>();
        }

        public long Id { get; set; }
        public long InsurerId { get; set; }
        public string Value { get; set; }
        public string Discount { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsCumulative { get; set; }
        public byte? PricingTypeId { get; set; }
        public byte? ConditionTypeId { get; set; }
        public long? InsuranceTermTypeId { get; set; }
        public byte? CalculationTypeId { get; set; }

        public virtual InsuranceTermType InsuranceTermType { get; set; }
        public virtual Insurer Insurer { get; set; }
        public virtual ICollection<InsurerTermDetail> InsurerTermDetails { get; set; }
        public virtual ICollection<PolicyRequestDetail> PolicyRequestDetails { get; set; }
    }
}
