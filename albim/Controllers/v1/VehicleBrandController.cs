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
    public class VehicleBrandController : BaseController
    {
        #region Property
        private readonly IConfiguration _configuration;
        private readonly IVehicleService _vehicleService;

        #endregion

        #region Constructor

        public VehicleBrandController(IConfiguration configuration, IVehicleService vehicleService)
        {
            _configuration = configuration;
            _vehicleService = vehicleService;
        }
        #endregion
        
      
        #region VehicleBrand Actions
        [AllowAnonymous]
        [HttpGet("{vehicleBrandId}")]
        public async Task<ApiResult<VehicleBrandResultViewModel>> GetVehicleBrand(long vehicleBrandId, CancellationToken cancellationToken)
        {
            var vehicleBrand = await _vehicleService.GetVehicleBrandAsync(vehicleBrandId, cancellationToken);
            return vehicleBrand;
        }
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ApiResult<List<VehicleBrandResultViewModel>>> GetVehicleBrands([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            // var vehicleBrands = await _vehicleService.GetVehicleBrandsAsync(page, pageSize, cancellationToken);
            var vehicleBrands = await _vehicleService.GetAllVehicleBrandsAsync(cancellationToken);
            return vehicleBrands;
        }
        // [HttpGet("all")]
        // public async Task<ApiResult<List<VehicleBrand>>> GetAllVehicleBrands(CancellationToken cancellationToken)
        // {
        //     var vehicleBrands = await _vehicleService.GetAllVehicleBrandsAsync(cancellationToken);
        //     return vehicleBrands;
        // }
        [HttpPost("")]
        public async Task<ApiResult<VehicleBrandResultViewModel>> CreateVehicleBrand(VehicleBrandInputViewModel vehicleBrandViewModel, CancellationToken cancellationToken)
        {
            var vehicleBrand = await _vehicleService.CreateVehicleBrandAsync(vehicleBrandViewModel, cancellationToken);
            return vehicleBrand;
        }
        [HttpPut("")]
        public async Task<ApiResult<VehicleBrandResultViewModel>> UpdateVehicleBrand(long vehicleBrandId, VehicleBrandInputViewModel vehicleBrandViewModel, CancellationToken cancellationToken)
        {
            var vehicleBrand = await _vehicleService.UpdateVehicleBrandAsync(vehicleBrandId, vehicleBrandViewModel, cancellationToken);
            return vehicleBrand;
        }
        [HttpDelete("{vehicleBrandId}")]
        public async Task<ApiResult<string>> DeleteVehicleBrand(long vehicleBrandId, CancellationToken cancellationToken)
        {
            var result = await _vehicleService.DeleteVehicleBrandAsync(vehicleBrandId, cancellationToken);
            return result.ToString();
        }
        #endregion
        
    }
}
