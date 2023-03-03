using Models.Comment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Article
{
    public class ArticlesViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("priority")]
        public byte? Priority { get; set; }
        [JsonProperty("is_activated")]
        public bool? IsActivated { get; set; }
        [JsonProperty("is_archived")]
        public bool IsArchived { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }


        [JsonProperty("author")]
        public virtual ArticleAuthorViewModel Author { get; set; }
        // [JsonProperty("comments")]
        // public virtual List<CommentResultViewModel> Comments { get; set; }
    }
    public class ArticleDetailViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("priority")]
        public byte? Priority { get; set; }
        [JsonProperty("is_activated")]
        public bool? IsActivated { get; set; }
        [JsonProperty("is_archived")]
        public bool IsArchived { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }


        [JsonProperty("author")]
        public virtual ArticleAuthorViewModel Author { get; set; }
        [JsonProperty("comments")]
        public virtual List<CommentResultViewModel> Comments { get; set; }
    }
}
