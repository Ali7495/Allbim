using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PolicyRequestAttachment
    {
        public long Id { get; set; }
        public long PolicyRequestId { get; set; }
        public long AttachmentId { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }

        public virtual Attachment Attachment { get; set; }
        public virtual PolicyRequest PolicyRequest { get; set; }
    }
}
