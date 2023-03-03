using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public long Id { get; set; }
        public long PersonId { get; set; }
        public Guid Code { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? EmailVerifiedAt { get; set; }
        public byte? IsEnable { get; set; }
        public byte? IsVerified { get; set; }
        public byte? IsTwoStepVerification { get; set; }
        public string TwoStepCode { get; set; }
        public DateTime? TwoStepExpiration { get; set; }
        public byte? IsLoginNotify { get; set; }
        public string VerificationCode { get; set; }
        public DateTime? VerificationExpiration { get; set; }
        public DateTime? LastLogOnDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ChangePasswordCode { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
