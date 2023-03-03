using Models.Insurance;
using Models.Person;
using System;
using System.Collections.Generic;
using Models.Company;

namespace Models.PolicyRequest
{
    public class PolicyRequestStatusViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        // public virtual ICollection<PolicyRequestViewModel> PolicyRequests { get; set; }

    }

}
