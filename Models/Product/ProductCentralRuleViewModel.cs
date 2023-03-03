using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Product
{
    public class ProductCentralRuleViewModel
    {
        public long Id { get; set; }
        public string JalaliYear { get; set; }
        public string GregorianYear { get; set; }
        public string FieldType { get; set; }
        public string Value { get; set; }
        public bool IsCumulative { get; set; }
        public long? CentralRuleTypeId { get; set; }
        public string Discount { get; set; }
        public byte? CalculationTypeId { get; set; }
        public byte? PricingTypeId { get; set; }
        public byte? ConditionTypeId { get; set; }

        public virtual ProductCentralRuleTypeViewModel CentralRuleType { get; set; }
    }
}
