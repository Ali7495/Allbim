using Models.Company;
using System.ComponentModel.DataAnnotations;

namespace Models.Insurance
{
    public class InsuredRequestVehicleDetailViewModel
    {
        public long Id { get; set; }
        [Required]
        public long InsuredRequestId { get; set; }

        public long? OwnerPersonId { get; set; }

        public long? OwnerCompanyId { get; set; }
        [Required]
        public long VehicleId { get; set; }
    }
}
