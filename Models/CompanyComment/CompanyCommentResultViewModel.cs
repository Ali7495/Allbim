using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyComment
{
    public class CompanyCommentResultViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("policy_code")]
        public Guid PolicyCode { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("author_type_id")]
        public byte? AuthorTypeId { get; set; }
        [JsonProperty("created_date_time")]
        public DateTime? createdDateTime { get; set; }

        [JsonProperty("author")]
        public virtual CompanyCommentAuthorViewModel Author { get; set; }
        //[JsonProperty("policy_request")]
        //public virtual CompanyCommentPolicyRequestViewModel PolicyRequest { get; set; }
        [JsonProperty("policy_request_comment_attachments")]
        public virtual List<CompanyCommentAttachmentResultViewModel> PolicyRequestCommentAttachments { get; set; }
    }
}
