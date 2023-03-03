using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Utilities;
using DAL.Models;
using Models.PageAble;

namespace DAL.Contracts
{
    public interface IPolicyRequestRepository : IRepository<PolicyRequest>
    {
        Task<PolicyRequest> GetByCode(Guid code, CancellationToken cancellationToken);
        Task<PolicyRequest> GetByCodeNoTracking(Guid code, CancellationToken cancellationToken);
        Task<PolicyRequest> GetByCodeWithoutRelationNoTracking(Guid code, CancellationToken cancellationToken);
        Task<PolicyRequest> GetPolicyRequestDetailByCode(Guid code, CancellationToken cancellationToken);
        Task<PolicyRequest> GetAllByCode(Guid code, CancellationToken cancellationToken);
        Task<PolicyRequest> checkPolicyRequestExistsByCode(Guid code, CancellationToken cancellationToken);
        Task<List<PolicyRequest>> GetByPersonId(long personId, CancellationToken cancellationToken);
        Task<PolicyRequest> GetPaymentDetailsByCode(Guid code, CancellationToken cancellationToken);
        Task<List<PolicyRequest>> GetByProvinceId(long provinceId, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequest>> GetByCompanyId(long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequest>> GetByReviewerId(long reviewerId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequest>> GetByMyRequests(long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequest>> GetAllByPaging(PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequest>> GetAllByPagingAndCompanyCodes(List<Guid> companyCodes, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequest>> GetByCompanyIdBySlug(PolicyRequestSlug slug, long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequest>> GetByReviewerIdBySlug(PolicyRequestSlug slug, long reviewerId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequest>> GetAllByPagingBySlug(PolicyRequestSlug slug, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<int> GetCountByInsurerId(long id, CancellationToken cancellationToken);
        Task<PolicyRequest> GetPolicyRequestAndCompanyByCode(Guid code, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequest>> GetRequestsByCompanyCode(Guid code, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<PolicyRequest> GetRequestByCompanyCode(Guid code, Guid policyCode, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequest>> GetRequestsByCompanyId(long id, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequest>> GetRequestsByStatusAndCompanyId(long companyId, long statusId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequest>> GetStatusAllRequestsByCompanyId(long id, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequest>> GetRequestsByNewStatusAndCompanyId(long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequest>> GetRequestsByFinishedStatusAndCompanyId(long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequest>> GetRequestsByUnFinishedStatusAndCompanyId(long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequest>> GetStatusAllRequestsByAgentCompanyId(long id, long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequest>> GetRequestsByNewStatusAndCompanyAgentId(long companyId, long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequest>> GetRequestsByFinishedStatusAndCompanyAgentId(long companyId, long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequest>> GetRequestsByUnFinishedStatusAndCompanyAgentId(long companyId, long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        Task<List<PolicyRequest>> GetListByReviewerId(long personId, CancellationToken cancellationToken);
        Task<List<PolicyRequest>> GetListByAgentSelectPersonId(long personId, CancellationToken cancellationToken);

        Task<List<PolicyRequest>> GetByPersonIdWithoutDetail(long personId, CancellationToken cancellationToken);

        Task<PolicyRequest> GetByCompanyIdAndPolicyCode(long companyId, Guid policyCode, CancellationToken cancellationToken);

        Task<List<PolicyRequest>> GetListByCompanyId(long companyId, CancellationToken cancellationToken);
    }
}