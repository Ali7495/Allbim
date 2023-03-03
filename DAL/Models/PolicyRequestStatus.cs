using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PolicyRequestStatus
    {
        public PolicyRequestStatus()
        {
            PolicyRequests = new HashSet<PolicyRequest>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<PolicyRequest> PolicyRequests { get; set; }
    }
}
