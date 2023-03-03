using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class CentralRuleType
    {
        public CentralRuleType()
        {
            InsuranceCentralRules = new HashSet<InsuranceCentralRule>();
        }

        public long Id { get; set; }
        public long InsuranceFieldId { get; set; }
        public string RuleCaption { get; set; }
        public int Order { get; set; }
        public string Field { get; set; }
        public string RelatedResource { get; set; }
        public byte ResourceTypeId { get; set; }
        public byte PricingTypeId { get; set; }

        public virtual InsuranceField InsuranceField { get; set; }
        public virtual ICollection<InsuranceCentralRule> InsuranceCentralRules { get; set; }
    }
}
