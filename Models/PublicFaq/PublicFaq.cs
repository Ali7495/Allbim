using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PublicFaq
{
    public class PublicFaqInputViewModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
    public class PublicFaqResultViewModel
    {
        public long Id{ get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
