using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Address
    {
        public Address()
        {
            CompanyAddresses = new HashSet<CompanyAddress>();
            PersonAddresses = new HashSet<PersonAddress>();
            PlaceAddresses = new HashSet<PlaceAddress>();
            PolicyRequestHolders = new HashSet<PolicyRequestHolder>();
            PolicyRequestInspections = new HashSet<PolicyRequestInspection>();
            PolicyRequestIssues = new HashSet<PolicyRequestIssue>();
        }

        public long Id { get; set; }
        public Guid Code { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
        public string Description { get; set; }
        public string ZoneNumber { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<CompanyAddress> CompanyAddresses { get; set; }
        public virtual ICollection<PersonAddress> PersonAddresses { get; set; }
        public virtual ICollection<PlaceAddress> PlaceAddresses { get; set; }
        public virtual ICollection<PolicyRequestHolder> PolicyRequestHolders { get; set; }
        public virtual ICollection<PolicyRequestInspection> PolicyRequestInspections { get; set; }
        public virtual ICollection<PolicyRequestIssue> PolicyRequestIssues { get; set; }
    }
}
