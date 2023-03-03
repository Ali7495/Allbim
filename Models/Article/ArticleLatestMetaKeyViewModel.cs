using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Article
{
    public class ArticleLatestMetaKeyViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("articleId")]
        public long ArticleId { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
