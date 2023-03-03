using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InsuredRequestCompany
    {
        public long Id { get; set; }
        public long InsuredRequestId { get; set; }
        public long CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual InsuredRequest InsuredRequest { get; set; }
    }
}
