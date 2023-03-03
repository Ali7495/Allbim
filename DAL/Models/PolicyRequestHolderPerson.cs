using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PolicyRequestHolderPerson
    {
        public long Id { get; set; }
        public long PolicyRequestHolderId { get; set; }
        public long PersonId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Person Person { get; set; }
        public virtual PolicyRequestHolder PolicyRequestHolder { get; set; }
    }
}
