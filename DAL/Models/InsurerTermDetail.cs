using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InsurerTermDetail
    {
        public long Id { get; set; }
        public long InsurerTermId { get; set; }
        public int? Order { get; set; }
        public long? ParentId { get; set; }
        public string Field { get; set; }
        public string Criteria { get; set; }
        public string Value { get; set; }
        public string Discount { get; set; }
        public string CalculationType { get; set; }
        public bool? IsCumulative { get; set; }
        public byte? PricingTypeId { get; set; }

        public virtual InsurerTerm InsurerTerm { get; set; }
    }
}
