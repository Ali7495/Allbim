using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.MultipleDiscount
{
    public class MultipleDiscountViewModel
    {
        [JsonProperty("order")]
        public int Order { get; set; }
        [JsonProperty("field")]
        public string Field { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
