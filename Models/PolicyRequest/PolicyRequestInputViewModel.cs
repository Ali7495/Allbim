using System;
using Newtonsoft.Json;

namespace Models.PolicyRequest
{
    public class PolicyRequestInputViewModel
    {
        [JsonProperty(PropertyName = "vehicle_type_id")]
        public long VehicleTypeId { get; set; }

        [JsonProperty(PropertyName = "vehicle_brand_id")]
        public long VehicleBrandId { get; set; }

        [JsonProperty(PropertyName = "vehicle_construction_year")]
        public int VehicleConstructionYear { get; set; }

        [JsonProperty(PropertyName = "is_without_insurance")]
        public bool? IsWithoutInsurance { get; set; }
        
        [JsonProperty(PropertyName = "vehicle_id")]
        public long? VehicleId { get; set; }

        [JsonProperty(PropertyName = "vehicle_application_id")]
        public long VehicleApplicationId { get; set; }

        [JsonProperty(PropertyName = "old_insurer_id")]
        public long? OldInsurerId { get; set; }

        [JsonProperty(PropertyName = "old_insurer_start_date")]
        public string OldInsurerStartDate { get; set; } = null;

        [JsonProperty(PropertyName = "old_insurer_expire_date")]
        public string OldInsurerExpireDate { get; set; }    = null;
        
        [JsonProperty(PropertyName = "is_changed_owner")]
        public bool IsChangedOwner { get; set; }

        [JsonProperty(PropertyName = "third_discount_id")]
        public int? ThirdDiscountId { get; set; }

        [JsonProperty(PropertyName = "driver_discount_id")]
        public int? DriverDiscountId { get; set; }

        [JsonProperty(PropertyName = "third_life_damage_id")]
        public int? ThirdLifeDamageId { get; set; }

        [JsonProperty(PropertyName = "third_financial_damage_id")]
        public int? ThirdFinancialDamageId { get; set; }

        [JsonProperty(PropertyName = "driver_life_damage_id")]
        public int? DriverLifeDamageId { get; set; } 
        [JsonProperty( PropertyName = "is_zero_kilometer")]
        public bool? IsZeroKilometer { get; set; }  
        [JsonProperty( PropertyName = "is_prev_damaged")]
       
        public bool? IsPrevDamaged { get; set; }

        [JsonProperty(PropertyName = "vehicle_clearance_date")]

        public string VehicleClearanceDate { get; set; } = null;
        
        
        [JsonProperty(PropertyName = "insurer_id")]
        public long InsurerId { get; set; }


        [JsonProperty(PropertyName = "vehicle_rule_category_id")]
        public long? VehicleRuleCategoryId { get; set; }
        
        
        
        
        
        // Body
        
        
        [JsonProperty(PropertyName ="car_value")]
        public string CarValue { get; set; }

        [JsonProperty(PropertyName = "transportation")]
        public int? Transportation { get; set; }

        [JsonProperty(PropertyName = "flood_and_earth_quake_id")]
        public int? FloodAndEarthquakeId { get; set; }

        [JsonProperty(PropertyName = "glass_breaking_id")]
        public int? GlassBreakingId { get; set; }

        [JsonProperty(PropertyName = "acid_and_chemical_id")]
        public int? AcidAndChemicalId { get; set; }

        [JsonProperty(PropertyName = "stealing_requested_parts_id")]
        public int? StealingRequestedPartsId { get; set; }

        [JsonProperty(PropertyName = "stealing_all_parts_id")]
        public int? StealingAllPartsId { get; set; }

        [JsonProperty(PropertyName = "franchise_removal_id")]
        public int? FranchiseRemovalId { get; set; }

        //[JsonProperty(PropertyName = "price_fluctuatiuon_id")]
        //public int? PriceFluctuatiuonId { get; set; }

        [JsonProperty(PropertyName = "no_damage_discount_id")]
        public int? NoDamageDiscountId { get; set; }

        [JsonProperty(PropertyName = "group_discount_id")]
        public int? GroupDiscountId { get; set; }

        [JsonProperty(PropertyName = "cash_discount_id")]
        public int? CashDiscountId { get; set; }

        [JsonProperty(PropertyName = "tax_id")]
        public int? TaxId { get; set; }

      
        [JsonProperty(PropertyName = "non_fabrique_assets_value")]
        public decimal NonFabriqueAssetsValue { get; set; }

       
        [JsonProperty(PropertyName =  "prev_isnurance_company_id")]
        public Guid? PrevInsuranceCompanyId { get; set; }
        [JsonProperty(PropertyName = "prev_insurance_end_date")]
        public DateTime PrevInsuranceEndDate { get; set; }
        [JsonProperty(PropertyName = "body_no_damage_discount_id")]
        public DateTime BodyNoDamageDisountId { get; set; }
        
        [JsonProperty(PropertyName = "has_third_insurance")]
        public bool? HasThirdInsurance { get; set; }
        [JsonProperty(PropertyName = "third_insurance_company_id")]
        public Guid? ThirdInsuranceCompanyId { get; set; }
        [JsonProperty(PropertyName = "third_no_damage_discount_id")]
        public DateTime ThirdNoDamageDisountId { get; set; }

        [JsonProperty(PropertyName = "has_natural_disaster")]
        public bool? NaturalDisaster { get; set; }
        

        [JsonProperty(PropertyName = "has_acidic_spray")]
        public bool? AcidicSpray { get; set; }
        

        // [JsonProperty(PropertyName = "franchise_removal_id")]
        // public int? FranchiseRemovalId { get; set; }

        [JsonProperty(PropertyName = "market_fluctuate_cover_id")]
        public int? MarketFluctuateCoverId { get; set; }
        
        // [JsonProperty(PropertyName = "group_discount_id")]
        // public int? GroupDiscountId { get; set; }

        [JsonProperty(PropertyName = "is_cash")]
        public bool? IsCash { get; set; }

        // [JsonProperty(PropertyName = "tax_id")]
        // public int? TaxId { get; set; }

        
        // Life

        [JsonProperty(PropertyName = "is_life_insurance")]
        public bool? IsLifeInsurance { get; set; }
        [JsonProperty(PropertyName = "life_company_id")]
        public Guid? LifeCompanyId { get; set; }



        // Bank
        [JsonProperty(PropertyName = "has_bank_long_account")]
        public bool? HasBankLongAccount { get; set; }
        [JsonProperty(PropertyName = "bank_account_id")]
        public long? BankAccountId { get; set; }

    }
}