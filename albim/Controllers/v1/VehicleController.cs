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
    public class VehicleController : BaseController
    {
        #region Property
        private readonly IConfiguration _configuration;
        private readonly IVehicleService _vehicleService;

        #endregion

        #region Constructor

        public VehicleController(IConfiguration configuration, IVehicleService vehicleService)
        {
            _configuration = configuration;
            _vehicleService = vehicleService;
        }
        #endregion

        #region Vehicle Actions
        
        [AllowAnonymous]
        [HttpGet("{vehicleId}")]
        public async Task<ApiResult<VehicleResultViewModel>> GetVehicle(long vehicleId, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleService.GetVehicleAsync(vehicleId, cancellationToken);
            return vehicle;
        }
        // [HttpGet("")]
        // public async Task<ApiResult<PagedResult<Vehicle>>> GetVehicles([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        // {
        //     var vehicles = await _vehicleService.GetVehiclesAsync(page, pageSize, cancellationToken);
        //     return vehicles;
        // }
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ApiResult<List<VehicleResultViewModel>>> GetAllVehicles(CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync(cancellationToken);
            return vehicles;
        }
        [HttpPost("")]
        public async Task<ApiResult<VehicleResultViewModel>> CreateVehicle(VehicleInputViewModel vehicleViewModel, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleService.CreateVehicleAsync(vehicleViewModel, cancellationToken);
            return vehicle;
        }
        [HttpPut("")]
        public async Task<ApiResult<VehicleResultViewModel>> UpdateVehicle(long vehicleId, VehicleInputViewModel vehicleViewModel, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleService.UpdateVehicleAsync(vehicleId, vehicleViewModel, cancellationToken);
            return vehicle;
        }
        [HttpDelete("{vehicleId}")]
        public async Task<ApiResult<string>> DeleteVehicle(long vehicleId, CancellationToken cancellationToken)
        {
            var result = await _vehicleService.DeleteVehicleAsync(vehicleId, cancellationToken);
            return result.ToString();
        }
        #endregion

      
        
    }
}
