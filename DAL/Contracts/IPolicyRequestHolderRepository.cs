using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IPolicyRequestHolderRepository:IRepository<PolicyRequestHolder>
    {
        Task<PolicyRequestHolder> GetByPolicyRequestCode(Guid code, CancellationToken cancellationToken);
        Task<PolicyRequestHolder> GetByPolicyRequestCodeNoTracking(Guid code, CancellationToken cancellationToken);
        Task<PolicyRequestHolder> GetByPolicyRequestCodeNoTrackingWithoutDetails(Guid code, CancellationToken cancellationToken);
        Task<PolicyRequestHolder> GetByPolicyRequestCodeWithoutRelation(Guid code, CancellationToken cancellationToken);

    }
}
