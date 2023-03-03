using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InsuranceFrontTab
    {
        public long Id { get; set; }
        public long InsuranceId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Insurance Insurance { get; set; }
    }
}
