using albim.Controllers;
using albim.Result;
using Common.Extensions;
using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Info;
using Services.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Models.PageAble;

namespace Albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [AllowAnonymous]
    public class InfoController : BaseController
    {
        #region Property
        private readonly IInfoService _infoService;
        #endregion
        #region Ctor
        public InfoController(IInfoService infoService)
        {
            _infoService = infoService;
        }
        #endregion
        #region Actions
        [HttpPost("")]
        public async Task<ApiResult<InfoResultViewModel>> Create([FromBody] InfoInputViewModel CreateViewModel, CancellationToken cancellationToken)
        {
            InfoResultViewModel ArticalData = await _infoService.Create(CreateViewModel, cancellationToken);
            return ArticalData;
        }
        [HttpDelete("{id}")]
        public async Task<ApiResult<string>> Delete(long id, CancellationToken cancellationToken)
        {
            bool res = await _infoService.Delete(id, cancellationToken);
            return res.ToString();
        }
        [HttpPut("{id}")]
        public async Task<ApiResult<InfoResultViewModel>> Update(long id, [FromBody] InfoInputViewModel UpdateViewModel, CancellationToken cancellationToken)
        {
            InfoResultViewModel Result = await _infoService.Update(id, UpdateViewModel, cancellationToken);
            return Result;
        }
        [HttpGet("{id}")]
        public async Task<ApiResult<InfoResultViewModel>> GetDetail(long id, CancellationToken cancellationToken)
        {
            InfoResultViewModel Result = await _infoService.Detail(id, cancellationToken);
            return Result;
        }
        //[HttpGet("")]
        //public async Task<ApiResult<PagedResult<InfoResultViewModel>>> GetAll([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        //{
        //    var ArticlesList = await _infoService.all(page, pageSize, cancellationToken);
        //    return ArticlesList;
        //}

        [HttpGet("list")]
        public async Task<ApiResult<List<InfoResultViewModel>>> GetAllWithoutPaging(CancellationToken cancellationToken)
        {
            return await _infoService.GetAllWithoutPaging(cancellationToken);
        }


        [HttpGet("")]
        public async Task<ApiResult<PagedResult<InfoResultViewModel>>> GetAll([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _infoService.GetAllPaging(pageAbleResult, cancellationToken);
        }


        #endregion

    }
}
