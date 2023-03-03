using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using albim.Controllers;
using albim.Result;
using Common.Exceptions;
using Common.Extensions;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Role;
using Models.RolePermission;
using Services;


namespace albim.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class RoleController : BaseController
    {
        #region Property
        //private readonly IUserService _userService;

        private readonly IRoleService _roleService;
        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        #endregion


        #region Role
        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<ApiResult<List<RoleResultViewModel>>> GetRoleList( CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            long roleId = long.Parse(userRole);
            var role = await _roleService.GetListByUserId(userId,roleId, cancellationToken);

            return role;
        }
        
        [AllowAnonymous]
        [HttpGet("{roleId}")]
        public async Task<ApiResult<RoleResultViewModel>> GetRoles(long id, CancellationToken cancellationToken)
        {
            var role = await _roleService.GetRole(id, cancellationToken);

            return role;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ApiResult<RoleResultViewModel>> CreateRole(RoleInputViewModel roleViewModel, CancellationToken cancellationToken)
        {
            var role = await _roleService.Create(roleViewModel, cancellationToken);

            return role;
        }
        

        [AllowAnonymous]
        [HttpPut("")]
        public async Task<ApiResult<RoleResultViewModel>> UpdateRole(long id, RoleInputViewModel roleViewModel, CancellationToken cancellationToken)
        {
            var role = await _roleService.Update(id, roleViewModel, cancellationToken);

            return role;
        }

        [AllowAnonymous]
        [HttpDelete("{roleId}")]
        public async Task<ApiResult<string>> DeleteRole(long id, CancellationToken cancellationToken)
        {
            bool result = await _roleService.Delete(id, cancellationToken);

            return result.ToString();
        }

        #endregion


        #region RolePermission


        [AllowAnonymous]
        [HttpPost("{roleId}/Permission/{permissionId}")]
        public async Task<ApiResult<RolePermissionResultViewModel>> CreateRolePermission(RolePermissionInputViewModel rolePermissionViewModel, CancellationToken cancellationToken)
        {
            var rolePermission = await _roleService.CreateRolePermission(rolePermissionViewModel, cancellationToken);

            return rolePermission;
        }

        [AllowAnonymous]
        [HttpPut("{roleId}/Permission/{permissionId}")]
        public async Task<ApiResult<RolePermissionResultViewModel>> UpdateRolePermission(long id, RolePermissionInputViewModel rolePermissionViewModel, CancellationToken cancellationToken)
        {
            var rolePermission = await _roleService.UpdateRolePermission(id, rolePermissionViewModel, cancellationToken);

            return rolePermission;
        }

        [AllowAnonymous]
        [HttpPut("{roleId}/Permission")]
        public async Task<ApiResult<string>> UpdatePermissions(long roleId, long[] permissionIds, CancellationToken cancellationToken)
        {
            bool result = await _roleService.UpdatePermissions(roleId, permissionIds, cancellationToken);

            return result.ToString();
        }

        [AllowAnonymous]
        [HttpDelete("{rolePermissionId}")]
        public async Task<ApiResult<string>> DeleteRolePermission(long id, CancellationToken cancellationToken)
        {
            bool result = await _roleService.DeleteRolePermission(id, cancellationToken);

            return result.ToString();
        }

        #endregion
    }
}
