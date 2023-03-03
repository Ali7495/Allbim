using Models.Company;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Insurance
{
    public class InsuranceCenteralRuleViewModel
    {
        public long Id { get; set; }
        [Required]
        public long InsuranceId { get; set; }
        public byte Type { get; set; }
        public string JalaliYear { get; set; }
        public string GregorianYear { get; set; }
        public string FieldType { get; set; }
        public string FieldId { get; set; }
        public string Value { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }


        public virtual InsuranceViewModel InsuranceViewModel { get; set; }

    }
}
