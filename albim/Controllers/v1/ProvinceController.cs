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

namespace albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [AllowAnonymous]
    public class ProvinceController : BaseController
    {
        #region Fields
        private readonly ICityService _cityService;
        #endregion

        #region CTOR

        public ProvinceController(ICityService cityService)
        {
            _cityService = cityService;
        }
        #endregion
        
        #region Province Actions
        [HttpPost("")]
        public async Task<ApiResult<ProvinceResultViewModel>> CreateProvince(ProvinceInputViewModel provinceViewModel, CancellationToken cancellationToken)
        {
            var result = await _cityService.CreateProvinceAsync(provinceViewModel, cancellationToken);
            return result;
        }
        [HttpDelete("{provinceId}")]
        public async Task<ApiResult<string>> DeleteProvince(long provinceId, CancellationToken cancellationToken)
        {
            var result = await _cityService.DeleteProvinceAsync(provinceId, cancellationToken);
            return result.ToString();
        }
        [HttpPut("{provinceId}")]
        public async Task<ApiResult<ProvinceResultViewModel>> UpdateProvince(long provinceId, ProvinceInputViewModel provinceViewModel, CancellationToken cancellationToken)
        {
            var result = await _cityService.UpdateProvinceAsync(provinceId, provinceViewModel, cancellationToken);
            return result;
        }
        [HttpGet("all")]
        public async Task<ApiResult<List<ProvinceResultViewModel>>> GetAllProvinces(CancellationToken cancellationToken)
        {
            var result = await _cityService.GetAllProvincesAsync(cancellationToken);
            return result;
        }
        [HttpGet("")]
        public async Task<ApiResult<PagedResult<ProvinceResultViewModel>>> GetProvinces([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var result = await _cityService.GetProvincesAsync(page, pageSize, cancellationToken);
            return result;
        }
        [HttpGet("{provinceId}")]
        public async Task<ApiResult<ProvinceResultViewModel>> GetProvince(long provinceId, CancellationToken cancellationToken)
        {
            var result = await _cityService.GetProvinceAsync(provinceId, cancellationToken);
            return result;
        }
        #endregion
        
    }
}

