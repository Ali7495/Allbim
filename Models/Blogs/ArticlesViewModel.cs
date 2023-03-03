using Models.Article;
using Models.Blogs;
using Models.Comment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Articles
{
    public class ArticlesInputViewModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("article_meta_key")]
        public List<ArticleMetaKeyViewModel> ArticleMetaKey { get; set; }
        
        [JsonPropertyName("author_code")]
        public Guid AuthorCode { get; set; }

        [JsonPropertyName("category_ids")]
        public virtual List<long> CategoryIds { get; set; }

    }

    public class ArticleMetaKeyViewModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class ArticleMetaKeyOutputViewModel
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }



    public class ArticleResultViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public byte? Priority { get; set; }
        public string Slug { get; set; }
        public bool? IsActivated { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public long? AuthorId { get; set; }
        public List<ArticleMetaKeyOutputViewModel> ArticleMetaKeys { get; set; }
        [JsonProperty("article_categories")]
        public virtual List<ArticleLatestCategoryViewModel> ArticleCategories { get; set; }
        public List<CommentResultViewModel> Comments { get; set; }
        public ArticleAuthorViewModel Author { get; set; }
    }
   
    public class ArticleSectionViewModel
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public int SectionId { get; set; }
    }

   
    public class ArticleSummaryViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("summary")]
        public string Summary { get; set; }
        [JsonPropertyName("priority")]
        public byte? Priority { get; set; }
        [JsonPropertyName("slug")]
        public string Slug { get; set; }
        [JsonPropertyName("is_activated")]
        public bool? IsActivated { get; set; }
        [JsonPropertyName("is_archived")]
        public bool IsArchived { get; set; }
        [JsonPropertyName("created_date_time")]
        public DateTime CreatedDateTime { get; set; }
    }
}
