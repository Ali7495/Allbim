using albim.Result;
using Albim.ActionFilters;
using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Models.City;
using Services.City;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Albim.Resources;
using DAL.Contracts;
using Models.Township;
using Models.Upload;

namespace albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [AllowAnonymous]
    public class CityController : BaseController
    {
        #region Fields
        private readonly ICityService _cityService;
        public  IRolePermissionRepository _rolePermissionRepository;
        // private readonly IStringLocalizer<Resource> _stringLocalize;
        #endregion

        #region CTOR

        public CityController(IRolePermissionRepository rolePermissionRepository , ICityService cityService
            // ,IStringLocalizer<Resource> stringLocalize
            )
        {
            _cityService = cityService;
            // _stringLocalize = stringLocalize;
            _rolePermissionRepository = rolePermissionRepository;
        }
        #endregion
        // [HttpGet("getstring")]
        // public string get1()
        // {
        //     // return _stringLocalize["NotFount"];
        // }
        #region City Actions
        [HttpPost("")]
        public async Task<ApiResult<CityResultViewModel>> CreateCity(CityInputViewModel cityViewModel, CancellationToken cancellationToken)
        {
            var result = await _cityService.CreateCityAsync(cityViewModel, cancellationToken);
            return result;
        }

        [HttpDelete("{cityId}")]
        public async Task<ApiResult<string>> DeleteCity(long cityId, CancellationToken cancellationToken)
        {
            var result = await _cityService.DeleteCityAsync(cityId, cancellationToken);
            return result.ToString();
        }

        [HttpPut("{cityId}")]
        public async Task<ApiResult<CityResultViewModel>> UpdateCity(long cityId, CityInputViewModel cityViewModel, CancellationToken cancellationToken)
        {
            var result = await _cityService.UpdateCityAsync(cityId, cityViewModel, cancellationToken);
            return result;
        }

        [HttpGet("")]
        // [Permission( "admin-city", "edit-city", "view-city")]
        public async Task<ApiResult<List<CityResultViewModel>>> GetAllCities(CancellationToken cancellationToken)
        {
            var result = await _cityService.GetAllCitiesAsync(cancellationToken);
            return result;
        }

        // [HttpGet("{page}/{pageSize}")]
        // public async Task<ApiResult<PagedResult<City>>> GetCities(int page, [FromQuery] int? pageSize, [FromQuery] string orderBy, CancellationToken cancellationToken)
        // {
        //     var result = await _cityService.GetCitiesAsync(page, pageSize, orderBy, cancellationToken);
        //     return result;
        // }


        [HttpGet("{cityId}")]
        [TypeFilter(typeof(RedisCachableAttribute), Arguments = new object[] { 25, false })]
        public async Task<ApiResult<CityResultViewModel>> GetCity(long cityId, CancellationToken cancellationToken)
        {
            var result = await _cityService.GetCityAsync(cityId, cancellationToken);
            return result;
        }
        #endregion

        #region Province Actions
        [HttpPost("/Province")]
        public async Task<ApiResult<ProvinceResultViewModel>> CreateProvince(ProvinceInputViewModel provinceViewModel, CancellationToken cancellationToken)
        {
            var result = await _cityService.CreateProvinceAsync(provinceViewModel, cancellationToken);
            return result;
        }
        [HttpDelete("/Province/{provinceId}")]
        public async Task<ApiResult<string>> DeleteProvince(long provinceId, CancellationToken cancellationToken)
        {
            var result = await _cityService.DeleteProvinceAsync(provinceId, cancellationToken);
            return result.ToString();
        }
        [HttpPut("/Province/{provinceId}")]
        public async Task<ApiResult<ProvinceResultViewModel>> UpdateProvince(long provinceId, ProvinceInputViewModel provinceViewModel, CancellationToken cancellationToken)
        {
            var result = await _cityService.UpdateProvinceAsync(provinceId, provinceViewModel, cancellationToken);
            return result;
        }
        [HttpGet("AllProvince")]
        public async Task<ApiResult<List<ProvinceResultViewModel>>> GetAllProvinces(CancellationToken cancellationToken)
        {
            var result = await _cityService.GetAllProvincesAsync(cancellationToken);
            return result;
        }
        [HttpGet("/Province")]
        public async Task<ApiResult<PagedResult<ProvinceResultViewModel>>> GetProvinces([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var result = await _cityService.GetProvincesAsync(page, pageSize, cancellationToken);
            return result;
        }
        [HttpGet("/Province/{provinceId}")]
        public async Task<ApiResult<ProvinceResultViewModel>> GetProvince(long provinceId, CancellationToken cancellationToken)
        {
            var result = await _cityService.GetProvinceAsync(provinceId, cancellationToken);
            return result;
        }
        #endregion

        #region TownShip Actions
        [HttpPost("/TownShip")]
        public async Task<ApiResult<TownshipResultViewModel>> CreateTownShip(TownshipInputViewModel townShipViewModel, CancellationToken cancellationToken)
        {
            var result = await _cityService.CreateTownShipAsync(townShipViewModel, cancellationToken);
            return result;
        }
        [HttpDelete("/TownShip/{townShipId}")]
        public async Task<ApiResult<string>> DeleteTownShip(long townShipId, CancellationToken cancellationToken)
        {
            var result = await _cityService.DeleteTownShipAsync(townShipId, cancellationToken);
            return result.ToString();
        }
        [HttpPut("/TownShip/{townShipId}")]
        public async Task<ApiResult<TownshipResultViewModel>> UpdateTownShip(long townShipId, TownshipInputViewModel townShipViewModel, CancellationToken cancellationToken)
        {
            var result = await _cityService.UpdateTownShipAsync(townShipId, townShipViewModel, cancellationToken);
            return result;
        }
        [HttpGet("/AllTownShip")]
        public async Task<ApiResult<List<TownshipResultViewModel>>> GetAllTownShips(CancellationToken cancellationToken)
        {
            var result = await _cityService.GetTownShipListAsync(cancellationToken);
            return result;
        }
        [HttpGet("/TownShip")]
        public async Task<ApiResult<PagedResult<TownshipResultViewModel>>> GetTownShips([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var result = await _cityService.GetTownShipPagingAsync(page, pageSize, cancellationToken);
            return result;
        }
        [HttpGet("/TownShip/{townShipId}")]
        public async Task<ApiResult<TownshipResultViewModel>> GetTownShip(long townShipId, CancellationToken cancellationToken)
        {
            var result = await _cityService.GetTownShipDetailAsync(townShipId, cancellationToken);
            return result;
        }
        #endregion
    }
}

