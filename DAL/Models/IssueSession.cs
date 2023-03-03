using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class IssueSession
    {
        public IssueSession()
        {
            PolicyRequestIssues = new HashSet<PolicyRequestIssue>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<PolicyRequestIssue> PolicyRequestIssues { get; set; }
    }
}
