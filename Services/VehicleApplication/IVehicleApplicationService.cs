using Common.Utilities;
using DAL.Models;
using Models.Vehicle;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IVehicleApplicationService
    {
        Task<List<VehicleApplicationResultViewModel>> GetVehicleApplicationsAsync(CancellationToken cancellationToken);
        Task<PagedResult<VehicleApplicationResultViewModel>> GetAll(int? page, int? pageSize, CancellationToken cancellationToken);
        Task<VehicleApplicationResultViewModel> Get(long id,CancellationToken cancellationToken,long VehicleApplicationId);
        Task<VehicleApplicationResultViewModel> Create(VehicleApplicationInputViewModel vehicleApplicationViewModel, CancellationToken cancellationToken);
        Task<VehicleApplicationResultViewModel> Update(long id, VehicleApplicationInputViewModel vehicleApplicationViewModel, CancellationToken cancellationToken);
        Task<bool> Delete(long id, CancellationToken cancellationToken,long VehicleApplicationId);
    }
}
