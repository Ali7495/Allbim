using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PolicyRequestCommentAttachment
    {
        public long Id { get; set; }
        public long? PolicyRequestCommentId { get; set; }
        public long? AttachmentId { get; set; }
        public int? AttachmentTypeId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Attachment Attachment { get; set; }
        public virtual PolicyRequestComment PolicyRequestComment { get; set; }
    }
}
