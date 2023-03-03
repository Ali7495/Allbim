<div align="right" dir="rtl" >

سرویس های مربوط به تغییر رمز در کنترلر SSO قرار دارند و به شرح زیر هستند:

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



       [HttpPatch("change_password/{username}")]
        [AllowAnonymous]
        public async Task<ApiResult<string>> ChangePassword(ChangePasswordViewModel mdoel, [FromRoute] string username,
            CancellationToken cancellationToken)
        {
            var res = await _iSsoService.ChangePassword(mdoel, username, cancellationToken);
            return res;
        }

        [HttpPatch("verification_code")]
        [AllowAnonymous]
        public async Task<ApiResult<string>> VerificationCode(VerificationViewModel ViewModel,
            CancellationToken cancellationToken)
        {
            var res = await _iSsoService.VerificationCode(ViewModel, cancellationToken);
            return res.ToString();
        }



        [HttpPatch("check_verification")]
        [AllowAnonymous]
        public async Task<ApiResult<string>> CheckVerification(CheckVerificationViewModel model,
            CancellationToken cancellationToken)
        {
            var res = await _iSsoService.CheckVerification(model, cancellationToken);
            return res;
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

سرویس `ChangePassword(mdoel, username, cancellationToken)` اطلاعات کاربری را با استفاده از ویومدل دریافت می کند. و ویومدل آن به شکل زیر است:

</div>


```C#

public class ChangePasswordViewModel
    {
        public string ChangePasswordCode { get; set; }
        public string password { get; set; }
        

    }

```

<div align="right" dir="rtl" >

همچنین بدنه سرویس :

</div>

```C#

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

```



<div align="right" dir="rtl" >

این سرویس پیامک حاوی کد ارسال میکند

<br>

سرویس زیر هم کد تایید ورودی کاربر را چک میکند :

</div>

```C#

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

```


<div align="right" dir="rtl" >



سرویس زیر هم نهایتا رمز را تغییر می دهد:
</div>


```C#


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



```