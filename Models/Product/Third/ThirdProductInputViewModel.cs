using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Product
{
    public class ThirdProductInputViewModel
    {
        [FromQuery(Name = "vehicle_type_id")] public long VehicleTypeId { get; set; }

        [FromQuery(Name = "vehicle_brand_id")] public long VehicleBrandId { get; set; }

        [FromQuery(Name = "vehicle_construction_year")]
        public int VehicleConstructionYear { get; set; }

        [FromQuery(Name = "is_without_insurance")]
        public bool? IsWithoutInsurance { get; set; }

        [FromQuery(Name = "vehicle_id")] public long? VehicleId { get; set; }

        [FromQuery(Name = "vehicle_application_id")]
        public long VehicleApplicationId { get; set; }

        [FromQuery(Name = "old_insurer_id")] public long? OldInsurerId { get; set; }

        [FromQuery(Name = "old_insurer_start_date")]
        public string OldInsurerStartDate { get; set; } = null;

        [FromQuery(Name = "old_insurer_expire_date")]
        public string OldInsurerExpireDate { get; set; } = null;

        [FromQuery(Name = "is_changed_owner")]
        public bool IsChangedOwner { get; set; }

        [FromQuery(Name = "third_discount_id")]
        public int? ThirdDiscountId { get; set; }

        [FromQuery(Name = "driver_discount_id")]
        public int? DriverDiscountId { get; set; }

        [FromQuery(Name = "third_life_damage_id")]
        public int? ThirdLifeDamageId { get; set; }

        [FromQuery(Name = "third_financial_damage_id")]
        public int? ThirdFinancialDamageId { get; set; }

        [FromQuery(Name = "driver_life_damage_id")]
        public int? DriverLifeDamageId { get; set; }

        [FromQuery(Name = "is_zero_kilometer")]
        public bool? IsZeroKilometer { get; set; }

        [FromQuery(Name = "is_prev_damaged")] public bool? IsPrevDamaged { get; set; }

        [FromQuery(Name = "vehicle_clearance_date")]

        public string VehicleClearanceDate { get; set; } = null;

        [FromQuery(Name = "insurer_id")] 
        public long InsurerId { get; set; }

        [FromQuery(Name = "vehicle_rule_category_id")]
        public long? VehicleRuleCategoryId { get; set; }
        [FromQuery(Name = "body_damage_discount")]
        public int? NoDamageDiscountId { get; set; }


        public decimal SuggestedPrice { get; set; }

    }
}