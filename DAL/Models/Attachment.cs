using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Attachment
    {
        public Attachment()
        {
            PersonAttachments = new HashSet<PersonAttachment>();
            PolicyRequestAttachments = new HashSet<PolicyRequestAttachment>();
            PolicyRequestCommentAttachments = new HashSet<PolicyRequestCommentAttachment>();
        }

        public long Id { get; set; }
        public Guid Code { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public byte[] Data { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<PersonAttachment> PersonAttachments { get; set; }
        public virtual ICollection<PolicyRequestAttachment> PolicyRequestAttachments { get; set; }
        public virtual ICollection<PolicyRequestCommentAttachment> PolicyRequestCommentAttachments { get; set; }
    }
}
