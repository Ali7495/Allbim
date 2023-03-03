using Models.CentralRuleType;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.InsuranceCentralRule
{
    public class InsuranceCentralRuleResultViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("jalali_year")]
        public string JalaliYear { get; set; }
        [JsonProperty("gregorian_year")]
        public string GregorianYear { get; set; }
        //[JsonProperty("field_id")]
        //public string FieldId { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("is_cumulative")]
        public bool IsCumulative { get; set; }
        [JsonProperty("central_rule_type_id")]
        public long? CentralRuleTypeId { get; set; }
        [JsonProperty("discount")]
        public string Discount { get; set; }
        [JsonProperty("calculation_type_id")]
        public byte? CalculationTypeId { get; set; }
        [JsonProperty("pricing_type_id")]
        public byte? PricingTypeId { get; set; }
        [JsonProperty("condition_type_id")]
        public byte? ConditionTypeId { get; set; }


        [JsonProperty("insurance")]
        public virtual InsuranceForInsuranceCentraulRuleResultViewModel Insurance { get; set; }
        [JsonProperty("central_rule_type")]
        public virtual CentralRuleTypeViewModel CentralRuleType { get; set; }
    }
}
