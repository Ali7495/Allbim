using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Product
{
    public class ProductInsuranceViewModel
    {
        public virtual ProductInsurerViewModel Insurer { get; set; }
        public virtual List<ProductInsurerTermViewModel> InsurerTerms { get; set; }
        public virtual List<ProductCentralRuleViewModel> InsuranceCentralRules { get; set; }
    }
}
