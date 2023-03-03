using Models.InsuranceTermType;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.InsurerTerm
{
    public class InsurerTermDetailedResultViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("insurance_term_type_id")]
        public long? InsuranceTermTypeId { get; set; }

        //[JsonProperty("type")]
        //public byte Type { get; set; }
        //[JsonProperty("field")]
        //public string Field { get; set; }
        //[JsonProperty("criteria")]
        //public string Criteria { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("discount")]
        public string Discount { get; set; }
        [JsonProperty("calculation_type_id")]
        public string CalculationType { get; set; }

        [JsonProperty("is_cumulative")]
        public bool IsCumulative { get; set; }

        [JsonProperty("pricing_type_id")]
        public byte? PricingTypeId { get; set; }

        [JsonProperty("condition_type_id")]
        public byte? ConditionTypeId { get; set; }


        [JsonProperty("insurer")]
        public virtual InsurerInsurerTermResultViewModel Insurer { get; set; }
        [JsonProperty("insurance_term_type")]
        public virtual InsuranceTermTypeViewModel InsuranceTermType { get; set; }
    }
}
