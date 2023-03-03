using Models.Company;
using System.ComponentModel.DataAnnotations;

namespace Models.Insurance
{
    public class InsuredPlaceViewModel
    {
        public long Id { get; set; }
        [Required]
        public long InsuredId { get; set; }
        [Required]
        public long PlaceId { get; set; }
    }
}
