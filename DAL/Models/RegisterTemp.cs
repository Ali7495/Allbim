using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class RegisterTemp
    {
        public long Id { get; set; }
        public string Mobile { get; set; }
        public string Code { get; set; }
        public string Ip { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
