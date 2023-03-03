using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Person
    {
        public Person()
        {
            Articles = new HashSet<Article>();
            Comments = new HashSet<Comment>();
            CompanyAgentPeople = new HashSet<CompanyAgentPerson>();
            CompanyAgents = new HashSet<CompanyAgent>();
            Discounts = new HashSet<Discount>();
            InsuredRequestPeople = new HashSet<InsuredRequestPerson>();
            InsuredRequestRelatedPeople = new HashSet<InsuredRequestRelatedPerson>();
            InsuredRequestVehicles = new HashSet<InsuredRequestVehicle>();
            PersonAddresses = new HashSet<PersonAddress>();
            PersonAttachments = new HashSet<PersonAttachment>();
            PersonCompanies = new HashSet<PersonCompany>();
            PolicyRequestComments = new HashSet<PolicyRequestComment>();
            PolicyRequestHolders = new HashSet<PolicyRequestHolder>();
            PolicyRequestRequestPeople = new HashSet<PolicyRequest>();
            PolicyRequestReviewers = new HashSet<PolicyRequest>();
            Reminders = new HashSet<Reminder>();
            Users = new HashSet<User>();
        }

        public long Id { get; set; }
        public Guid Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public string Identity { get; set; }
        public string FatherName { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte GenderId { get; set; }
        public byte? MarriageId { get; set; }
        public byte? MillitaryId { get; set; }
        public bool IsDeleted { get; set; }
        public string JobName { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CompanyAgentPerson> CompanyAgentPeople { get; set; }
        public virtual ICollection<CompanyAgent> CompanyAgents { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<InsuredRequestPerson> InsuredRequestPeople { get; set; }
        public virtual ICollection<InsuredRequestRelatedPerson> InsuredRequestRelatedPeople { get; set; }
        public virtual ICollection<InsuredRequestVehicle> InsuredRequestVehicles { get; set; }
        public virtual ICollection<PersonAddress> PersonAddresses { get; set; }
        public virtual ICollection<PersonAttachment> PersonAttachments { get; set; }
        public virtual ICollection<PersonCompany> PersonCompanies { get; set; }
        public virtual ICollection<PolicyRequestComment> PolicyRequestComments { get; set; }
        public virtual ICollection<PolicyRequestHolder> PolicyRequestHolders { get; set; }
        public virtual ICollection<PolicyRequest> PolicyRequestRequestPeople { get; set; }
        public virtual ICollection<PolicyRequest> PolicyRequestReviewers { get; set; }
        public virtual ICollection<Reminder> Reminders { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
