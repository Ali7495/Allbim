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
using System.Threading;
using System.Threading.Tasks;
using Services.ViewModels.Menu;
using Common.Extensions;

namespace Albim.Controllers.v1
{
    public class MenuController : BaseController
    {
        #region fields

        private readonly IMenuService _menuService;

        #endregion

        #region CTOR

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        #endregion

        [AllowAnonymous]
        [HttpGet("{MenuId}")]
        public async Task<ApiResult<MenuResultViewModel>> GetMenu(long id, CancellationToken cancellationToken)
        {
            MenuResultViewModel menuViewModel = await _menuService.Get(id, cancellationToken);

            return menuViewModel;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ApiResult<Menu>> CreateMenu(MenuInputViewModel menuViewModel, CancellationToken cancellationToken)
        {
            Menu model = await _menuService.Create(menuViewModel, cancellationToken);

            return model;
        }


        [AllowAnonymous]
        [HttpPut("")]
        public async Task<ApiResult<Menu>> UpdateMenu(long id, MenuInputViewModel menuViewModel, CancellationToken cancellationToken)
        {
            Menu model = await _menuService.Update(id, menuViewModel, cancellationToken);

            return model;
        }

        [AllowAnonymous]
        [HttpDelete("{MenuId}")]
        public async Task<ApiResult<string>> DeleteMenu(long id, CancellationToken cancellationToken)
        {
            bool result = await _menuService.Delete(id, cancellationToken);

            return result.ToString();
        }


        [HttpGet("")]
        public async Task<ApiResult<List<MenuTreeItemViewModel>>> AdminMenu(CancellationToken cancellationToken)
        {
            long? userId = long.Parse(HttpContext.User.Identity.GetUserId());
            return await _menuService.GetAdminMenu(userId,cancellationToken);
        }

    }
}
