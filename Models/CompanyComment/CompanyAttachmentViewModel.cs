using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyComment
{
    public class CompanyAttachmentViewModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("extention")]
        public string Extension { get; set; }
    }
}
