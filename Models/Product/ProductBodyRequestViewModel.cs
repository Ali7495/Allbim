using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Product
{
    public class ProductBodyRequestViewModel
    {
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

        [FromQuery(Name = "transportation")]
        public int? Transportation { get; set; }

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

        [FromQuery(Name = "price_fluctuatiuon_id")]
        public int? PriceFluctuatiuonId { get; set; }

        [FromQuery(Name = "no_damage_discount_id")]
        public int? NoDamageDiscountId { get; set; }

        [FromQuery(Name = "group_discount_id")]
        public int? GroupDiscountId { get; set; }

        [FromQuery(Name = "cash_discount_id")]
        public int? CashDiscountId { get; set; }

        [FromQuery(Name = "tax_id")]
        public int? TaxId { get; set; }
    }
}
