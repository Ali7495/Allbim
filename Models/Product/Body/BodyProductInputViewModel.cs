using Microsoft.AspNetCore.Mvc;
using Models.MultipleDiscount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Product
{
    public class BodyProductInputViewModel
    {


        [FromQuery(Name = "vehicle_type_id")]
        public long VehicleTypeId { get; set; }

        [FromQuery(Name = "vehicle_brand_id")]
        public long VehicleBrandId { get; set; }

        [FromQuery(Name = "vehicle_construction_year")]
        public int VehicleConstructionYear { get; set; }
        
        [FromQuery(Name = "vehicle_id")]
        public long? VehicleId { get; set; }

        [FromQuery(Name = "vehicle_application_id")]
        public long VehicleApplicationId { get; set; }

        
        public long VehicleRuleCategoryId { get; set; }

        [FromQuery(Name = "car_value")]
        public string CarValue { get; set; }  
        [FromQuery(Name = "non_fabrique_assets_value")]
        public decimal NonFabriqueAssetsValue { get; set; }

        [FromQuery(Name = "is_without_insurance")]
        public bool? IsWithoutInsurance { get; set; }

        [FromQuery(Name = "prev_isnurance_company_id")]
        public Guid? PrevInsuranceCompanyId { get; set; }
        [FromQuery(Name = "prev_insurance_end_date")]
        public DateTime PrevInsuranceEndDate { get; set; }
        [FromQuery(Name = "body_no_damage_discount_id")]
        public long BodyNoDamageDisountId { get; set; }
        
        [FromQuery(Name = "has_third_insurance")]
        public bool? HasThirdInsurance { get; set; }
        [FromQuery(Name = "third_insurance_company_id")]
        public Guid? ThirdInsuranceCompanyId { get; set; }
        [FromQuery(Name = "third_no_damage_discount_id")]
        public long ThirdNoDamageDisountId { get; set; }
        
        [FromQuery(Name = "is_zero_kilometer")]
        public bool? IsZeroKilometer { get; set; }

        

        [FromQuery(Name = "transportation")]
        public int? Transportation { get; set; }

        //[FromQuery(Name = "has_natural_disaster")]
        //public bool? NaturalDisaster { get; set; }

        //[FromQuery(Name = "has_glass_breaking")]
        //public bool? GlassBreakingId { get; set; }

        //[FromQuery(Name = "has_acidic_spray")]
        //public bool? AcidicSpray { get; set; }

        //[FromQuery(Name = "has_stealing_requested_parts")]
        //public bool? StealingRequestedPartsId { get; set; }

        //[FromQuery(Name = "has_stealing_all_parts")]
        //public bool? StealingAllPartsId { get; set; }

        [FromQuery(Name = "flood_and_earth_quake_id")]
        public int? FloodAndEarthquakeId { get; set; }

        [FromQuery(Name = "glass_breaking_id")]
        public int? GlassBreakingId { get; set; }

        [FromQuery(Name = "acid_and_chemical_id")]
        public int? AcidAndChemicalId { get; set; }

        [FromQuery(Name = "stealing_requested_parts_id")]
        public int? StealingRequestedPartsId { get; set; }

        [FromQuery(Name = "stealing_all_parts_id")]
        public int? StealingAllPartsId { get; set; }

        [FromQuery(Name = "franchise_removal_id")]
        public int? FranchiseRemovalId { get; set; }

        [FromQuery(Name = "market_fluctuate_cover_id")]
        public int? MarketFluctuateCoverId { get; set; }

        [FromQuery(Name = "no_damage_discount_id")]
        public int? NoDamageDiscountId { get; set; }

        //[FromQuery(Name = "multiple_discounts")]
        //public List<MultipleDiscountViewModel> MultipleDiscounts { get; set; }

        [FromQuery(Name = "group_discount_id")]
        public int? GroupDiscountId { get; set; }

        [FromQuery(Name = "cash_discount_id")]
        public int? CashDiscountId { get; set; }

        [FromQuery(Name = "is_cash")]
        public bool? IsCash { get; set; }

        [FromQuery(Name = "tax_id")]
        public int? TaxId { get; set; }


        // Life

        //[FromQuery(Name = "is_life_insurance")]
        //public bool? IsLifeInsurance { get; set; }
        //[FromQuery(Name = "life_company_id")]
        //public Guid? LifeCompanyId { get; set; }



        // Bank
        [FromQuery(Name = "has_bank_long_account")]
        public bool? HasBankLongAccount { get; set; }
        [FromQuery(Name = "bank_account_id")]
        public long? BankAccountId { get; set; }

        public decimal SuggestedPrice { get; set; }
    }
}
