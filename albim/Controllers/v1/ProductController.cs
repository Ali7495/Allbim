using albim.Controllers;
using albim.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Insurance;
using Models.QueryParams;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using albim.Builder;
using Microsoft.AspNetCore.Http;
using Models.PolicyRequest;
using Models.Product;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Services.PolicyRequest;

namespace Albim.Controllers.v1
{
    public class ProductController : BaseController
    {
        private readonly IPolicyRequestService _policyRequestService;

        public ProductController(IPolicyRequestService policyRequestService)
        {
            _policyRequestService = policyRequestService;
        }

        [AllowAnonymous]
        [HttpGet("third/price")] 
        public async Task<ApiResult<List<ThirdInsuranceResultViewModel>>> GetThirdProducts(string slug, [FromQuery] ThirdProductInputViewModel thirdProductInputViewModel, CancellationToken cancellationToken)
        {
            return await _policyRequestService.GetAvailableThirdInsuranceInsurers(thirdProductInputViewModel, cancellationToken);
        }
        
        [AllowAnonymous]
        [HttpGet("{slug}/price")]
        public async Task<ApiResult<List<BodyInsuranceResultViewModel>>> GetBodyProduct(string slug, [FromQuery] BodyProductInputViewModel bodyProductInputViewModel, CancellationToken cancellationToken)
        {

            return await _policyRequestService.GetAvailableBodyInsurers(slug, bodyProductInputViewModel, cancellationToken);
        }

    }
}
