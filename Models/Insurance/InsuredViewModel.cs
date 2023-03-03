using Models.Company;
using System.ComponentModel.DataAnnotations;

namespace Models.Insurance
{
    public class InsuredViewModel
    {
        public long Id { get; set; }
        [Required]

        public long PolicyId { get; set; }
    }
}
