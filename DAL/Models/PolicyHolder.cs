using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PolicyHolder
    {
        public PolicyHolder()
        {
            PolicyHolderCompanies = new HashSet<PolicyHolderCompany>();
            PolicyHolderPeople = new HashSet<PolicyHolderPerson>();
        }

        public long Id { get; set; }
        public long PolicyId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Policy Policy { get; set; }
        public virtual ICollection<PolicyHolderCompany> PolicyHolderCompanies { get; set; }
        public virtual ICollection<PolicyHolderPerson> PolicyHolderPeople { get; set; }
    }
}
