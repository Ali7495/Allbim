using albim.Controllers;
using albim.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Vehicle;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class VehicleRuleCategoryController : BaseController
    {
        private readonly IVehicleService _vehicleService;

        public VehicleRuleCategoryController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [AllowAnonymous]
        [HttpGet("")]
        public async Task<ApiResult<List<VehicleRuleCategoryResultViewModel>>> GetAllVehicleRuleCategories(CancellationToken cancellationToken)
        {
            List<VehicleRuleCategoryResultViewModel> vehicleTypes = await _vehicleService.GetAllVehicleRuleCategoryAsync(cancellationToken);
            return vehicleTypes;
        }
    }
}
