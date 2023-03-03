using System;

namespace Models.Insurance
{
    public class InsurerTermViewModel
    {
        public long Id { get; set; }
        //public byte Type { get; set; }
       // public string Field { get; set; }
        //public string Criteria { get; set; }
        public string Value { get; set; }
        public string Discount { get; set; }
        public byte CalculationTypeId { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public long InsurerId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
