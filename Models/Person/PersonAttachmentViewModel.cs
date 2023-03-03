using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Person
{
    public class PersonAttachmentViewModel
    {
        [JsonProperty("id")]
        public long? Id { get; set; }
        [JsonProperty("person_code")]
        public Guid? PersonCode { get; set; }
        [JsonProperty("attachment_code")]
        public Guid? attachmentCode { get; set; }
        [JsonProperty("type_id")]
        public int TypeId { get; set; }
    }
}
