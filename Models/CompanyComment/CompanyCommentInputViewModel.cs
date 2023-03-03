using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyComment
{
    public class CompanyCommentInputViewModel
    {
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("author_code")]
        public Guid? AuthorCode { get; set; }
        //[JsonProperty("author_type_id")]
        //public byte? AuthorTypeId { get; set; }
        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("attachment_codes")]
        public List<Guid> AttachmentCodes { get; set; }
    }
}
