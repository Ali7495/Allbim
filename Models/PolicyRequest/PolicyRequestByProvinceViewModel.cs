using Models.Insurance;
using Models.Person;
using Models.Vehicle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PolicyRequest
{
    public class PolicyRequestByProvinceViewModel
    {
        [JsonProperty("insurer")]
        public InsurerByProvinceViewModel Insurer { get; set; }

        //[JsonProperty("vehicle_type")]
        //public string VehicleType { get; set; }

        //[JsonProperty("vehicle_model")]
        //public string VehicleModel { get; set; }

        [JsonProperty("vehicle")]
        public VehicleResultViewModel Vehicle { get; set; }

        //[JsonProperty("first_name")]
        //public string FirstName { get; set; }

        //[JsonProperty("last_name")]
        //public string LastName { get; set; }

        //[JsonProperty("national_code")]
        //public string NationalCode { get; set; }

        [JsonProperty("request_person")]
        public PersonViewModel RequestPerson { get; set; }

        [JsonProperty("construction_year")]
        public string ConstructionYear { get; set; }

        [JsonProperty("old_insurance_start_date")]
        public string OldInsuranceStartDate { get; set; }

        [JsonProperty("old_insurance_end_date")]
        public string OldInsuranceEndDate { get; set; }

        [JsonProperty("old_policy_request_discount")]
        public string OldPolicyRequestDiscount { get; set; }

        [JsonProperty("driver_damage_discount_percent")]
        public string DriverDamageDiscountPercent { get; set; }

        [JsonProperty("old_body_insurer")]
        public string OldBodyInsurer { get; set; }

        [JsonProperty("cover_percent")]
        public string CoverPercent { get; set; }

        [JsonProperty("payment_price")]
        public string PaymentPrice { get; set; }
    }
}
