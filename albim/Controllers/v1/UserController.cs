using System;
using albim.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.User;
using Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Services.SmsService;
using Common.Extensions;
using Common.Utilities;
using Models.PageAble;

namespace albim.Controllers.v1
{

    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UserController : BaseController
    {
        #region Property
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ISsoService _iSsoService;
        private readonly ISmsService _smsService;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public UserController(IUserService userService, IConfiguration configuration, ISsoService iSsoService, ISmsService smsService)
        {
            _userService = userService;
            _configuration = configuration;
            _iSsoService = iSsoService;
            _smsService = smsService;
        }
        #endregion



        [HttpPost("")]
        public async Task<ApiResult<UserResultViewModel>> CreateUser([FromBody] UserInputViewModel inputViewModel,CancellationToken cancellationToken)
        {
            return await _userService.Create(inputViewModel, cancellationToken);
        }
        [HttpGet("{user_code}")]
        public async Task<ApiResult<UserResultViewModel>> GetUser(Guid user_code, CancellationToken cancellationToken)
        {
            return await _userService.GetUser(user_code, cancellationToken);
        }
        [HttpGet("")]
        public async Task<ApiResult<PagedResult<UserResultViewModel>>> GetUsers([FromQuery] PageAbleResult pageAbleResult,CancellationToken cancellationToken)
        {
            return await _userService.GetUsers(pageAbleResult, cancellationToken);
        }
        [HttpPut("{user_code}")]
        public async Task<ApiResult<UserResultViewModel>> UpdateUser(Guid user_code, [FromBody] UserUpdateInputViewModel inputViewModel, CancellationToken cancellationToken)
        {
            return await _userService.Update(user_code, inputViewModel, cancellationToken);
        }
        [HttpDelete("{user_code}")]
        public async Task<bool> DeleteUser(Guid user_code, CancellationToken cancellationToken)
        {
            return await _userService.Delete(user_code, cancellationToken);
        }






        [HttpGet("GetData")]
        public async Task<ApiResult<List<UserInfoViewModel>>> GetData([FromQuery] string parameter,
         CancellationToken cancellationToken)
        {
            var res = await _userService.GetData(parameter, cancellationToken);
            return res;
        }


        [HttpGet("mine")]
        public async Task<ApiResult<UserInfoViewModel>> GetDataMine(CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            var res = await _userService.GetDataMine(userId, cancellationToken);
            return res;
        }

        // [HttpPut("mine")]
        // public async Task<ApiResult<UserInfoViewModel>> UpdateMine(UserViewModel userViewModel, CancellationToken cancellationToken)
        // {
        //     long userId = long.Parse(HttpContext.User.GetId());
        //
        //     var res = await _userService.UpdateMine(userId, userViewModel, cancellationToken);
        //     return res;
        // }

        [HttpPatch("{code}/Role/{role_id}")]
        public async Task<ApiResult<UserInfoViewModel>> AssignRole(Guid code, long role_id,
         CancellationToken cancellationToken)
        {
            var res = await _userService.AssignUserRole(code, role_id, cancellationToken);
            return res;
        }
        [HttpDelete("{code}/Role/{role_id}")]
        public async Task<ApiResult<UserInfoViewModel>> DeleteRole(Guid code, long role_id,
         CancellationToken cancellationToken)
        {
            var res = await _userService.DeleteUserRole(code, role_id, cancellationToken);
            return res;
        }


        [HttpPatch("{code}/changePassword")]
        public async Task<ApiResult<string>> ChangeUserPassword(Guid code,[FromBody] UserChangePasswordViewModel viewModel, CancellationToken cancellationToken)
        {
            return await _userService.ChangeUserPassword(code, viewModel, cancellationToken);
            
        }


        [HttpPatch("mine/changePassword")]
        public async Task<ApiResult<string>> ChangeUserPasswordMine([FromBody] MineUserChangePasswordViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _userService.ChangeUserPasswordMine(userId, viewModel, cancellationToken);
        }

        //[HttpGet("SmsMessage")]
        //public async Task<bool> SmsMessage(CancellationToken cancellationToken)
        //{
        //    string userId = HttpContext.User.GetId();

        //    return await _smsService.SendAlertMessage(long.Parse(userId), cancellationToken);
        //}
    }
}
