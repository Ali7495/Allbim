using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Info
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Slug { get; set; }
        public bool IsDeleted { get; set; }
    }
}
