using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ContactU
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public long TrackingNumber { get; set; }
    }
}
