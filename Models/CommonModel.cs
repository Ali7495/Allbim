using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.ViewModels
{
    public class CommonModel
    {
       

        public class RolesModel
        {
            [JsonProperty(PropertyName = "role_id")]
            public int Id { get; set; }
            [JsonProperty(PropertyName = "role_name")]
            public string Name { get; set; }
        }
        public class LoginModel
        {
            [JsonProperty(PropertyName = "username")]
            [Required]
            public string UserName { get; set; }
            [JsonProperty(PropertyName = "password")]
            [Required]
            public string Password { get; set; }
        }

        public class RegisterModel
        {
            [JsonProperty(PropertyName = "first_name")]
            [Required]
            public string FirstName { get; set; }
            [JsonProperty(PropertyName = "last_name")]
            [Required]
            public string LastName { get; set; }
            [JsonProperty(PropertyName = "national_code")]
            [Required]
            public string NationalCode { get; set; }
            [JsonProperty(PropertyName = "identity")]
            [Required]
            public string Identity { get; set; }
            [JsonProperty(PropertyName = "father_name")]
            [Required]
            public string FatherName { get; set; }
            [JsonProperty(PropertyName = "birth_date")]
            public DateTime? BirthDate { get; set; }
            [JsonProperty(PropertyName = "gender_id")]
            [Required]
            public byte GenderId { get; set; }
            [JsonProperty(PropertyName = "marriage_id")]
            [Required]
            public byte MarriageId { get; set; }
            [JsonProperty(PropertyName = "millitary_id")]
            public byte? MillitaryId { get; set; }

            [JsonProperty(PropertyName = "password")]
            [Required]
            public string Password { get; set; }
            [JsonProperty(PropertyName = "email")]
            [Required]
            public string Email { get; set; }
            [JsonProperty(PropertyName = "username")]
            [Required]
            public string Username { get; set; }

        }
    }
}