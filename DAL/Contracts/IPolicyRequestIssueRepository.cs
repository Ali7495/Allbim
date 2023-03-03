using System;
using System.Threading;
using System.Threading.Tasks;
using DAL.Models;


namespace DAL.Contracts
{
    public interface IPolicyRequestIssueRepository : IRepository<PolicyRequestIssue>
    {
        Task<PolicyRequestIssue> GetByPolicyRequestCode(Guid code,  CancellationToken cancellationToken);

    }
}