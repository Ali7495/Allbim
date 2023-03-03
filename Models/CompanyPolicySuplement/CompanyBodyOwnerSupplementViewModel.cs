using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyPolicySuplement
{
    public class CompanyBodyOwnerSupplementViewModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }
        [JsonProperty("national_code")]
        public string NationalCode { get; set; }

        [JsonProperty("birth_date")]
        public DateTime BirthDate { get; set; }

        [JsonProperty("gender_id")]
        public byte GenderId { get; set; }

        [JsonProperty("city_id")]
        public long CityId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
