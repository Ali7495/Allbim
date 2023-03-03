using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IPolicyRequestCommentRepository : IRepository<PolicyRequestComment>
    {
        Task<List<PolicyRequestComment>> GetAllPolicyRequestCommentById(long ID, CancellationToken cancellationToken);
        Task<PolicyRequestComment> GetPolicyRequestCommentById(long ID, CancellationToken cancellationToken);
        Task<List<PolicyRequestComment>> GetAllPolicyRequestCommentByCompanyId(long companyId, CancellationToken cancellationToken);

        Task<List<PolicyRequestComment>> GetPolicyRequestCommentsByCompanyIdAndPolicyCode(long companyId, Guid policyCode, CancellationToken cancellationToken);
    }
}
