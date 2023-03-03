using Models.City;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Person
{
    public class PersonResultWithAgentCompanyViewModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }
        [JsonProperty("first_name")]

        public string FirstName { get; set; }
        [JsonProperty("last_name")]

        public string LastName { get; set; }
        [JsonProperty("national_code")]

        public string NationalCode { get; set; }
        [JsonProperty("identity")]

        public string Identity { get; set; }
        [JsonProperty("father_name")]

        public string FatherName { get; set; }
        [JsonProperty("birth_date")]
        public DateTime? BirthDate { get; set; }
        [JsonProperty("gender_id")]

        public byte GenderId { get; set; }
        [JsonProperty("marriage_id")]

        public byte MarriageId { get; set; }
        [JsonProperty("millitary_id")]
        public byte? MillitaryId { get; set; }

        [JsonProperty("job_name")]
        public string JobName { get; set; }

        [JsonProperty("role")]
        public virtual PersonRoleResultViewModel Role { get; set; }

        [JsonProperty("user")]
        public virtual PersonUserResultViewModel User { get; set; }

        [JsonProperty("person_company")]
        public virtual CompanyForPersonResultViewModel PersonCompany { get; set; }

        [JsonProperty("person_address")]
        public virtual AddressForPersonViewModel PersonAddress { get; set; }

        [JsonProperty("city")]
        public virtual PersonCityViewModel City { get; set; }

    }
}
