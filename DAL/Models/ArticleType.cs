using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ArticleType
    {
        public ArticleType()
        {
            Articles = new HashSet<Article>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
