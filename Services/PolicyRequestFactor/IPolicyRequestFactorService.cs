using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Models.PageAble;
using Models.Policy;
using Models.PolicyRequest;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.PolicyRequest
{
    public interface IPolicyRequestFactorService
    {
        Task<PolicyRequestFactorViewModel> Create(PolicyRequestFactorViewModel viewModel, CancellationToken cancellationToken);
        Task<PolicyRequestFactorViewModel> GetPolicyFactor(long id, CancellationToken cancellationToken);
        Task<PolicyRequestFactorViewModel> Update(long factorId, PolicyRequestFactorViewModel viewmodel, CancellationToken cancellationToken);
        Task<bool> Delete(Guid code, long factorId, CancellationToken cancellationToken);
        Task<PolicyRequestFactorViewModel> GetByCode(Guid code, CancellationToken cancellationToken);
    }
}