using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class CompanyAgent
    {
        public CompanyAgent()
        {
            CompanyAgentPeople = new HashSet<CompanyAgentPerson>();
            PolicyRequests = new HashSet<PolicyRequest>();
        }

        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long PersonId { get; set; }
        public long CityId { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual City City { get; set; }
        public virtual Company Company { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<CompanyAgentPerson> CompanyAgentPeople { get; set; }
        public virtual ICollection<PolicyRequest> PolicyRequests { get; set; }
    }
}
