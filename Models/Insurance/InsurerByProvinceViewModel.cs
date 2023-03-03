using Models.Company;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Insurance
{
    public class InsurerByProvinceViewModel
    {
        public long Id { get; set; }
        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }
        [JsonProperty("description")]
        [Required]
        public string Description { get; set; }

        [JsonProperty("insurance")]
        [Required]
        public InsuranceViewModel Insurance { get; set; }
        [Required]
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }

        public long CompanyId { get; set; }

        [JsonProperty("company_code")]
        public Guid CompanyCode { get; set; }

        [JsonProperty("company")]
        public virtual CompanyDetailViewModel Company { get; set; }
    }
}
