using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PersonCompany
{
    public class PersonCompanyInputViewModel
    {
        public string Position { get; set; }
    }
    public class PersonCompanyDTOViewModel
    {
        public Guid PersonCode { get; set; }
        public Guid CompanyCode { get; set; }
        public string Position { get; set; }
    }
}
