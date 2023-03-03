using Models.Company;
using System.ComponentModel.DataAnnotations;

namespace Models.Insurance
{
    public class InsuredVehicleViewModel
    {
        public long Id { get; set; }
        [Required]
        public long InsuredId { get; set; }
        [Required]
        public long VehicleId { get; set; }
    }
}
