using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InsuredCompany
    {
        public long Id { get; set; }
        public long InsuredId { get; set; }
        public long CompanyId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Company Company { get; set; }
        public virtual Insured Insured { get; set; }
    }
}
