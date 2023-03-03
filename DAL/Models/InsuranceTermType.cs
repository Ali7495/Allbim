using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InsuranceTermType
    {
        public InsuranceTermType()
        {
            InsurerTerms = new HashSet<InsurerTerm>();
        }

        public long Id { get; set; }
        public long InsuranceFieldId { get; set; }
        public string TermCaption { get; set; }
        public int Order { get; set; }
        public string Field { get; set; }
        public string RelatedResource { get; set; }
        public byte ResourceTypeId { get; set; }
        public byte PricingTypeId { get; set; }

        public virtual InsuranceField InsuranceField { get; set; }
        public virtual ICollection<InsurerTerm> InsurerTerms { get; set; }
    }
}
