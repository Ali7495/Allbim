using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Category
    {
        public Category()
        {
            ArticleCategories = new HashSet<ArticleCategory>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
    }
}
