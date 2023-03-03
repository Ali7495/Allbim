using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Company
    {
        public Company()
        {
            CompanyAddresses = new HashSet<CompanyAddress>();
            CompanyAgents = new HashSet<CompanyAgent>();
            CompanyCenters = new HashSet<CompanyCenter>();
            InsuredRequestCompanies = new HashSet<InsuredRequestCompany>();
            InsuredRequestVehicles = new HashSet<InsuredRequestVehicle>();
            Insurers = new HashSet<Insurer>();
            PersonCompanies = new HashSet<PersonCompany>();
            PolicyRequestHolders = new HashSet<PolicyRequestHolder>();
        }

        public long Id { get; set; }
        public Guid Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string AvatarUrl { get; set; }
        public string Summary { get; set; }
        public long? ArticleId { get; set; }
        public string EstablishedYear { get; set; }
        public int? BranchNumber { get; set; }
        public int? WealthLevel { get; set; }
        public int? DamagePaymentSatisfactionRating { get; set; }
        public bool IsInsurer { get; set; }

        public virtual Article Article { get; set; }
        public virtual ICollection<CompanyAddress> CompanyAddresses { get; set; }
        public virtual ICollection<CompanyAgent> CompanyAgents { get; set; }
        public virtual ICollection<CompanyCenter> CompanyCenters { get; set; }
        public virtual ICollection<InsuredRequestCompany> InsuredRequestCompanies { get; set; }
        public virtual ICollection<InsuredRequestVehicle> InsuredRequestVehicles { get; set; }
        public virtual ICollection<Insurer> Insurers { get; set; }
        public virtual ICollection<PersonCompany> PersonCompanies { get; set; }
        public virtual ICollection<PolicyRequestHolder> PolicyRequestHolders { get; set; }
    }
}
