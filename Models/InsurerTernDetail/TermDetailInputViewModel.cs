using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.InsurerTernDetail
{
    public class TermDetailInputViewModel
    {
        [JsonProperty("parent_id")]
        public long ParentId { get; set; }
        [JsonProperty("field")]
        public string Field { get; set; }
        [JsonProperty("criteria")]
        public string Criteria { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("discount")]
        public string Discount { get; set; }
        [JsonProperty("calculation_type")]
        public string CalculationType { get; set; }
        [JsonProperty("is_cumulative")]
        public bool IsCumulative { get; set; }
    }
}
