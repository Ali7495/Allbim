using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using Models.Person;

namespace Models.User
{
    public class UserViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "person_code")]
        [Required]
        public string PersonCode { get; set; }

        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "password")]
        [Required]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "email")]
        [Required]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "username")]
        [Required]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "email_verified_at")]
        public DateTime? EmailVerifiedAt { get; set; }
        [JsonProperty(PropertyName = "is_enable")]
        public byte? IsEnable { get; set; }
        [JsonProperty(PropertyName = "is_verified")]
        public byte? IsVerified { get; set; }
        [JsonProperty(PropertyName = "is_two_step_verification")]
        public byte? IsTwoStepVerification { get; set; }
        [JsonProperty(PropertyName = "two_step_code")]
        public string TwoStepCode { get; set; }
        [JsonProperty(PropertyName = "two_step_expiration")]
        public DateTime? TwoStepExpiration { get; set; }
        [JsonProperty(PropertyName = "is_login_notify")]
        public byte? IsLoginNotify { get; set; }
        public string VerificationCode { get; set; }
        public DateTime? VerificationExpiration { get; set; }
        public DateTime? LastLogOnDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ChangePasswordCode { get; set; }

    }
    public class CheckVerificationViewModel
    {
        [JsonProperty(PropertyName = "username")]
        [Required]
        public string Username { get; set; }
        public string VerificationCode { get; set; }
    }
    public class ChangePasswordViewModel
    {
        public string ChangePasswordCode { get; set; }
        public string password { get; set; }
        

    }

    public class UserInfoViewModel
    {
        public string PersonCode { get; set; }
        public string code { get; set; }
        public string Username { get; set; }
        public virtual PersonInfoViewModel Person { get; set; }
    }
    
}
