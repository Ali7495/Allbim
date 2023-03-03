using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Product
{
    public class ProductInsurerTermViewModel
    {
        public long Id { get; set; }
        public long InsurerId { get; set; }
        public string Value { get; set; }
        public string Discount { get; set; }
        public byte? CalculationTypeId { get; set; }
        public bool IsCumulative { get; set; }
        public byte? PricingTypeId { get; set; }
        public byte? ConditionTypeId { get; set; }
        public long? InsuranceTermTypeId { get; set; }

        public virtual ProductInsuranceTermTypeViewModel InsuranceTermType { get; set; }
        public virtual List<ProductInsurerDetailViewModel> InsurerTermDetails { get; set; }
    }
}
