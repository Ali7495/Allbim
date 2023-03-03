using albim.Controllers;
using albim.Result;
using Common.Extensions;
using Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Models.Factor;
using Models.PageAble;
using Services.FactorServices;
using Services.PolicyRequest;
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
    public class FactorController : BaseController
    {
        private readonly IFactorServices _factorServices;

        public FactorController(IFactorServices factorServices)
        {
            _factorServices = factorServices;
        }

        
        [HttpGet("mine")]
        public async Task<ApiResult<PagedResult<FactorViewModel>>> GetAllFactorsMine([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PagedResult<FactorViewModel> result = await _factorServices.GetAllFactorsMine(userId, pageAbleResult, cancellationToken);
            return result;
        }

        [HttpGet("mine/{id}")]
        public async Task<ApiResult<FactorViewModel>> GetFactorMine(long id, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            FactorViewModel result = await _factorServices.GetFactorMine(userId, id, cancellationToken);
            return result;
        }
    }
}
