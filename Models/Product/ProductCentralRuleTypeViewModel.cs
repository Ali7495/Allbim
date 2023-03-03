using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Product
{
    public class ProductCentralRuleTypeViewModel
    {
        public long Id { get; set; }
        public long InsuranceFieldId { get; set; }
        public string RuleCaption { get; set; }
        public int Order { get; set; }
        public string Field { get; set; }
        public string RelatedResource { get; set; }
        public byte ResourceTypeId { get; set; }
        public byte PricingTypeId { get; set; }
    }
}
