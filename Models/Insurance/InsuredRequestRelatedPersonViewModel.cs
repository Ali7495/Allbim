using Models.Company;
using System.ComponentModel.DataAnnotations;

namespace Models.Insurance
{
    public class InsuredRequestRelatedPersonViewModel
    {
        public long Id { get; set; }
        [Required]
        public long InsuredRequestId { get; set; }
        [Required]
        public int RelationTypeId { get; set; }
        [Required]
        public long PersonId { get; set; }
    }
}
