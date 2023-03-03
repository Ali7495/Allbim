using DAL.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace DAL.Contracts
{
    public interface IVehicleBrandRepository : IRepository<VehicleBrand>
    {
        Task<List<VehicleBrand>> GetByVehicleTypeId(long vehicleTypeId, CancellationToken cancellationToken);
    }
}