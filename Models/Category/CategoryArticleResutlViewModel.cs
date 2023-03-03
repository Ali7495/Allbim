using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Category
{
    public class CategoryArticleResutlViewModel
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
        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("author_first_name")]
        public string AuthorFirstName { get; set; }
        [JsonProperty("author_last_name")]
        public string AuthorLastName { get; set; }
    }
}
