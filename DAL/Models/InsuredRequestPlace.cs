using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InsuredRequestPlace
    {
        public long Id { get; set; }
        public long InsuredRequestId { get; set; }
        public int PlaceTypeId { get; set; }
        public long PlaceId { get; set; }

        public virtual InsuredRequest InsuredRequest { get; set; }
        public virtual Place Place { get; set; }
    }
}
