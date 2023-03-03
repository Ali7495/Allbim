using DAL.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Login;
using Models.Person;
using Models.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Exceptions;
using SmsIrRestfulNetCore;
using Models.User;
using Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Common.Utilities;
using Models.SMS;
using Services.SmsService;

namespace Services
{
    public class SsoService : ISsoService
    {
        private readonly SiteSettings _siteSetting;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly ISmsService _smsService;
        private readonly IRegisterTempRepository _registerTempRepository;
        private readonly IMapper _mapper;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public SsoService(IOptionsSnapshot<SiteSettings> settings, IUserRepository userRepository,
            IRepository<Person> personRepository, ISmsService smsService,
            IRegisterTempRepository registerTempRepository, IMapper mapper, IRolePermissionRepository rolePermissionRepository)
        {
            _siteSetting = settings.Value;
            _userRepository = userRepository;
            _personRepository = personRepository;
            _smsService = smsService;
            _registerTempRepository = registerTempRepository;
            _mapper = mapper;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public Task<bool> Validate(LoginViewModel loginViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(RegisterViewModel registerViewModel, CancellationToken cancellationToken)
        {
            RegisterTemp registerTemp =
                await _registerTempRepository.GetRegisterTempByCodeAndMobile(registerViewModel.Username,
                    registerViewModel.Code, cancellationToken);
            User oldUser = await _userRepository.GetByUserName(registerViewModel.Username);
            if (oldUser != null)
            {
                throw new BadRequestException("این نام کاربری قبلا ثبت شده است");
            }
            if (registerTemp != null)
            {
                Person person = new Person
                {
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    NationalCode = registerViewModel.NationalCode,
                    Identity = registerViewModel.Identity,
                    FatherName = registerViewModel.FatherName,
                    BirthDate =
                        registerViewModel.BirthDate != null ? DateTime.Parse(registerViewModel.BirthDate) : null,
                    GenderId = registerViewModel.GenderId,
                    MarriageId = registerViewModel.MarriageId,
                    MillitaryId = registerViewModel.MillitaryId
                };
                await _personRepository.AddAsync(person, cancellationToken);
                User user = new User
                {
                    Username = registerViewModel.Username,
                    Password = SecurityHelper.GetSha256Hash(registerViewModel.Password),
                    Email = registerViewModel.Email,
                    PersonId = person.Id
                };
                await _userRepository.AddAsync(user, registerViewModel.Password, cancellationToken);

                return user;
            }
            else
            {
                throw new BadRequestException("کد نا معتبر است");
            }
        }
        public async Task<UserResultViewModel> RegisterFromApi(RegisterViewModel registerViewModel, CancellationToken cancellationToken)
        {
            var user = await Register(registerViewModel, cancellationToken);
            return _mapper.Map<UserResultViewModel>(user);
           
        }

        public async Task<string> Login(LoginViewModel loginViewModel, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUserAndPass(loginViewModel.UserName, loginViewModel.Password,
                cancellationToken);
            if (user == null)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            var jwt = await GenerateAsync(user);
            user.LastLogOnDate = DateTime.Now;
            await _userRepository.UpdateAsync(user, cancellationToken);
            return jwt;
        }
        public async Task<string> Login_Dashboard(LoginViewModel loginViewModel, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUserAndPassWithRoles(loginViewModel.UserName, loginViewModel.Password, cancellationToken);
            if (user == null)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");
            
            List<RolePermission> UserRolesPermissions = await _rolePermissionRepository.GetRolesPermissionsAsync(user.UserRoles.Select(s => s.RoleId).ToList());
            string[] dashbaordPermissions = new string[] {"dashboard-login"};
            
            if (!UserRolesPermissions.Select(z => z.Permission.Name.ToLower()).ContainsAllItems(dashbaordPermissions))
                throw new BadRequestException("حساب کاربری شما دسترسی های لازم را ندارد");

            
            var jwt = await GenerateAsync(user);
            user.LastLogOnDate = DateTime.Now;
            await _userRepository.UpdateAsync(user, cancellationToken);
            return jwt;
        }

        public async Task<string> GenerateAsync(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSetting.Jwt.Key);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256Signature);

            var claims = await _getClaimsAsync(user);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _siteSetting.Jwt.Issuer,
                Audience = _siteSetting.Jwt.Issuer,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_siteSetting.Jwt.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(_siteSetting.Jwt.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claims)
            };


            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);

            var jwt = tokenHandler.WriteToken(securityToken);

