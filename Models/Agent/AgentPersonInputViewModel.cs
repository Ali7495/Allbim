using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Agent
{
    public class AgentPersonInputViewModel
    {
        [JsonProperty("first_name")]
        [Required]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        [Required]
        public string LastName { get; set; }
        [JsonProperty("national_code")]
        [Required]
        public string NationalCode { get; set; }
        [JsonProperty("identity")]
        [Required]
        public string Identity { get; set; }
        [JsonProperty("father_name")]
        [Required]
        public string FatherName { get; set; }
        [JsonProperty("birth_date")]
        public DateTime? BirthDate { get; set; }
        [JsonProperty("gender_id")]
        [Required]
        public byte GenderId { get; set; }
        [JsonProperty("marriage_id")]
        [Required]
        public byte MarriageId { get; set; }
        [JsonProperty("millitary_id")]
        public byte? MillitaryId { get; set; }

        [JsonProperty("job_name")]
        public string JobName { get; set; }
    }
}
