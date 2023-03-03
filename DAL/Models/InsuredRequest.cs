using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InsuredRequest
    {
        public InsuredRequest()
        {
            InsuredRequestCompanies = new HashSet<InsuredRequestCompany>();
            InsuredRequestPeople = new HashSet<InsuredRequestPerson>();
            InsuredRequestPlaces = new HashSet<InsuredRequestPlace>();
            InsuredRequestRelatedPeople = new HashSet<InsuredRequestRelatedPerson>();
            InsuredRequestVehicles = new HashSet<InsuredRequestVehicle>();
        }

        public long Id { get; set; }
        public long PolicyRequestId { get; set; }

        public virtual PolicyRequest PolicyRequest { get; set; }
        public virtual ICollection<InsuredRequestCompany> InsuredRequestCompanies { get; set; }
        public virtual ICollection<InsuredRequestPerson> InsuredRequestPeople { get; set; }
        public virtual ICollection<InsuredRequestPlace> InsuredRequestPlaces { get; set; }
        public virtual ICollection<InsuredRequestRelatedPerson> InsuredRequestRelatedPeople { get; set; }
        public virtual ICollection<InsuredRequestVehicle> InsuredRequestVehicles { get; set; }
    }
}
