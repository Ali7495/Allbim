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
    public class VehicleApplicationController : BaseController
    {
        #region Property
        private readonly IConfiguration _configuration;
        private readonly IVehicleApplicationService _vehicleApplicationService;

        #endregion

        #region Constructor

        public VehicleApplicationController(IConfiguration configuration, IVehicleApplicationService vehicleApplicationService)
        {
            _configuration = configuration;
            _vehicleApplicationService = vehicleApplicationService; 
        }
        #endregion

        #region VehicleApplication Actions
        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ApiResult<List<VehicleApplicationResultViewModel>>> Get( CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleApplicationService.GetVehicleApplicationsAsync( cancellationToken);
            return vehicles;
        }

       
        #endregion


    }
}
