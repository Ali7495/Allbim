using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PersonAttachment
    {
        public long Id { get; set; }
        public long? PersonId { get; set; }
        public long? AttachmentId { get; set; }
        public int TypeId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Attachment Attachment { get; set; }
        public virtual Person Person { get; set; }
    }
}
