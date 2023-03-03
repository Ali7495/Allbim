using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json.Serialization;

namespace Models.Person
{
    public class RegisterViewModel
    {
        [JsonPropertyName("first_name")]
        [Required]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        [Required]
        public string LastName { get; set; }
        [JsonPropertyName("national_code")]
        [Required]
        public string NationalCode { get; set; }
        [JsonPropertyName("identity")]
        [Required]
        public string Identity { get; set; }
        [JsonPropertyName("father_name")]
        [Required]
        public string FatherName { get; set; }
        [JsonPropertyName("birth_date")]
        [AllowNull]
        public string BirthDate { get; set; }

        [JsonPropertyName("gender_id")]
        [Required]
        public byte GenderId { get; set; }
        [JsonPropertyName("marriage_id")]
        [Required]
        public byte MarriageId { get; set; }
        [JsonPropertyName("millitary_id")]
        public byte? MillitaryId { get; set; }

        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; }
        [JsonPropertyName("email")]
        [Required]
        public string Email { get; set; }
        [JsonPropertyName("username")]
        [Required]
        public string Username { get; set; }

        [JsonPropertyName("code")]
        [Required]
        public string Code { get; set; }
    }
}