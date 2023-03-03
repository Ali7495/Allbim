using Models.Company;
using System.ComponentModel.DataAnnotations;

namespace Models.Insurance
{
    public class InsuredRequestPlaceViewModel
    {
        public long Id { get; set; }
        [Required]
        public long InsuredRequestId { get; set; }
        [Required]
        public int PlaceTypeId { get; set; }
        [Required]
        public long PlaceId { get; set; }
    }
}
