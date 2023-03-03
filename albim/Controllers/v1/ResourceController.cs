using albim.Controllers;
using albim.Result;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Services.ViewModels.Menu;
using Common.Extensions;
using Models.Resource;

namespace Albim.Controllers.v1
{
    public class ResourceController : BaseController
    {
        #region fields

        private readonly IResourceOperationService _resourceOperationService;

        #endregion

        #region CTOR

        public ResourceController(IResourceOperationService resourceOperationService)
        {
            _resourceOperationService = resourceOperationService;
        }

        #endregion
        
        [HttpGet("{ResourceName}/operation")]
        public async Task<ApiResult<List<ResourceOperationViewModel>>> GetMenu([FromRoute]string ResourceName, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            long roleId = long.Parse(userRole);

            var output = await _resourceOperationService.GetByResourceName(ResourceName,userId,roleId, cancellationToken);

            return output;
        }



    }
}
