using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class City
    {
        public City()
        {
            Addresses = new HashSet<Address>();
            Centers = new HashSet<Center>();
            CompanyAgents = new HashSet<CompanyAgent>();
            CompanyCenters = new HashSet<CompanyCenter>();
            Reminders = new HashSet<Reminder>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long TownShipId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TownShip TownShip { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Center> Centers { get; set; }
        public virtual ICollection<CompanyAgent> CompanyAgents { get; set; }
        public virtual ICollection<CompanyCenter> CompanyCenters { get; set; }
        public virtual ICollection<Reminder> Reminders { get; set; }
    }
}
