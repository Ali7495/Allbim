using DAL.Contracts;
using DAL.Models;
using Models.SMS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.SmsService
{
    public class SmsService : ISmsService
    {

        private readonly IUserRepository _userRepository;

        public SmsService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public Task<bool> SmsSenderAsync(string username, string message, string code)
        {
            return Task.Run<bool>(async () =>
            {
                User user = await _userRepository.GetByUserName(username);

                SmsViewModel model = new SmsViewModel();
                model.token = "67E1F56B-7CBF-4D93-BDAC-4A67D5A5DA3A";


                if (code != null)
                {
                    model.message = String.Format(message, code);
                }
                else
                {
                    model.message = message;
                }

                if (user != null)
                {
                    model.mobile = user.Username;

                    user.VerificationCode = code;
                    user.VerificationExpiration = DateTime.Now.AddMinutes(2);

                    _userRepository.Update(user);
                }
                else
                {
                    model.mobile = username;
                }


                return await user.SendSmsAsync(model);
            });
        }
    }
}
