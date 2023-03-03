using Common.Utilities;
using DAL.Models;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IPolicyRequestDetailRepository
    {
        Task<List<PolicyRequestDetail>> GetDetailsByPolicyRequestId(long id, CancellationToken cancellationToken);


        Task<PagedResult<PolicyRequestDetail>> GetCompanyPolicyRequestDetailsByPolicyCode(Guid code, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
    }
}
