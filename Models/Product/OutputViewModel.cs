using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Product
{
    public class OutputViewModel
    {
        public OutputViewModel()
        {
            Cumulatives = new List<CumulativeViewModel>();
        }

        public ProductViewModel Product { get; set; }
        public List<CumulativeViewModel> Cumulatives { get; set; }
    }
}
