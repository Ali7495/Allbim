using albim.Result;
using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Place;
using Services;
using System.Threading;
using System.Threading.Tasks;

namespace albim.Controllers.v1
{

    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class PlaceController : BaseController
    {
        #region Property
        private readonly IConfiguration _configuration;
        private readonly IPlaceService _placeService;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public PlaceController(IConfiguration configuration, IPlaceService placeService)
        {
            _configuration = configuration;
            _placeService = placeService;
        }
        #endregion

        #region place Actions
        /// <summary>
        /// To Create a User
        /// </summary>c
        /// <param name="placeViewModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ApiResult<PlaceResultViewModel>> Create(PlaceInputViewModel placeViewModel, CancellationToken cancellationToken)
        {
            var place = await _placeService.Create(placeViewModel, cancellationToken);
            return place;
        }


        [HttpGet("{id}")]
        public async Task<ApiResult<PlaceResultViewModel>> GetDetail(long id, CancellationToken cancellationToken)
        {
            var place = await _placeService.Detail(id, cancellationToken);
            return place;
        }

        [HttpGet()]
        public async Task<ApiResult<PagedResult<PlaceResultViewModel>>> GetAll([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var people = await _placeService.Get(page, pageSize, cancellationToken);
            return people;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult<string>> Delete(int id, CancellationToken cancellationToken)
        {
            var res = await _placeService.Delete(id, cancellationToken);
            return res.ToString();
        }
        [HttpPut("{id}")]
        public async Task<ApiResult<PlaceResultViewModel>> Update(long Id, PlaceInputViewModel ViewModel, CancellationToken cancellationToken)
        {
            var result = await _placeService.Update(Id, ViewModel, cancellationToken);
            return result;
        }
        #endregion

        #region place address Actions
        /// <summary>
        /// To Create a User
        /// </summary>c
        /// <param name="ViewModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("{PlaceId}/Address")]
        public async Task<ApiResult<PlaceAddressResultViewModel>> CreatePlaceAddress(PlaceAddressInputViewModel ViewModel, CancellationToken cancellationToken)
        {
            var place = await _placeService.CreatePlaceAddress(ViewModel, cancellationToken);
            return place;
        }


        [HttpGet("{PlaceId}/Address")]
        public async Task<ApiResult<PlaceAddressResultViewModel>> GetDetailPlaceAddress(long id, CancellationToken cancellationToken)
        {
            var place = await _placeService.DetailPlaceAddress(id, cancellationToken);
            return place;
        }

        [HttpGet("{PlaceId}/Address")]
        public async Task<ApiResult<PagedResult<PlaceAddressResultViewModel>>> GetAllPlaceAddress([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var model = await _placeService.GetPlaceAddress(page, pageSize, cancellationToken);
            return model;
        }

        [HttpDelete("{PlaceId}/Address")]
        public async Task<ApiResult<string>> DeletePlaceAddress(int id, CancellationToken cancellationToken)
        {
            var res = await _placeService.DeletePlaceAddress(id, cancellationToken);
            return res.ToString();
        }
        [HttpPut("{PlaceId}/Address")]
        public async Task<ApiResult<PlaceAddressResultViewModel>> UpdatePlaceAddress(long Id, PlaceAddressInputViewModel ViewModel, CancellationToken cancellationToken)
        {
            var result = await _placeService.UpdatePlaceAddress(Id, ViewModel, cancellationToken);
            return result;
        }
        #endregion

    }
}
