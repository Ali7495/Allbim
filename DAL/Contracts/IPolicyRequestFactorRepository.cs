using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using DAL.Models;
using Models.PageAble;

namespace DAL.Contracts
{
    public interface IPolicyRequestFactorRepository : IRepository<PolicyRequestFactor>
    {
        Task<List<PolicyRequestFactor>> GetFactorsWithDetailsByPolicyId(long id, CancellationToken cancellationToken);
        Task<PolicyRequestFactor> GetByPolicyIdFactorId(long id, long modelId, CancellationToken cancellationToken);
        Task<PolicyRequestFactor> GetPaymentInfoByPolicyId(long id, CancellationToken cancellationToken);
        Task<PolicyRequestFactor> GetByIdNoTracking(long id, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequestFactor>> GetAllFactorsOfCompany(long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);


        Task<PagedResult<PolicyRequestFactor>> GetAllPolicyFactorsOfCompany(long companyId, long policyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PolicyRequestFactor> GetFactorByIdNoTrakingWithDetails(long factorId, CancellationToken cancellationToken);
        Task<PolicyRequestFactor> GetByIdWithPayment(long factorId, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequestFactor>> GetAllFactors(PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequestFactor>> GetAllPersonFactors(long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PolicyRequestFactor> GetByIdWithPaymentAndDetails(long factorId, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequestFactor>> GetAllFactorsByAllParameters(Guid companyCode, long statusId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequestFactor>> GetAllFactorsByStatus(long statusId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequestFactor>> GetAllFactorsByCompany(Guid companyCode, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequestFactor>> GetAllFactorsByPersonId(long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequestFactor>> GetAllFactorsByPersonAndStatusId(long personId, long statusId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequestFactor>> GetAllCompanyFactorsByStatusId(long companyId, long statusId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequestFactor>> GetAllCompanyFactors(long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

    }
}