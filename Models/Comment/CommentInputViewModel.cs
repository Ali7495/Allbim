using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comment
{
    public class CommentInputViewModel
    {
        //[JsonProperty("author_code")]
        //public Guid? AuthorCode { get; set; }
        [JsonProperty("parent_id")]
        public long? ParentId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("score")]
        public int? Score { get; set; }


        
    }
}
