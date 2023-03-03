using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Article
    {
        public Article()
        {
            ArticleCategories = new HashSet<ArticleCategory>();
            ArticleMetaKeys = new HashSet<ArticleMetaKey>();
            ArticleSections = new HashSet<ArticleSection>();
            Comments = new HashSet<Comment>();
            Companies = new HashSet<Company>();
            Insurers = new HashSet<Insurer>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public byte? Priority { get; set; }
        public bool? IsActivated { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public long? AuthorId { get; set; }
        public string Slug { get; set; }
        public long ArticleTypeId { get; set; }

        public virtual ArticleType ArticleType { get; set; }
        public virtual Person Author { get; set; }
        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
        public virtual ICollection<ArticleMetaKey> ArticleMetaKeys { get; set; }
        public virtual ICollection<ArticleSection> ArticleSections { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Insurer> Insurers { get; set; }
    }
}
