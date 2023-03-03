using DAL.Models;
using Models.SMS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.SmsService
{
    public static class SmsExtensions
    {
        public static async Task<bool> SendSmsAsync(this User user, SmsViewModel model)
        {
            if (model.mobile != null)
            {
                return await SendSmsAsync(model);
            }

            return false;
        }

        public static async Task<bool> SendSmsAsync(SmsViewModel model)
        {
            if (model.mobile != null)
            {
                HttpClient client = new HttpClient();

                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
                    "application/json");
                HttpResponseMessage responseMessage =
                    await client.PostAsync("http://178.22.121.237/aloni.web.api/api/Communication/sendSms", content);

                responseMessage.EnsureSuccessStatusCode();

                string responseBody = await responseMessage.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(responseBody);

                if (result.result != true)
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}