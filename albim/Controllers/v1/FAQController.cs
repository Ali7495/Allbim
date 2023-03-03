using albim.Controllers;
using albim.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.PublicFaq;
using Services.PublicFaq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using Models.PageAble;

namespace Albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class FAQController : BaseController
    {
        #region Property
        private readonly IPublicFaqService _publicFaqService;
        #endregion
        #region Ctor
        public FAQController(IPublicFaqService publicFaqService)
        {
            _publicFaqService = publicFaqService;
        }
        #endregion

        #region Actions
        [HttpPost("")]
        public async Task<ApiResult<PublicFaqResultViewModel>> Create([FromBody] PublicFaqInputViewModel CreateViewModel, CancellationToken cancellationToken)
        {
            return await _publicFaqService.Create(CreateViewModel, cancellationToken);
        }
        [HttpDelete("{id}")]
        public async Task<ApiResult<string>> Delete(long id, CancellationToken cancellationToken)
        {
            bool res = await _publicFaqService.Delete(id, cancellationToken);
            return res.ToString();
        }
        [HttpPut("{id}")]
        public async Task<ApiResult<PublicFaqResultViewModel>> Update(long id, [FromBody] PublicFaqInputViewModel UpdateViewModel, CancellationToken cancellationToken)
        {
            return await _publicFaqService.Update(id, UpdateViewModel, cancellationToken);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ApiResult<PublicFaqResultViewModel>> GetDetail(long id, CancellationToken cancellationToken)
        {
            return await _publicFaqService.Detail(id, cancellationToken);
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<ApiResult<List<PublicFaqResultViewModel>>> GetAllWithoutPaging(CancellationToken cancellationToken)
        {
            return await _publicFaqService.GetAllWithoutPaging(cancellationToken);
        }


        [HttpGet("")]
        [AllowAnonymous]
        public async Task<ApiResult<PagedResult<PublicFaqResultViewModel>>> GetAllPaging([FromQuery] PageAbleResult pageAbleResult,CancellationToken cancellationToken)
        {
            return await _publicFaqService.GetAllPaging(pageAbleResult,cancellationToken);
        }


        #endregion

    }
}
