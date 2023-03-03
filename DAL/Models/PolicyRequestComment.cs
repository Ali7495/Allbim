using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PolicyRequestComment
    {
        public PolicyRequestComment()
        {
            PolicyRequestCommentAttachments = new HashSet<PolicyRequestCommentAttachment>();
        }

        public long Id { get; set; }
        public long PolicyRequestId { get; set; }
        public string Description { get; set; }
        public long AuthorId { get; set; }
        public byte? AuthorTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDateTime { get; set; }

        public virtual Person Author { get; set; }
        public virtual PolicyRequest PolicyRequest { get; set; }
        public virtual ICollection<PolicyRequestCommentAttachment> PolicyRequestCommentAttachments { get; set; }
    }
}
