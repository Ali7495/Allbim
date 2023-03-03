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
    public interface IAgentRepository : IRepository<CompanyAgent>
    {
        Task<List<CompanyAgent>> GetAllByCompanyCodeAsync(Guid code, CancellationToken cancellationToken);
        Task<CompanyAgent> GetByCompanyAndPersonCodeAsync(Guid code, Guid personCode, CancellationToken cancellationToken);
        Task<CompanyAgent> GetByCompanyAndPersonCodeAsyncNoTracking(Guid code, Guid personCode, CancellationToken cancellationToken);
        Task<CompanyAgent> GetLonelyByPersonAndCompanyCodeAsync(Guid code, Guid personCode, CancellationToken cancellationToken);
        Task<CompanyAgent> GetByPersonIdAndCompanyId(long personId, long companyId, CancellationToken cancellationToken);
        Task<PagedResult<CompanyAgent>> GetCompanyAgents(long companyId, long roleId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<CompanyAgent> GetByIdNoTracking(long companyAgentId, CancellationToken cancellationToken);
    }
}
