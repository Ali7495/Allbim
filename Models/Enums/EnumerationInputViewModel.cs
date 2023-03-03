using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Enums
{
    public class EnumerationInputViewModel
    {

        [JsonProperty("parent_id")]
        public long? ParentId { get; set; }
        [JsonProperty("category_name")]
        public string CategoryName { get; set; }
        [JsonProperty("category_caption")]
        public string CategoryCaption { get; set; }
        [JsonProperty("enum_id")]
        public int EnumId { get; set; }
        [JsonProperty("enum_caption")]
        public string EnumCaption { get; set; }
        [JsonProperty("order")]
        public byte? Order { get; set; }
        [JsonProperty("is_enable")]
        public byte? IsEnable { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
