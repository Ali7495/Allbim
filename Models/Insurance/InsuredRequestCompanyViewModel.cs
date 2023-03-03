using Models.Company;
using System.ComponentModel.DataAnnotations;

namespace Models.Insurance
{
    public class InsuredRequestCompanyViewModel
    {
        public long Id { get; set; }
        [Required]
        public long InsuredRequestId { get; set; }
        [Required]
        public long CompanyId { get; set; }
    }
}
