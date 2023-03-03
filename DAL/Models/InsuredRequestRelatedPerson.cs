using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InsuredRequestRelatedPerson
    {
        public long Id { get; set; }
        public long InsuredRequestId { get; set; }
        public int RelationTypeId { get; set; }
        public long PersonId { get; set; }

        public virtual InsuredRequest InsuredRequest { get; set; }
        public virtual Person Person { get; set; }
    }
}
