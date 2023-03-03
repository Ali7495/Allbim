using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.SmsService
{
    public interface ISmsService
    {
        Task<bool> SmsSenderAsync(string username, string message, string code);
    }
}
