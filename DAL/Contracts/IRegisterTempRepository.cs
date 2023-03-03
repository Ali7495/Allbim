using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IRegisterTempRepository : IRepository<RegisterTemp>
    {
        Task<RegisterTemp> GetRegisterTempByCodeAndMobile(string mobile, string code, CancellationToken cancellationToken);
    }
}
