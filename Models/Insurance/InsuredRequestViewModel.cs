using Models.Company;
using System.ComponentModel.DataAnnotations;

namespace Models.Insurance
{
    public class InsuredRequestViewModel
    {
        public long Id { get; set; }
        [Required]
        public long PolicyRequestId { get; set; }
    }
}
