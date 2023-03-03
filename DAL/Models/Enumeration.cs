using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Enumeration
    {
        public Enumeration()
        {
            InverseParent = new HashSet<Enumeration>();
        }

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCaption { get; set; }
        public int EnumId { get; set; }
        public string EnumCaption { get; set; }
        public byte? Order { get; set; }
        public byte? IsEnable { get; set; }
        public string Description { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Enumeration Parent { get; set; }
        public virtual ICollection<Enumeration> InverseParent { get; set; }
    }
}
