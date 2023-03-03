using Models.Company;
using System.ComponentModel.DataAnnotations;

namespace Models.Insurance
{
    public class InsuredPersonViewModel
    {
        public long Id { get; set; }
        [Required]
        public long InsuredId { get; set; }
        [Required]
        public long PersonId { get; set; }
    }
}
