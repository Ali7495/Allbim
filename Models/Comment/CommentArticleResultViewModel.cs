using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comment
{
    public class CommentArticleResultViewModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
