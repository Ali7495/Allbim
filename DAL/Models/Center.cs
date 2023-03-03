using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Center
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? CityId { get; set; }

        public virtual City City { get; set; }
    }
}
