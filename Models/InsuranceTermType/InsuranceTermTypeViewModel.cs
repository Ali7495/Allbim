using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.InsuranceTermType
{
    public class InsuranceTermTypeViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("insurance_field_id")]
        public long InsuranceFieldId { get; set; }
        [JsonProperty("term_caption")]
        public string TermCaption { get; set; }
        [JsonProperty("order")]
        public int Order { get; set; }
        [JsonProperty("field")]
        public string Field { get; set; }
        [JsonProperty("related_resource")]
        public string RelatedResource { get; set; }
        [JsonProperty("resource_type_id")]
        public byte ResourceTypeId { get; set; }
        [JsonProperty("pricing_type_id")]
        public byte PricingTypeId { get; set; }

        public virtual InsuranceFieldViewModel InsuranceField { get; set; }
    }
}
