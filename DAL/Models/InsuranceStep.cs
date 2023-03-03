using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InsuranceStep
    {
        public long Id { get; set; }
        public long? InsuranceId { get; set; }
        public string StepName { get; set; }
        public int StepOrder { get; set; }

        public virtual Insurance Insurance { get; set; }
    }
}
