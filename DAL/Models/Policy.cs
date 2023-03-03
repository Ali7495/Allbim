using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Policy
    {
        public Policy()
        {
            Insureds = new HashSet<Insured>();
            PolicyDetails = new HashSet<PolicyDetail>();
            PolicyHolders = new HashSet<PolicyHolder>();
            PolicyRequestHolders = new HashSet<PolicyRequestHolder>();
        }

        public long Id { get; set; }
        public Guid Code { get; set; }
        public string Title { get; set; }
        public long InsurerId { get; set; }
        public string PolicyNumber { get; set; }
        public string Description { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Insurer Insurer { get; set; }
        public virtual ICollection<Insured> Insureds { get; set; }
        public virtual ICollection<PolicyDetail> PolicyDetails { get; set; }
        public virtual ICollection<PolicyHolder> PolicyHolders { get; set; }
        public virtual ICollection<PolicyRequestHolder> PolicyRequestHolders { get; set; }
    }
}
