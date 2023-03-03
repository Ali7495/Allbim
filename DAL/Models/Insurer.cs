using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Insurer
    {
        public Insurer()
        {
            Discounts = new HashSet<Discount>();
            InsurerTerms = new HashSet<InsurerTerm>();
            PolicyRequests = new HashSet<PolicyRequest>();
        }

        public long Id { get; set; }
        public long InsuranceId { get; set; }
        public long CompanyId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? ArticleId { get; set; }

        public virtual Article Article { get; set; }
        public virtual Company Company { get; set; }
        public virtual Insurance Insurance { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<InsurerTerm> InsurerTerms { get; set; }
        public virtual ICollection<PolicyRequest> PolicyRequests { get; set; }
    }
}