            return jwt;
        }

        private async Task<IEnumerable<Claim>> _getClaimsAsync(User user)
        {
            //JwtRegisteredClaimNames.Sub
            var securityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var userRoles = await _userRepository.GetUserRolesAsync(user.Id);
            userClaims.AddRange(userRoles.Select(z => new Claim(ClaimTypes.Role, z.Id.ToString())));

            return userClaims;
        }

        public async Task<string> RefreshTokenAsync(User user)
        {
            // jwt refresh token
            var jwt = await GenerateAsync(user);
            return jwt;
        }

        public async Task<string> VerificationCode(VerificationViewModel ViewModel, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByUserName(ViewModel.UserName);

            if (user != null)
            {
                string code = StringGenerator.RandomConfirmationCode();

                SmsViewModel smsView = new SmsViewModel()
                {
                    message = String.Format("کد تایید پنج رقمی : {0}", code),
                    mobile = user.Username,
                    token = "67E1F56B-7CBF-4D93-BDAC-4A67D5A5DA3A"
                };

                user.VerificationCode = code;
                user.ChangePasswordCode = code;
                user.VerificationExpiration = DateTime.Now.AddMinutes(2);
                await _userRepository.UpdateAsync(user, cancellationToken);

                bool result = await user.SendSmsAsync(smsView);

                return result.ToString();
            }

            else throw new BadRequestException("شماره موجود نیست");
        }

        public bool SendSmsGeneral(string[] destNo, string content, int accountSmsId)
        {
            return true;
        }

        public bool SendEmail(SendSmsViewModel viewModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CheckPhone(string username, string ip, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByUserName(username);

            if (user != null)
            {
                return username;
            }



            string code = StringGenerator.RandomConfirmationCode();

            SmsViewModel smsView = new SmsViewModel()
            {
                message = String.Format("کد تایید پنج رقمی برای ثبت نام : {0}", code),
                mobile = username,
                token = "67E1F56B-7CBF-4D93-BDAC-4A67D5A5DA3A"
            };

            RegisterTemp registerTemp = new RegisterTemp()
            {
                Code = code,
                ExpirationDate = DateTime.Now.AddMinutes(2),
                Mobile = username,
                Ip = ip
            };

            await _registerTempRepository.AddAsync(registerTemp, cancellationToken);

            bool result = await user.SendSmsAsync(smsView);
            if (!result)
            {
                // لاگ بیاندازد
            }

            // return result.ToString();
            throw new BadRequestException("کد نا معتبر است");
        }

        public async Task<string> TwoStep(string username, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByUserName(username);

            if (user != null)
            {
                string code = StringGenerator.RandomConfirmationCode();

                SmsViewModel smsView = new SmsViewModel()
                {
                    message = String.Format("کد تایید پنج رقمی : {0}", code),
                    mobile = user.Username,
                    token = "67E1F56B-7CBF-4D93-BDAC-4A67D5A5DA3A"
                };

                user.TwoStepCode = code;
                user.TwoStepExpiration = DateTime.Now.AddMinutes(2);

                await _userRepository.UpdateAsync(user, cancellationToken);

                bool result = await user.SendSmsAsync(smsView);


                return result.ToString();
            }

            else throw new BadRequestException("شماره موجود نیست");
        }

        public async Task<string> CheckTwoStep(CheckVerificationViewModel model, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUserNameCode(model);
            if (user != null)
            {
                var jwt = await GenerateAsync(user);
                user.TwoStepCode = null;
                user.TwoStepExpiration = null;
                user.LastLogOnDate = DateTime.Now;
                await _userRepository.UpdateAsync(user, cancellationToken);
                return jwt;
            }


            else throw new BadRequestException("کد اعتبارسنجی احراز نشد");
        }


        public async Task<string> CheckVerification(CheckVerificationViewModel model,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUserNameVerificationCode(model.Username, model.VerificationCode);
            if (user != null)
            {
                user.VerificationCode = null;
                user.VerificationExpiration = null;
                var guid = Guid.NewGuid();
                user.ChangePasswordCode = guid.ToString().Substring(0, 8);
                await _userRepository.UpdateAsync(user, cancellationToken);
                return "موفقیت آمیز بود";
            }

            else throw new BadRequestException("کد اعتبارسنجی احراز نشد");
        }

        public async Task<string> ChangePassword(ChangePasswordViewModel model, string username,
            CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByUserNameChangePassword(model, username);
            if (user != null)
            {
                var passwordHash = SecurityHelper.GetSha256Hash(model.password);
                user.Password = passwordHash;
                user.ChangePasswordCode = null;
                await _userRepository.UpdateAsync(user, cancellationToken);
                return "موفقیت آمیز بود";
            }
            else throw new BadRequestException("تغییر گذرواژه با خطا مواجه شد");
        }

        public async Task<UserInfoViewModel> GetUserInfo(long userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetwithPersonById(cancellationToken, userId);
            if (user == null)
            {
                throw new NotFoundException("user");
            }

            return _mapper.Map<UserInfoViewModel>(user);
        }
    }
}