using albim.Result;
using Albim.ActionFilters;
using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.City;
using Services.City;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Township;
using Models.Upload;

namespace albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [AllowAnonymous]
    public class TownshipController : BaseController
    {
        #region Fields
        private readonly ICityService _cityService;
        #endregion

        #region CTOR

        public TownshipController(ICityService cityService)
        {
            _cityService = cityService;
        }
        #endregion

        #region TownShip Actions
        [HttpPost("")]
        public async Task<ApiResult<TownshipResultViewModel>> CreateTownShip(TownshipInputViewModel townShipViewModel, CancellationToken cancellationToken)
        {
            var result = await _cityService.CreateTownShipAsync(townShipViewModel, cancellationToken);
            return result;
        }
        [HttpDelete("{townShipId}")]
        public async Task<ApiResult<string>> DeleteTownShip(long townShipId, CancellationToken cancellationToken)
        {
            var result = await _cityService.DeleteTownShipAsync(townShipId, cancellationToken);
            return result.ToString();
        }
        [HttpPut("{townShipId}")]
        public async Task<ApiResult<TownshipResultViewModel>> UpdateTownShip(long townShipId, TownshipInputViewModel townShipViewModel, CancellationToken cancellationToken)
        {
            var result = await _cityService.UpdateTownShipAsync(townShipId, townShipViewModel, cancellationToken);
            return result;
        }
        [HttpGet("all")]
        public async Task<ApiResult<List<TownshipResultViewModel>>> GetTownShipList(CancellationToken cancellationToken)
        {
            var result = await _cityService.GetTownShipListAsync(cancellationToken);
            return result;
        }
        [HttpGet("")]
        public async Task<ApiResult<PagedResult<TownshipResultViewModel>>> GetTownShipPaging([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var result = await _cityService.GetTownShipPagingAsync(page, pageSize, cancellationToken);
            return result;
        }
        [HttpGet("{townShipId}")]
        public async Task<ApiResult<TownshipResultViewModel>> GetTownShipDetail(long townShipId, CancellationToken cancellationToken)
        {
            var result = await _cityService.GetTownShipDetailAsync(townShipId, cancellationToken);
            return result;
        }
        #endregion
    }
}

