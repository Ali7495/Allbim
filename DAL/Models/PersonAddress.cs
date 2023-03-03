﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PersonAddress
    {
        public long Id { get; set; }
        public long AddressId { get; set; }
        public long PersonId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public long AddressTypeId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Person Person { get; set; }
    }
}
