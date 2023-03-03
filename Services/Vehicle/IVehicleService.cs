using Common.Utilities;
using DAL.Models;
using Models.Vehicle;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IVehicleService
    {
        Task<List<VehicleTypeViewModel>> GetVehicleTypesAsync(CancellationToken cancellationToken);
        Task<List<VehicleBrandResultViewModel>> GetVehicleBrandsByTypeAsync(long vehicleTypeId, CancellationToken cancellationToken);


        #region Get
        Task<List<VehicleDetail>> GetAllVehicleDetailsAsync(CancellationToken cancellationToken);
        Task<PagedResult<VehicleDetail>> GetVehicleDetainsAsync(int? page, int? pageSize, CancellationToken cancellationToken);
        Task<VehicleDetail> GetVehicleDetailAsync(long id, CancellationToken cancellationToken);



        Task<VehicleBrandResultViewModel> GetVehicleBrandAsync(long id, CancellationToken cancellationToken);
        Task<PagedResult<VehicleBrandResultViewModel>> GetVehicleBrandsAsync(int? page, int? pageSize, CancellationToken cancellationToken);
        Task<List<VehicleBrandResultViewModel>> GetAllVehicleBrandsAsync(CancellationToken cancellationToken);

        Task<VehicleType> GetVehicleTypeAsync(long id, CancellationToken cancellationToken);
        Task<PagedResult<VehicleType>> GetVehicleTypesAsync(int? page, int? pageSize, CancellationToken cancellationToken);
        Task<List<VehicleTypeViewModel>> GetAllVehicleTypesAsync(CancellationToken cancellationToken);

        Task<List<VehicleRuleCategoryResultViewModel>> GetAllVehicleRuleCategoryAsync(CancellationToken cancellationToken);
        #endregion

        #region Delete
        Task<bool> DeleteVehicleTypeAsync(long id, CancellationToken cancellationToken);
        Task<bool> DeleteVehicleBrandAsync(long id, CancellationToken cancellationToken);
        Task<bool> DeleteVehicleAsync(long id, CancellationToken cancellationToken);
        Task<bool> DeleteVehicleDetailAsync(long id, CancellationToken cancellationToken);
        #endregion

        #region Update
        Task<VehicleType> UpdateVehicleTypeAsync(long id, VehicleTypeViewModel vehicleTypeViewModel, CancellationToken cancellationToken);
        Task<VehicleBrandResultViewModel> UpdateVehicleBrandAsync(long id, VehicleBrandInputViewModel vehicleBrandViewModel, CancellationToken cancellationToken);
        
        Task<VehicleDetail> UpdateVehicleDetailAsync(long id, VehicleDetailViewModel vehicleDetailViewModel, CancellationToken cancellationToken);
        #endregion

        #region Create
        Task<VehicleType> CreateVehicleTypeAsync(VehicleTypeViewModel vehicleTypeVM, CancellationToken cancellationToken);
        Task<VehicleBrandResultViewModel> CreateVehicleBrandAsync(VehicleBrandInputViewModel vehicleBrandVM, CancellationToken cancellationToken);
       
        Task<VehicleDetail> CreateVehicleDetailAsync(VehicleDetailViewModel vehicleDetailViewModel, CancellationToken cancellationToken);
        #endregion
        
        
        
        
        
        
        #region vehicle
        Task<VehicleResultViewModel> CreateVehicleAsync(VehicleInputViewModel vehicleViewModel, CancellationToken cancellationToken);
        Task<VehicleResultViewModel> UpdateVehicleAsync(long id, VehicleInputViewModel vehicleViewModel, CancellationToken cancellationToken);
        Task<VehicleResultViewModel> GetVehicleAsync(long id, CancellationToken cancellationToken);
        Task<List<VehicleResultViewModel>> GetAllVehiclesAsync(CancellationToken cancellationToken);
        Task<PagedResult<VehicleResultViewModel>> GetVehiclesAsync(int? page, int? pageSize, CancellationToken cancellationToken);
        Task<List<VehicleResultViewModel>> GetVehiclesByBrandAsync(long vehicleBrandId, CancellationToken cancellationToken);
        #endregion
    }
}
