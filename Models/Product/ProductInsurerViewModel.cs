using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Product
{
    public class ProductInsurerViewModel
    {
        public long Id { get; set; }
        public long InsuranceId { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string AvatarUrl { get; set; }
    }
}
