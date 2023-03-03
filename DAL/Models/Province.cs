using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Province
    {
        public Province()
        {
            TownShips = new HashSet<TownShip>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<TownShip> TownShips { get; set; }
    }
}
