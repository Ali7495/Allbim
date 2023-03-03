using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Insured
    {
        public Insured()
        {
            InsuredCompanies = new HashSet<InsuredCompany>();
            InsuredPeople = new HashSet<InsuredPerson>();
            InsuredPlaces = new HashSet<InsuredPlace>();
            InsuredRelatedPeople = new HashSet<InsuredRelatedPerson>();
            InsuredVehicles = new HashSet<InsuredVehicle>();
        }

        public long Id { get; set; }
        public long PolicyId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Policy Policy { get; set; }
        public virtual ICollection<InsuredCompany> InsuredCompanies { get; set; }
        public virtual ICollection<InsuredPerson> InsuredPeople { get; set; }
        public virtual ICollection<InsuredPlace> InsuredPlaces { get; set; }
        public virtual ICollection<InsuredRelatedPerson> InsuredRelatedPeople { get; set; }
        public virtual ICollection<InsuredVehicle> InsuredVehicles { get; set; }
    }
}
