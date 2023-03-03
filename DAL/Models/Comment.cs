using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Comment
    {
        public Comment()
        {
            InverseParent = new HashSet<Comment>();
        }

        public long Id { get; set; }
        public long? AuthorId { get; set; }
        public long ArticleId { get; set; }
        public long? ParentId { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }
        public bool IsApproved { get; set; }
        public int? Score { get; set; }

        public virtual Article Article { get; set; }
        public virtual Person Author { get; set; }
        public virtual Comment Parent { get; set; }
        public virtual ICollection<Comment> InverseParent { get; set; }
    }
}
