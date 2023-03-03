using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TownShip
    {
        public TownShip()
        {
            Cities = new HashSet<City>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long ProvinceId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Province Province { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}
