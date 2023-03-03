using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PolicyRequestHolder
    {
        public long Id { get; set; }
        public long PolicyRequestId { get; set; }
        public long? PersonId { get; set; }
        public long? CompanyId { get; set; }
        public byte? IssuedPersonType { get; set; }
        public string IssuedPersonRelation { get; set; }
        public long? AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Company Company { get; set; }
        public virtual Person Person { get; set; }
        public virtual PolicyRequest PolicyRequest { get; set; }
    }
}
