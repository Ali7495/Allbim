using albim.Controllers;
using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Discount;
using Models.PageAble;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using albim.Result;

namespace Albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [AllowAnonymous]
    public class DiscountController : BaseController
    {
        #region Fields

        private readonly IDiscountServices _discountServices;

        #endregion

        #region CTOR

        public DiscountController(IDiscountServices discountServices)
        {
            _discountServices = discountServices;
        }

        #endregion

        
        [HttpPost("")]
        public async Task<ApiResult<DiscountResultViewModel>> CreateDiscount(DiscountInputViewModel viewModel, CancellationToken cancellationToken)
        {
            return await _discountServices.CreateDiscount(viewModel, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<DiscountResultViewModel>> GetDiscount(long id, CancellationToken cancellationToken)
        {
            return await _discountServices.GetDiscount(id, cancellationToken);
        }

        [HttpGet("")]
        public async Task<ApiResult<PagedResult<DiscountResultViewModel>>> GetAllDiscounts([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _discountServices.GetAllDiscounts(pageAbleResult, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<ApiResult<DiscountResultViewModel>> UpdateDiscount(long id, DiscountInputViewModel viewModel, CancellationToken cancellationToken)
        {
            return await _discountServices.UpdateDiscount(id, viewModel, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult<string>> DeleteDiscount(long id, CancellationToken cancellationToken)
        {
            var result = await _discountServices.DeleteDiscount(id, cancellationToken);
            return result.ToString();
        }
    }
}
