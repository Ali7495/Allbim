using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InsuranceField
    {
        public InsuranceField()
        {
            CentralRuleTypes = new HashSet<CentralRuleType>();
            InsuranceTermTypes = new HashSet<InsuranceTermType>();
        }

        public long Id { get; set; }
        public long InsuranceId { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public long? InsuranceFieldTypeId { get; set; }

        public virtual Insurance Insurance { get; set; }
        public virtual ICollection<CentralRuleType> CentralRuleTypes { get; set; }
        public virtual ICollection<InsuranceTermType> InsuranceTermTypes { get; set; }
    }
}
