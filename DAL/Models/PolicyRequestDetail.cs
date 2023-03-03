using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PolicyRequestDetail
    {
        public long Id { get; set; }
        public long PolicyRequestId { get; set; }
        public byte Type { get; set; }
        public string Field { get; set; }
        public string Criteria { get; set; }
        public string Value { get; set; }
        public string Discount { get; set; }
        public string CalculationType { get; set; }
        public string UserInput { get; set; }
        public long? InsurerTermId { get; set; }
        public bool IsCumulative { get; set; }

        public virtual InsurerTerm InsurerTerm { get; set; }
        public virtual PolicyRequest PolicyRequest { get; set; }
    }
}
