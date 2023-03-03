using albim.Result;
using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Vehicle;
using Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace albim.Controllers.v1
{

    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class VehicleTypeController : BaseController
    {
        #region Property
        private readonly IConfiguration _configuration;
        private readonly IVehicleService _vehicleService;
        private readonly IVehicleApplicationService _vehicleApplicationService;

        #endregion

        #region Constructor

        public VehicleTypeController(IConfiguration configuration, IVehicleService vehicleService, IVehicleApplicationService vehicleApplicationService)
        {
            _configuration = configuration;
            _vehicleService = vehicleService;
            _vehicleApplicationService = vehicleApplicationService;
        }
        #endregion

        #region Vehicle Actions

        // [HttpGet("")]
        // public async Task<ApiResult<List<VehicleTypeViewModel>>> GetVehiclesTypes(CancellationToken cancellationToken)
        // {
        //     var vehicles = await _vehicleService.GetVehicleTypesAsync(cancellationToken);
        //     return vehicles;
        // }

       

        [AllowAnonymous]
        [HttpGet("{vehicleId}/brand/{vehicleBrandId}/vehicle")]
        public async Task<ApiResult<List<VehicleResultViewModel>>> GetVehiclesByBrand(long vehicleBrandId, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleService.GetVehiclesByBrandAsync(vehicleBrandId, cancellationToken);
            return vehicles;
        }
        #endregion

        #region VehicleType Actions
        [AllowAnonymous]
        [HttpGet("{vehicleTypeId}")]
        public async Task<ApiResult<VehicleType>> GetVehicleType(long vehicleTypeId, CancellationToken cancellationToken)
        {
            var vehicleType = await _vehicleService.GetVehicleTypeAsync(vehicleTypeId, cancellationToken);
            return vehicleType;
        }
        // [HttpGet("")]
        // public async Task<ApiResult<PagedResult<VehicleType>>> GetVehicleTypes([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        // {
        //     var vehicleTypes = await _vehicleService.GetVehicleTypesAsync(page, pageSize, cancellationToken);
        //     return vehicleTypes;
        // }
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ApiResult<List<VehicleTypeViewModel>>> GetAllVehicleTypes(CancellationToken cancellationToken)
        {
            var vehicleTypes = await _vehicleService.GetAllVehicleTypesAsync(cancellationToken);
            return vehicleTypes;
        }
        [HttpPost("")]
        public async Task<ApiResult<VehicleType>> CreateVehicleType(VehicleTypeViewModel VehicleTypeViewModel, CancellationToken cancellationToken)
        {
            var vehicleType = await _vehicleService.CreateVehicleTypeAsync(VehicleTypeViewModel, cancellationToken);
            return vehicleType;
        }
        [HttpPut("")]
        public async Task<ApiResult<VehicleType>> UpdateVehicleType(long VehicleTypeId, VehicleTypeViewModel VehicleTypeViewModel, CancellationToken cancellationToken)
        {
            var vehicleType = await _vehicleService.UpdateVehicleTypeAsync(VehicleTypeId, VehicleTypeViewModel, cancellationToken);
            return vehicleType;
        }
        [HttpDelete("{vehicleTypeId}")]
        public async Task<ApiResult<string>> DeleteVehicleType(long vehicleTypeId, CancellationToken cancellationToken)
        {
            var result = await _vehicleService.DeleteVehicleTypeAsync(vehicleTypeId, cancellationToken);
            return result.ToString();
        }
        #endregion

        #region VehicleApplication Actions
        [AllowAnonymous]
        [HttpGet("{Id}/application/{VehicleApplicationId}")]
        public async Task<ApiResult<VehicleApplicationResultViewModel>> Get([FromRoute]long Id, CancellationToken cancellationToken,[FromRoute]long VehicleApplicationId)
        {
            var result = await _vehicleApplicationService.Get(Id, cancellationToken,VehicleApplicationId);
            return result;
        }
        
        [AllowAnonymous]
        [HttpGet("{Id}/application")]
        public async Task<PagedResult<VehicleApplicationResultViewModel>> GetAll([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var result = await _vehicleApplicationService.GetAll( page,  pageSize, cancellationToken);
            return result;
        }
        [HttpPost("{Id}")]
        public async Task<VehicleApplicationResultViewModel> Create(VehicleApplicationInputViewModel vehicleApplicationInputViewModel, CancellationToken cancellationToken)
        {
            var result = await _vehicleApplicationService.Create(vehicleApplicationInputViewModel, cancellationToken);
            return result;
        }
        [HttpPut("{Id}/application")]
        public async Task<ApiResult<VehicleApplicationResultViewModel>> Update([FromRoute] long Id, VehicleApplicationInputViewModel vehicleApplicationInputViewModel, CancellationToken cancellationToken)
        {
            var result = await _vehicleApplicationService.Update(Id, vehicleApplicationInputViewModel, cancellationToken);
            return result;
        }
        [HttpDelete("{Id}/application/{VehicleApplicationId}")]
        public async Task<ApiResult<string>> Delete([FromRoute] long Id, CancellationToken cancellationToken,long VehicleApplicationId)
        {
            var result = await _vehicleApplicationService.Delete(Id, cancellationToken, VehicleApplicationId);
            return result.ToString();
        }
        #endregion

        
        
        #region vehicle brand
        [AllowAnonymous]
        [HttpGet("{vehicleId}/brand")]
        public async Task<ApiResult<List<VehicleBrandResultViewModel>>> GetVehiclesBrandsByType(long vehicleId, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleService.GetVehicleBrandsByTypeAsync(vehicleId, cancellationToken);
            return vehicles;
        }
        
        #endregion
    }
}
