using Models.Company;
using System.ComponentModel.DataAnnotations;

namespace Models.Insurance
{
    public class InsuredCompanyViewModel
    {
        public long Id { get; set; }
        [Required]

        public long InsuredId { get; set; }
        [Required]

        public long CompanyId { get; set; }
    }
}
