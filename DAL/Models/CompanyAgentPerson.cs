using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class CompanyAgentPerson
    {
        public long Id { get; set; }
        public long? CompanyAgentId { get; set; }
        public long? PersonId { get; set; }
        public string Position { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual CompanyAgent CompanyAgent { get; set; }
        public virtual Person Person { get; set; }
    }
}
