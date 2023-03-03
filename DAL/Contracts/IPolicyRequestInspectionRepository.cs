using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IPolicyRequestInspectionRepository : IRepository<PolicyRequestInspection>
    {
        Task<PolicyRequestInspection> GetByPolicyRequestCode(Guid code, CancellationToken cancellationToken);
        Task<PolicyRequestInspection> GetByPolicyRequestCodeNoTracking(Guid code, CancellationToken cancellationToken);
    }
}
