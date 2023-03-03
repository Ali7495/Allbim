using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PolicyHolderPerson
    {
        public long Id { get; set; }
        public long PolicyHolderId { get; set; }
        public long PersonId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Person Person { get; set; }
        public virtual PolicyHolder PolicyHolder { get; set; }
    }
}
