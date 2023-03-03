using Common.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Extensions;
using DAL.Contracts;
using DAL.Models;
using Models.PageAble;

namespace DAL.Repositories
{
    public class PolicyRequestRepository : Repository<PolicyRequest>, IPolicyRequestRepository
    {
        public PolicyRequestRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }


        public async Task<PolicyRequest> checkPolicyRequestExistsByCode(Guid code, CancellationToken cancellationToken)
        {
            PolicyRequest item = await Table.AsNoTracking().Where(p => p.Code == code).FirstOrDefaultAsync(cancellationToken);

            return item;
        }

        public async Task<PolicyRequest> GetByCode(Guid code, CancellationToken cancellationToken)
        {
            return await Table.Where(p => p.Code == code).Include(i => i.RequestPerson).Include(i => i.Insurer)
                .SingleOrDefaultAsync(cancellationToken);
        }
        public async Task<PolicyRequest> GetByCodeNoTracking(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Code == code)
                .Include(i => i.RequestPerson)
                .Include(i => i.Insurer)
                .ThenInclude(x => x.Insurance)
                .ThenInclude(x => x.InsuranceFrontTabs)
                .Include(i => i.Insurer)
                .ThenInclude(x => x.Company)
                .Include(x => x.AgentSelected)
                .ThenInclude(x => x.Person)
                .Include(x => x.AgentSelected)
                .ThenInclude(x => x.City)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<PolicyRequest> GetByCodeWithoutRelationNoTracking(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Code == code)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<PolicyRequest> GetPolicyRequestDetailByCode(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Code == code)
                .Include(i => i.RequestPerson)
                .Include(i => i.PolicyRequestDetails)
                .Include(i => i.PolicyRequestHolders).ThenInclude(p => p.Person)
                .Include(i => i.Insurer).ThenInclude(x => x.Company)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<PolicyRequest> GetAllByCode(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Code == code).Include(i => i.RequestPerson).Include(i => i.Insurer)
                .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                .Include(i => i.PolicyRequestHolders).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<List<PolicyRequest>> GetByPersonId(long personId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.RequestPersonId == personId).Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                  .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                  .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                  .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                  .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                  .Include(x => x.PolicyRequestStatus)
                  .Include(i => i.PolicyRequestHolders).ToListAsync(cancellationToken);
        }

        public async Task<PolicyRequest> GetPaymentDetailsByCode(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Code == code).Include(i => i.Insurer).ThenInclude(th => th.Insurance).Include(i => i.PolicyRequestDetails).Include(i => i.PolicyRequestFactors).ThenInclude(th => th.Payment).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<List<PolicyRequest>> GetByProvinceId(long provinceId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.PolicyRequestHolders.FirstOrDefault().Address.City.TownShip.ProvinceId == provinceId).Include(i => i.PolicyRequestDetails).Include(i => i.Insurer).ThenInclude(th => th.Insurance).Include(i => i.Insurer).ThenInclude(th => th.Company).Include(i => i.RequestPerson).Include(i => i.PolicyRequestFactors).ThenInclude(th => th.Payment).Include(i => i.PolicyRequestHolders).Include(i => i.InsuredRequests).ThenInclude(th => th.InsuredRequestVehicles).ThenInclude(th => th.Vehicle).ThenInclude(th => th.VehicleBrand).ToListAsync(cancellationToken);
        }

        public async Task<PagedResult<PolicyRequest>> GetByCompanyId(long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Insurer.CompanyId == companyId)
                .Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                .Include(x => x.PolicyRequestStatus)
                .Include(i => i.PolicyRequestHolders).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequest>> GetByReviewerId(long reviewerId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(r => r.ReviewerId == reviewerId).Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                .Include(x => x.PolicyRequestStatus)
                .Include(i => i.PolicyRequestHolders).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequest>> GetAllByPaging(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking()
                .Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                .Include(x => x.PolicyRequestStatus)
                .Include(i => i.PolicyRequestHolders).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }
        public async Task<PagedResult<PolicyRequest>> GetByCompanyIdBySlug(PolicyRequestSlug slug, long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            if (slug == PolicyRequestSlug.accepted_Slug)
            {
                return await Table.AsNoTracking().Where(p => p.Insurer.CompanyId == companyId && p.PolicyRequestStatusId == 5)
                .Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                .Include(x => x.PolicyRequestStatus)
                .Include(i => i.PolicyRequestHolders).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
            }
            else if (slug == PolicyRequestSlug.accepted_Slug)
            {
                return await Table.AsNoTracking().Where(p => p.Insurer.CompanyId == companyId && p.PolicyRequestStatusId == 4)
                .Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                .Include(x => x.PolicyRequestStatus)
                .Include(i => i.PolicyRequestHolders).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
            }
            else
            {
                return await Table.AsNoTracking().Where(p => p.Insurer.CompanyId == companyId && (p.PolicyRequestStatusId == 1 || p.PolicyRequestStatusId == 2 || p.PolicyRequestStatusId == 3))
                .Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                .Include(x => x.PolicyRequestStatus)
                .Include(i => i.PolicyRequestHolders).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
            }

        }

        public async Task<PagedResult<PolicyRequest>> GetByReviewerIdBySlug(PolicyRequestSlug slug, long reviewerId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            if (slug == PolicyRequestSlug.accepted_Slug)
            {
                return await Table.AsNoTracking().Where(r => r.ReviewerId == reviewerId && r.PolicyRequestStatusId == 5).Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                .Include(x => x.PolicyRequestStatus)
                .Include(i => i.PolicyRequestHolders).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
            }
            else if (slug == PolicyRequestSlug.inProgress_Slug)
            {
                return await Table.AsNoTracking().Where(r => r.ReviewerId == reviewerId && r.PolicyRequestStatusId == 4).Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                    .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                    .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                    .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                    .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                    .Include(x => x.PolicyRequestStatus)
                    .Include(i => i.PolicyRequestHolders).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
            }
            else
            {
                return await Table.AsNoTracking().Where(r => r.ReviewerId == reviewerId && (r.PolicyRequestStatusId == 1 || r.PolicyRequestStatusId == 2 || r.PolicyRequestStatusId == 3)).Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                .Include(x => x.PolicyRequestStatus)
                .Include(i => i.PolicyRequestHolders).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
            }
        }

        public async Task<PagedResult<PolicyRequest>> GetAllByPagingBySlug(PolicyRequestSlug slug, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            if (slug == PolicyRequestSlug.accepted_Slug)
            {
                return await Table.AsNoTracking().Where(x => x.PolicyRequestStatusId == 5)
                    .Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                    .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                    .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                    .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                    .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                    .Include(x => x.PolicyRequestStatus)
                    .Include(i => i.PolicyRequestHolders).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
            }
            else if (slug == PolicyRequestSlug.inProgress_Slug)
            {
                return await Table.AsNoTracking().Where(x => x.PolicyRequestStatusId == 4)
                    .Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                    .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                    .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                    .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                    .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                    .Include(x => x.PolicyRequestStatus)
                    .Include(i => i.PolicyRequestHolders).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
            }
            else
            {
                return await Table.AsNoTracking().Where(x => x.PolicyRequestStatusId == 1 || x.PolicyRequestStatusId == 2 || x.PolicyRequestStatusId == 3)
                    .Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                    .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                    .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                    .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                    .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                    .Include(x => x.PolicyRequestStatus)
                    .Include(i => i.PolicyRequestHolders).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
            }
        }


        public async Task<int> GetCountByInsurerId(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.InsurerId == id).CountAsync(cancellationToken);
        }

        public async Task<PolicyRequest> GetPolicyRequestAndCompanyByCode(Guid code, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.Code == code)
                .Include(x => x.Insurer).ThenInclude(x => x.Company)
                .Include(c => c.AgentSelected)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<PolicyRequest>> GetByMyRequests(long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(r => r.RequestPersonId == personId).Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                .Include(x => x.PolicyRequestStatus)
                .Include(i => i.PolicyRequestHolders).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequest>> GetRequestsByCompanyCode(Guid code, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(r => r.Insurer.Company.Code == code)
                .Include(i => i.RequestPerson)
                .Include(i => i.Insurer).ThenInclude(x => x.Insurance)
                .Include(x => x.Insurer).ThenInclude(x => x.Company)
                .Include(x => x.AgentSelected).ThenInclude(th => th.City)
                .Include(x => x.AgentSelected).ThenInclude(th => th.Company)
                .Include(x => x.AgentSelected).ThenInclude(th => th.Person)
                .Include(x => x.PolicyRequestStatus)
                .Include(x => x.Reviewer).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PolicyRequest> GetRequestByCompanyCode(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(r => r.Insurer.Company.Code == code && r.Code == policyCode)
                .Include(i => i.RequestPerson)
                .Include(i => i.Insurer).ThenInclude(x => x.Insurance)
                .Include(x => x.Insurer).ThenInclude(x => x.Company)
                .Include(x => x.AgentSelected).ThenInclude(th => th.City)
                .Include(x => x.AgentSelected).ThenInclude(th => th.Company)
                .Include(x => x.AgentSelected).ThenInclude(th => th.Person)
                .Include(x => x.PolicyRequestStatus)
                .Include(x => x.Reviewer).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<PolicyRequest>> GetRequestsByCompanyId(long id, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(r => r.Insurer.CompanyId == id)
                .Include(i => i.RequestPerson)
                .Include(i => i.Insurer).ThenInclude(x => x.Insurance)
                .Include(x => x.Insurer).ThenInclude(x => x.Company)
                .Include(x => x.AgentSelected).ThenInclude(th => th.City)
                .Include(x => x.AgentSelected).ThenInclude(th => th.Company)
                .Include(x => x.AgentSelected).ThenInclude(th => th.Person)
                .Include(x => x.PolicyRequestStatus)
                .Include(x => x.Reviewer).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequest>> GetRequestsByStatusAndCompanyId(long companyId, long statusId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p=> p.Insurer.CompanyId == companyId && p.PolicyRequestStatusId == statusId)
                .Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                .Include(x => x.PolicyRequestStatus)
                .Include(i => i.PolicyRequestHolders).OrderByDescending(o => o.Id).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequest>> GetStatusAllRequestsByCompanyId(long id, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(r => r.Insurer.CompanyId == id)
                 .Include(i => i.RequestPerson)
                 .Include(i => i.Insurer).ThenInclude(x => x.Insurance)
                 .Include(x => x.Insurer).ThenInclude(x => x.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.City)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Person)
                 .Include(x => x.PolicyRequestStatus)
                 .Include(x => x.Reviewer).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequest>> GetRequestsByNewStatusAndCompanyId(long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(r => r.Insurer.CompanyId == companyId && r.AgentSelectedId == null)
                 .Include(i => i.RequestPerson)
                 .Include(i => i.Insurer).ThenInclude(x => x.Insurance)
                 .Include(x => x.Insurer).ThenInclude(x => x.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.City)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Person)
                 .Include(x => x.PolicyRequestStatus)
                 .Include(x => x.Reviewer).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequest>> GetRequestsByFinishedStatusAndCompanyId(long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(r => r.Insurer.CompanyId == companyId && r.PolicyRequestStatusId == 4 || r.PolicyRequestStatusId == 5)
                 .Include(i => i.RequestPerson)
                 .Include(i => i.Insurer).ThenInclude(x => x.Insurance)
                 .Include(x => x.Insurer).ThenInclude(x => x.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.City)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Person)
                 .Include(x => x.PolicyRequestStatus)
                 .Include(x => x.Reviewer).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequest>> GetRequestsByUnFinishedStatusAndCompanyId(long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(r => r.Insurer.CompanyId == companyId && r.PolicyRequestStatusId != 4 && r.PolicyRequestStatusId != 5)
                 .Include(i => i.RequestPerson)
                 .Include(i => i.Insurer).ThenInclude(x => x.Insurance)
                 .Include(x => x.Insurer).ThenInclude(x => x.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.City)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Person)
                 .Include(x => x.PolicyRequestStatus)
                 .Include(x => x.Reviewer).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequest>> GetStatusAllRequestsByAgentCompanyId(long id, long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(r => r.Insurer.CompanyId == id && r.AgentSelected.PersonId == personId)
                 .Include(i => i.RequestPerson)
                 .Include(i => i.Insurer).ThenInclude(x => x.Insurance)
                 .Include(x => x.Insurer).ThenInclude(x => x.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.City)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Person)
                 .Include(x => x.PolicyRequestStatus)
                 .Include(x => x.Reviewer).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequest>> GetRequestsByNewStatusAndCompanyAgentId(long companyId, long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(r => r.Insurer.CompanyId == companyId && r.AgentSelectedId == null && r.AgentSelected.PersonId == personId)
                 .Include(i => i.RequestPerson)
                 .Include(i => i.Insurer).ThenInclude(x => x.Insurance)
                 .Include(x => x.Insurer).ThenInclude(x => x.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.City)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Person)
                 .Include(x => x.PolicyRequestStatus)
                 .Include(x => x.Reviewer).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequest>> GetRequestsByFinishedStatusAndCompanyAgentId(long companyId, long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(r => r.Insurer.CompanyId == companyId && r.AgentSelected.PersonId == personId && r.PolicyRequestStatusId == 4 && r.PolicyRequestStatusId == 5)
                 .Include(i => i.RequestPerson)
                 .Include(i => i.Insurer).ThenInclude(x => x.Insurance)
                 .Include(x => x.Insurer).ThenInclude(x => x.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.City)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Person)
                 .Include(x => x.PolicyRequestStatus)
                 .Include(x => x.Reviewer).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequest>> GetRequestsByUnFinishedStatusAndCompanyAgentId(long companyId, long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(r => r.Insurer.CompanyId == companyId && r.AgentSelected.PersonId == personId && r.PolicyRequestStatusId != 4 && r.PolicyRequestStatusId != 5)
                 .Include(i => i.RequestPerson)
                 .Include(i => i.Insurer).ThenInclude(x => x.Insurance)
                 .Include(x => x.Insurer).ThenInclude(x => x.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.City)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Company)
                 .Include(x => x.AgentSelected).ThenInclude(th => th.Person)
                 .Include(x => x.PolicyRequestStatus)
                 .Include(x => x.Reviewer).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<List<PolicyRequest>> GetListByReviewerId(long personId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.ReviewerId == personId).Include(i => i.Reviewer)
                .Include(i => i.AgentSelected).ThenInclude(th => th.Person).ToListAsync(cancellationToken);
        }

        public async Task<List<PolicyRequest>> GetListByAgentSelectPersonId(long personId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.AgentSelected.PersonId == personId).Include(i => i.AgentSelected).ThenInclude(th => th.Person).ToListAsync(cancellationToken);
        }

        public async Task<List<PolicyRequest>> GetByPersonIdWithoutDetail(long personId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.RequestPersonId == personId).ToListAsync(cancellationToken);
        }

        public async Task<PolicyRequest> GetByCompanyIdAndPolicyCode(long companyId, Guid policyCode, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p=> p.Code == policyCode && p.Insurer.CompanyId == companyId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<PolicyRequest>> GetListByCompanyId(long companyId, CancellationToken cancellationToken)
        { 
            return await Table.AsNoTracking().Where(p => p.Insurer.CompanyId == companyId).ToListAsync(cancellationToken);
        }

        public async Task<PagedResult<PolicyRequest>> GetAllByPagingAndCompanyCodes(List<Guid> companyCodes, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p=> companyCodes.Contains(p.Insurer.Company.Code))
                .Include(i => i.RequestPerson).Include(i => i.Insurer).ThenInclude(x => x.Insurance).Include(x => x.Insurer).ThenInclude(x => x.Company)
                .Include(i => i.InsuredRequests).Include(i => i.PolicyRequestAttachments)
                .ThenInclude(th => th.Attachment).Include(i => i.PolicyRequestFactors)
                .Include(x => x.InsuredRequests).ThenInclude(x => x.InsuredRequestVehicles).ThenInclude(x => x.Vehicle).ThenInclude(x => x.VehicleBrand)
                .Include(x => x.PolicyRequestFactors).ThenInclude(x => x.Payment)
                .Include(x => x.PolicyRequestStatus)
                .Include(i => i.PolicyRequestHolders).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }
    }
}