using albim.Controllers;
using albim.Result;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Permission;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Albim.Controllers.v1
{
    public class PermissionController : BaseController
    {

        #region fields

        private readonly IPermissionService _permissionService;

        #endregion

        #region CTOR

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        #endregion

        [AllowAnonymous]
        [HttpGet("{permissionId}")]
        public async Task<ApiResult<PermissionResultViewModel>> GetPermission(long id, CancellationToken cancellationToken)
        {
            PermissionResultViewModel permission = await _permissionService.Get(id, cancellationToken);

            return permission;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ApiResult<PermissionResultViewModel>> CreatePermission(PermissionInputViewModel permissionViewModel, CancellationToken cancellationToken)
        {
            var permission = await _permissionService.Create(permissionViewModel, cancellationToken);

            return permission;
        }


        [AllowAnonymous]
        [HttpPut("")]
        public async Task<ApiResult<PermissionResultViewModel>> UpdatePermission(long id,PermissionInputViewModel permissionViewModel, CancellationToken cancellationToken)
        {
            var permission = await _permissionService.Update(id, permissionViewModel, cancellationToken);

            return permission;
        }

        [AllowAnonymous]
        [HttpDelete("{permissionId}")]
        public async Task<ApiResult<string>> DeletePermission(long id, CancellationToken cancellationToken)
        {
            bool result = await _permissionService.Delete(id, cancellationToken);

            return result.ToString();
        }
    }
}
