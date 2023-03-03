using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Models.PageAble;
using Models.Policy;
using Models.PolicyRequest;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.PolicyRequest
{
    public interface IPolicyRequestDetailService
    {
        Task<PolicyRequestDetailViewModel> Create(PolicyRequestDetailViewModel viewModel, CancellationToken cancellationToken);
        Task<bool> Delete(long id, CancellationToken cancellationToken);
        Task<PolicyRequestDetailViewModel> Update(long id, PolicyRequestDetailViewModel viewModel, CancellationToken cancellationToken);
        Task<PolicyRequestDetailViewModel> GetPolicyDetail(long id, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequestDetailViewModel>> GetAll(PageAbleResult pageAbleResult, CancellationToken cancellationToken);
    }
}