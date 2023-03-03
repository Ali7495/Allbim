using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Models.Insurance;
using Models.Person;

namespace Models.PolicyRequestPaymentInfo
{
    public class PolicyRequestPaymentInfoViewModel
    {
        [JsonPropertyName("code")]
        public Guid? Code { get; set; }
        
        [JsonPropertyName("insurance")]
        public virtual InsuranceViewModel Insurance { get; set; }        
     
        [JsonPropertyName("vehicle_fullname")]
        public string VehicleFullName { get; set; }
        
        [JsonPropertyName("vehicle_construction_year")]
        public virtual AddressViewModel VehicleConstructionYear { get; set; }
        
        [JsonPropertyName("insurance_price")]
        public string InsurancePrice { get; set; }
        
        [JsonPropertyName("total_price")]
        public string TotalPice { get; set; }        
        
        [JsonPropertyName( "third_discount_id")]
        public int? ThirdDiscountId { get; set; }

        [JsonPropertyName(  "driver_discount_id")]
        public int? DriverDiscountId { get; set; }
        [JsonPropertyName(  "old_insurer_start_date")]
        public string OldInsurerStartDate { get; set; } = null;

        [JsonPropertyName( "old_insurer_expire_date")]
        public string OldInsurerExpireDate { get; set; } = null;
    


    }
}
