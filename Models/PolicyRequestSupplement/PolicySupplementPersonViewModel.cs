using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Models.Person;

namespace Models.PolicyRequestSupplement
{
    public class PolicySupplementPersonViewModel
    {
        [JsonPropertyName("code")]
        public Guid? Code { get; set; }
        //
        // [JsonPropertyName("first_name")] public string FirstName { get; set; } = null;
        // [JsonPropertyName("last_name")]
        // public string LastName { get; set; } = null;
        [JsonPropertyName("national_code")]
        [Required]
        public string NationalCode { get; set; }
        [JsonPropertyName("birth_date")]
        [Required]
        public DateTime BirthDate { get; set; }
        [JsonPropertyName("gender_id")]
        [Required]
        public byte GenderId { get; set; }
        
       

    }
}
