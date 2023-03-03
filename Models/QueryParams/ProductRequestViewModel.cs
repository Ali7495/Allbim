using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Models.MultipleDiscount;

namespace Models.QueryParams
{
    public class ProductRequestViewModel
    {
        public ProductRequestViewModel()
        {
            MultipleDiscounts = new List<MultipleDiscountViewModel>();
        }

        //third

        [FromQuery(Name = "vehicle_type_id")]
        public long VehicleTypeId { get; set; }

        [FromQuery(Name = "vehicle_brand_id")]
        public long VehicleBrandId { get; set; }

        [FromQuery(Name = "vehicle_construction_year")]
        public int VehicleConstructionYear { get; set; }

        [FromQuery(Name = "is_without_insurance")]
        public bool? IsWithoutInsurance { get; set; }

        [FromQuery(Name = "vehicle_id")]
        public long? VehicleId { get; set; }

        [FromQuery(Name = "vehicle_application_id")]
        public long VehicleApplicationId { get; set; }

        [FromQuery(Name = "vehicle_rule_category_id")]
        public long? VehicleRuleCategoryId { get; set; }

        [FromQuery(Name = "old_insurer_id")]
        public long? OldInsurerId { get; set; }

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

        [JsonProperty(PropertyName = "is_zero_kilometer")]
        public bool? IsZeroKilometer { get; set; }


        [JsonProperty(PropertyName = "is_prev_damaged")]
        public bool? IsPrevDamaged { get; set; }

        [FromQuery(Name = "vehicle_clearance_date")]
        public string VehicleClearanceDate { get; set; } = null;

        [FromQuery(Name = "Enum_id")]
        public int? EnumId { get; set; } = null;


        // Body
        [FromQuery(Name = "car_value")]
        public string CarValue { get; set; }

        [FromQuery(Name = "is_body_Insurance")]
        public bool? IsBodyInsurance { get; set; }

        [FromQuery(Name = "body_insurer")]
        public string BodyInsurer { get; set; }

        [FromQuery(Name = "prev_insurance_date")]
        public string PrevInsuranceDate { get; set; }


        [FromQuery(Name = "transportation_id")]
        public int? TransportationId { get; set; }

        [FromQuery(Name = "natural_disaster")]
        public int? NaturalDisaster { get; set; }

        [FromQuery(Name = "glass_breaking_id")]
        public int? GlassBreakingId { get; set; }

        [FromQuery(Name = "acidic_spray")]
        public int? AcidicSpray { get; set; }

        [FromQuery(Name = "stealing_requested_parts_id")]
        public int? StealingRequestedPartsId { get; set; }

        [FromQuery(Name = "stealing_all_parts_id")]
        public int? StealingAllPartsId { get; set; }

        [FromQuery(Name = "franchise_removal_id")]
        public int? FranchiseRemovalId { get; set; }

        [FromQuery(Name = "market_fluctuate_cover_id")]
        public int? MarketFluctuateCoverId { get; set; }

        [FromQuery(Name = "body_damage_discount")]
        public int? NoDamageDiscountId { get; set; }

        [FromQuery(Name = "multiple_discounts")]
        public List<MultipleDiscountViewModel> MultipleDiscounts { get; set; }

        [FromQuery(Name = "group_discount_id")]
        public int? GroupDiscountId { get; set; }

        [FromQuery(Name = "cash_discount_id")]
        public int? CashDiscountId { get; set; }

        [FromQuery(Name = "tax_id")]
        public int? TaxId { get; set; }




         

        // Life

        [FromQuery(Name = "is_life_insurance")]
        public bool? IsLifeInsurance { get; set; }
        [FromQuery(Name = "life_insurer_id")]
        public int? LifeInsurer { get; set; }



        // Bank
        [FromQuery(Name = "is_bank_long_account")]
        public bool? IsBankLongAccount { get; set; }
        [FromQuery(Name = "bank_account_id")]
        public int? BankAccount { get; set; }
    }
}
