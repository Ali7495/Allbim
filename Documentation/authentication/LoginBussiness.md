<div align="right" dir="rtl" >

سرویس های مربوط به ورود در کنترلر SSO قرار دارند و به شرح زیر هستند:

</div>


```C#

[HttpGet("check_phone/{username}")]
        [AllowAnonymous]
        public async Task<ApiResult<string>> CheckPhone([FromRoute] string username,
            CancellationToken cancellationToken)
        {
            IPAddress Ip = Request.HttpContext.Connection.RemoteIpAddress;

            var res = await _iSsoService.CheckPhone(username, Ip.ToString(), cancellationToken);
            return res;
        }



       [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ApiResult<string>> Login([FromBody] LoginViewModel loginViewModel, CancellationToken cancellationToken)
        {
            var jwt = await _iSsoService.Login(loginViewModel, cancellationToken);
            return jwt;
        }


```

<br>

<div align="right" dir="rtl" >

سرویس `CheckPhone(username, Ip.ToString(), cancellationToken)` شماره تلفن کاربر را که در username دریافت کرده بررسی می کند.

</div>


```C#

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

```

<br>

<div align="right" dir="rtl" >

سرویس `Login(loginViewModel, cancellationToken)` اطلاعات کاربری را با استفاده از ویومدل دریافت می کند. و ویومدل آن به شکل زیر است:

</div>


```C#

public class LoginViewModel
    {
        [JsonProperty(PropertyName = "username")]
        [Required]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "password")]
        [Required]
        public string Password { get; set; }
    }

```

<div align="right" dir="rtl" >

همچنین بدنه سرویس :

</div>

```C#

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

```