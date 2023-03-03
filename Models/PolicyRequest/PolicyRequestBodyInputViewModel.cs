using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.PolicyRequest
{
    public class PolicyRequestBodyInputViewModel
    {
        [JsonProperty(PropertyName = "vehicle_type_id")]
        public long VehicleTypeId { get; set; }

        [JsonProperty(PropertyName = "vehicle_brand_id")]
        public long VehicleBrandId { get; set; }

        [JsonProperty(PropertyName = "vehicle_construction_year")]
        public int VehicleConstructionYear { get; set; }

        [JsonProperty(PropertyName = "vehicle_id")]
        public long? VehicleId { get; set; }

        [JsonProperty(PropertyName = "vehicle_application_id")]
        public long VehicleApplicationId { get; set; }

        //[JsonProperty(PropertyName = "vehicle_rule_category_id")]
        public long? VehicleRuleCategoryId { get; set; }

        [JsonProperty(PropertyName = "insurer_id")]
        public long InsurerId { get; set; }

        [JsonProperty(PropertyName = "car_value")]
        public string CarValue { get; set; }

        [JsonProperty(PropertyName = "transportation_id")]
        public int? TransportationId { get; set; }

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

        [JsonProperty(PropertyName = "price_fluctuatiuon_id")]
        public int? PriceFluctuatiuonId { get; set; }

        [JsonProperty(PropertyName = "no_damage_discount_id")]
        public int? NoDamageDiscountId { get; set; }

        [JsonProperty(PropertyName = "group_discount_id")]
        public int? GroupDiscountId { get; set; }

        [JsonProperty(PropertyName = "cash_discount_id")]
        public int? CashDiscountId { get; set; }


        [JsonProperty(PropertyName = "tax_id")]
        public int? TaxId { get; set; }
    }
}
