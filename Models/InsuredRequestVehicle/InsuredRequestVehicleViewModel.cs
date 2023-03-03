using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class InsuredRequestVehicleViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("insured_id")]
        [Required]
        public long InsuredRequestId { get; set; }
        [JsonPropertyName("person_code")]
        [Required]
        public Guid? OwnerPersonCode { get; set; }
        [JsonPropertyName("company_code")]
        [Required]
        public Guid? OwnerCompanyCode { get; set; }
        [Required]
        public long VehicleId { get; set; }
    }
}
