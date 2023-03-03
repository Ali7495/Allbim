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
    public class PageInputViewModel
    {
        [JsonPropertyName("title")] public string Title { get; set; }

        [JsonPropertyName("summary")] public string Summary { get; set; }

        [JsonPropertyName("description")] public string Description { get; set; }

        [JsonPropertyName("priority")] public byte? Priority { get; set; }
        [Required]
        [JsonPropertyName("slug")] 
        public string Slug { get; set; }

        /// <summary>
        /// 1:UI
        /// 2:Users Panel
        /// 3:Admins Panel
        /// </summary>
        // [JsonPropertyName("article_section")]
        // public List<int> ArticleSections { get; set; }

        [JsonPropertyName("article_meta_key")] public List<ArticleMetaKeyViewModel> ArticleMetaKey { get; set; }

        // [JsonPropertyName("author_code")] public Guid AuthorCode { get; set; }

        // [JsonPropertyName("category_ids")] public virtual List<long> CategoryIds { get; set; }

    }
}
