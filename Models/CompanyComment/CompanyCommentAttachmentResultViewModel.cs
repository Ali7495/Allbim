using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyComment
{
    public class CompanyCommentAttachmentResultViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("policy_request_comment_id")]
        public long? PolicyRequestCommentId { get; set; }
        [JsonProperty("attachment_code")]
        public Guid? AttachmentCode { get; set; }
        [JsonProperty("attachment_type_id")]
        public int? AttachmentTypeId { get; set; }

        [JsonProperty("attachment")]
        public virtual CompanyAttachmentViewModel Attachment { get; set; }
    }
}
