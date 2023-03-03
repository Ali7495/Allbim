using albim.Result;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Login;
using Models.Person;
using Models.User;
using Services;
using System.Threading;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Extensions;
using System.Net;

namespace albim.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class SsoController : BaseController
    {
        #region Property

        private readonly IConfiguration _configuration;
        private readonly ISsoService _iSsoService;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public SsoController(IConfiguration configuration, ISsoService iSsoService)
        {
            _configuration = configuration;
            _iSsoService = iSsoService;
        }

        #endregion

        #region Create User

        /// <summary>
        /// To Create a User
        /// </summary>
        /// <param name="registerViewModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost()]
        [AllowAnonymous]
        public async Task<ApiResult<string>> Create(RegisterViewModel registerViewModel,
            CancellationToken cancellationToken)
        {
            var user = await _iSsoService.Register(registerViewModel, cancellationToken);
            return user.Code.ToString();
        }

        #endregion

        #region User Login

        /// <summary>
        /// Login Authentication
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ApiResult<string>> Login([FromBody] LoginViewModel loginViewModel, CancellationToken cancellationToken)
        {
            var jwt = await _iSsoService.Login(loginViewModel, cancellationToken);
            return jwt;
        }
        [HttpPost("login_Dashboard")]
        [AllowAnonymous]
        public async Task<ApiResult<string>> login_Dashboard(LoginViewModel loginViewModel, CancellationToken cancellationToken)
        {
            var jwt = await _iSsoService.Login_Dashboard(loginViewModel, cancellationToken);
            return jwt;
        }


        [HttpPatch("verification_code")]
        [AllowAnonymous]
        public async Task<ApiResult<string>> VerificationCode(VerificationViewModel ViewModel,
            CancellationToken cancellationToken)
        {
            var res = await _iSsoService.VerificationCode(ViewModel, cancellationToken);
            return res.ToString();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<UserResultViewModel> Register(RegisterViewModel ViewModel, CancellationToken cancellationToken)
        {
            var res = _iSsoService.RegisterFromApi(ViewModel, cancellationToken);
            return await res;
        }

        [HttpGet("check_phone/{username}")]
        [AllowAnonymous]
        public async Task<ApiResult<string>> CheckPhone([FromRoute] string username,
            CancellationToken cancellationToken)
        {
            IPAddress Ip = Request.HttpContext.Connection.RemoteIpAddress;

            var res = await _iSsoService.CheckPhone(username, Ip.ToString(), cancellationToken);
            return res;
        }

        [HttpPatch("check_verification")]
        [AllowAnonymous]
        public async Task<ApiResult<string>> CheckVerification(CheckVerificationViewModel model,
            CancellationToken cancellationToken)
        {
            var res = await _iSsoService.CheckVerification(model, cancellationToken);
            return res;
        }

        [HttpPatch("two_step/{username}")]
        [AllowAnonymous]
        public async Task<ApiResult<string>> TwoStep([FromRoute] string username, CancellationToken cancellationToken)
        {
            var res = await _iSsoService.TwoStep(username, cancellationToken);
            return res;
        }

        [HttpPatch("check_two_step")]
        [AllowAnonymous]
        public async Task<ApiResult<string>> CheckTwoStep(CheckVerificationViewModel model,
            CancellationToken cancellationToken)
        {
            var res = await _iSsoService.CheckTwoStep(model, cancellationToken);
            return res;
        }

        [HttpPatch("change_password/{username}")]
        [AllowAnonymous]
        public async Task<ApiResult<string>> ChangePassword(ChangePasswordViewModel mdoel, [FromRoute] string username,
            CancellationToken cancellationToken)
        {
            var res = await _iSsoService.ChangePassword(mdoel, username, cancellationToken);
            return res;
        }


        [HttpGet("info")]
        public async Task<ApiResult<UserInfoViewModel>> UserInfo(CancellationToken cancellationToken)
        {
            var user = HttpContext.User.GetId();
            if (user == null)
            {
                throw new NotFoundException("user");
            }
            int userId = int.Parse(user);
            UserInfoViewModel result = await _iSsoService.GetUserInfo(userId, cancellationToken);
            return result;

            // var res = await _iSsoService.ChangePassword(mdoel, username, cancellationToken);
            // return res;
        }

        #endregion
    }
}