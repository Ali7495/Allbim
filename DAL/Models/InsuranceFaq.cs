using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InsuranceFaq
    {
        public long Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool IsDeleted { get; set; }
        public long InsuranceId { get; set; }

        public virtual Insurance Insurance { get; set; }
    }
}
