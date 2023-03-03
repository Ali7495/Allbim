using Common.Exceptions;
using Common.Extensions;
using DAL.Contracts;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Albim.ActionFilters
{
    public class PermissionAttribute : TypeFilterAttribute
    {
        public PermissionAttribute(params string[] permissions) : base(typeof(PermissionAttributeImpl))
        {
            Arguments = new object[] {permissions};
        }

        private class PermissionAttributeImpl : IActionFilter,IAsyncActionFilter 
        {
            private string[] _permissions;
            private readonly IUserRepository _userRepository;
            private readonly IRolePermissionRepository _rolePermissionRepository;

            public PermissionAttributeImpl(IUserRepository userRepository,
                IRolePermissionRepository rolePermissionRepository, string[] permissions)
            {
                _userRepository = userRepository;
                _rolePermissionRepository = rolePermissionRepository;
                _permissions = permissions;
            }

            public void OnActionExecuting(ActionExecutingContext context)    
            {         
                  
            }
            
            public void OnActionExecuted(ActionExecutedContext context)    
            {         
              
            }
            
            public async Task OnActionExecutionAsync(ActionExecutingContext context,
                ActionExecutionDelegate next)
            {
                var claims = context.HttpContext.User.Claims.ToList();
                var UserRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).ToList();
                if (UserRole == null)
                    throw new BadRequestException("شما نقشی در این سیستم ندارید");
                List<RolePermission> UserRolesPermissions =
                    await _rolePermissionRepository.GetRolesPermissionsAsync(UserRole.Select(long.Parse).ToList());
      
                var PermissionsInUserRolesPermissions = UserRolesPermissions.Select(s => s.Permission.Name)
                    .Where(w => _permissions.Contains(w)).ToList();

                if (PermissionsInUserRolesPermissions.Count <= 0)
                    context.Result = new UnauthorizedResult();

                await next();
            }
        }
    }
}