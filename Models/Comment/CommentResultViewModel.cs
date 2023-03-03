using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comment
{
    public class CommentResultViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("author_code")]
        public Guid? AuthorCode { get; set; }
        [JsonProperty("article_id")]
        public long ArticleId { get; set; }
        [JsonProperty("parent_id")]
        public long? ParentId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("score")]
        public int? Score { get; set; }


        [JsonProperty("article")]
        public virtual CommentArticleResultViewModel Article { get; set; }
        [JsonProperty("author")]
        public virtual CommentAuthorResutlVeiwModel Author { get; set; }
        [JsonProperty("parent")]
        public virtual CommentResultViewModel Parent { get; set; }
    }
}
