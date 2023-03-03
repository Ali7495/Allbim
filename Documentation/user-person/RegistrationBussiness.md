<div align="right" dir="rtl" >

سرویس های مربوط به ثبت نام در کنترلر SSO قرار دارند و به شرح زیر هستند:

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



        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<UserResultViewModel> Register(RegisterViewModel ViewModel, CancellationToken cancellationToken)
        {
            var res = _iSsoService.RegisterFromApi(ViewModel, cancellationToken);
            return await res;
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

سرویس `RegisterFromApi(ViewModel, cancellationToken)` اطلاعات فرد و کاربری را با استفاده از ویومدل دریافت می کند. و ویومدل آن به شکل زیر است:

</div>


```C#

public class RegisterViewModel
    {
        [JsonPropertyName("first_name")]
        [Required]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        [Required]
        public string LastName { get; set; }
        [JsonPropertyName("national_code")]
        [Required]
        public string NationalCode { get; set; }
        [JsonPropertyName("identity")]
        [Required]
        public string Identity { get; set; }
        [JsonPropertyName("father_name")]
        [Required]
        public string FatherName { get; set; }
        [JsonPropertyName("birth_date")]
        [AllowNull]
        public string BirthDate { get; set; }

        [JsonPropertyName("gender_id")]
        [Required]
        public byte GenderId { get; set; }
        [JsonPropertyName("marriage_id")]
        [Required]
        public byte MarriageId { get; set; }
        [JsonPropertyName("millitary_id")]
        public byte? MillitaryId { get; set; }

        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; }
        [JsonPropertyName("email")]
        [Required]
        public string Email { get; set; }
        [JsonPropertyName("username")]
        [Required]
        public string Username { get; set; }

        [JsonPropertyName("code")]
        [Required]
        public string Code { get; set; }
    }


```

<div align="right" dir="rtl" >

همچنین بدنه سرویس :

</div>

```C#

        public async Task<UserResultViewModel> RegisterFromApi(RegisterViewModel registerViewModel, CancellationToken cancellationToken)
        {
            var user = await Register(registerViewModel, cancellationToken);
            return _mapper.Map<UserResultViewModel>(user);
           
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

```