using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using Models.City;

namespace Models.Person
{
    public class PersonViewModel
    {
        [JsonProperty("code")]
        public Guid? Code { get; set; }
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

        [JsonProperty("role_id")]
        public long? RoleId { get; set; }

        [JsonProperty("user")]
        public virtual PersonUserViewModel User { get; set; }

        [JsonProperty("person_company")]
        public virtual CompanyForPersonViewModel PersonCompany { get; set; } 

        [JsonProperty("person_address")]
        public virtual AddressForPersonViewModel PersonAddress { get; set; }
        [JsonProperty("agent_id")]
        public long? AgentId { get; set; }
        [JsonProperty("agent_city_id")]
        public long? AgentCityId { get; set; }

    }
    public class PersonUpdateViewModel
    {
        [JsonProperty("code")]
        public Guid? Code { get; set; }
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
        //[JsonProperty("city_id")]
        //public long? CityId { get; set; }
        //[JsonProperty("province_id")]
        //public int? ProvinceId { get; set; }
        //public long? TownShipId { get; set; }
        public string JobName { get; set; }
        public bool IsDeleted { get; set; }
        public virtual PersonAddressViewModel PersonAddress { get; set; }

    }
    public class MyPersonViewModel
    {
    
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        public string Address { get; set; }
        public string NationalCode { get; set; }
        public string JobName { get; set; }
        public string Mobile { get; set; }
        public virtual CityResultViewModel City { get; set; }
        public virtual ProvinceResultViewModel Province { get; set; }
     
        public string Email { get; set; }

    }    
    public class MyPersonUpdateViewModel
    {
    
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }

        public string Address { get; set; }
        [Required]
        public string NationalCode { get; set; }
        public string JobName { get; set; }
        [Required]
        public long CityId { get; set; }
     
        public string Email { get; set; }

    }
    public class PersonInfoViewModel
    {
        [JsonProperty("code")]
        public Guid Code { get; set; }
        [JsonProperty("first_name")]
        [Required]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        [Required]
        public string LastName { get; set; }


    }
}
