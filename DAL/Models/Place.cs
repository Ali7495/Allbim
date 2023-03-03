using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Place
    {
        public Place()
        {
            InsuredRequestPlaces = new HashSet<InsuredRequestPlace>();
            PlaceAddresses = new HashSet<PlaceAddress>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<InsuredRequestPlace> InsuredRequestPlaces { get; set; }
        public virtual ICollection<PlaceAddress> PlaceAddresses { get; set; }
    }
}
