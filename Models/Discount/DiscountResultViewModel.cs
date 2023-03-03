using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Discount
{
    public class DiscountResultViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("person_id")]
        public long? PersonId { get; set; }
        [JsonProperty("insurance_id")]
        public long? InsuranceId { get; set; }
        [JsonProperty("insurer_id")]
        public long? InsurerId { get; set; }
        [JsonProperty("value")]
        public int? Value { get; set; }
        [JsonProperty("expiration_date_time")]
        public DateTime? ExpirationDateTime { get; set; }
        [JsonProperty("is_used")]
        public bool IsUsed { get; set; }

        [JsonProperty("insurance")]
        public virtual DiscountInsuranceResultViewModel Insurance { get; set; }
        //public virtual Insurer Insurer { get; set; }
        [JsonProperty("person")]
        public virtual DiscountPersonResultViewModel Person { get; set; }
    }
}
