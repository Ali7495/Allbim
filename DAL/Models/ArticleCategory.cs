using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ArticleCategory
    {
        public long Id { get; set; }
        public long? ArticleId { get; set; }
        public long? CategoryId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Article Article { get; set; }
        public virtual Category Category { get; set; }
    }
}
