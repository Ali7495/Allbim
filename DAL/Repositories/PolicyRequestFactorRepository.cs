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
    public class PolicyRequestFactorRepository : Repository<PolicyRequestFactor>, IPolicyRequestFactorRepository
    {
        public PolicyRequestFactorRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<List<PolicyRequestFactor>> GetFactorsWithDetailsByPolicyId(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.PolicyRequestId == id).Include(i=> i.PolicyRequestFactorDetails).Include(i=> i.Payment).ToListAsync(cancellationToken);

        }
        public async Task<PolicyRequestFactor> GetByPolicyIdFactorId(long id, long modelId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(p => p.PolicyRequestId == modelId && p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<PolicyRequestFactor> GetPaymentInfoByPolicyId(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(x=>x.Payment)
                .Include(x=>x.PolicyRequest).ThenInclude(x=>x.PolicyRequestDetails)
                .Include(x=>x.PolicyRequest).ThenInclude(x=>x.Insurer).ThenInclude(x=>x.Insurance)
                .Include(x=>x.PolicyRequest).ThenInclude(x=>x.InsuredRequests).ThenInclude(x=>x.InsuredRequestVehicles).ThenInclude(x=>x.Vehicle)
                .Where(p => p.PolicyRequestId == id).SingleOrDefaultAsync(cancellationToken);

        }

        public async Task<PolicyRequestFactor> GetByIdNoTracking(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f => f.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<PolicyRequestFactor>> GetAllFactorsOfCompany(long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f => f.PolicyRequest.Insurer.CompanyId == companyId)
                .Include(i=> i.PolicyRequest)
                .Include(i=> i.Payment)
                .Include(i=> i.PolicyRequestFactorDetails)
                .GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }


        public async Task<PagedResult<PolicyRequestFactor>> GetAllPolicyFactorsOfCompany(long companyId, long policyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f => f.PolicyRequest.Insurer.CompanyId == companyId && f.PolicyRequestId == policyId)
                .Include(i => i.PolicyRequest)
                .Include(i => i.Payment)
                .Include(i=> i.PolicyRequestFactorDetails)
                .GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PolicyRequestFactor> GetFactorByIdNoTrakingWithDetails(long factorId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f => f.Id == factorId)
                .Include(i=> i.Payment)
                .Include(i=> i.PolicyRequest)
                .Include(i=> i.PolicyRequestFactorDetails)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<PolicyRequestFactor>> GetAllFactors(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking()
                .Include(i => i.PolicyRequest)
                .ThenInclude(th=> th.Insurer)
                .ThenInclude(th=> th.Company)
                .Include(i => i.Payment)
                .ThenInclude(th => th.PaymentGateway)
                .Include(i=> i.PolicyRequestFactorDetails)
                .GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequestFactor>> GetAllPersonFactors(long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f=> f.PolicyRequest.RequestPersonId == personId)
                .Include(i => i.PolicyRequest)
                .Include(i => i.Payment)
                .Include(i=> i.PolicyRequestFactorDetails)
                .GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PolicyRequestFactor> GetByIdWithPayment(long factorId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f => f.Id == factorId).Include(i => i.Payment).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PolicyRequestFactor> GetByIdWithPaymentAndDetails(long factorId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f => f.Id == factorId).Include(i => i.Payment).Include(i=> i.PolicyRequestFactorDetails).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<PolicyRequestFactor>> GetAllFactorsByAllParameters(Guid companyCode, long statusId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f => f.PolicyRequest.Insurer.Company.Code == companyCode && f.Payment.PaymentStatusId == statusId).Include(i => i.Payment).ThenInclude(th => th.PaymentGateway).Include(i => i.PolicyRequestFactorDetails).Include(i => i.PolicyRequest).ThenInclude(th => th.Insurer).ThenInclude(th => th.Company).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequestFactor>> GetAllFactorsByStatus(long statusId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f => f.Payment.PaymentStatusId == statusId).Include(i => i.Payment).ThenInclude(th => th.PaymentGateway).Include(i => i.PolicyRequestFactorDetails).Include(i => i.PolicyRequest).ThenInclude(th => th.Insurer).ThenInclude(th => th.Company).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequestFactor>> GetAllFactorsByCompany(Guid companyCode, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f => f.PolicyRequest.Insurer.Company.Code == companyCode).Include(i => i.Payment).ThenInclude(th => th.PaymentGateway).Include(i => i.PolicyRequestFactorDetails).Include(i => i.PolicyRequest).ThenInclude(th => th.Insurer).ThenInclude(th => th.Company).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequestFactor>> GetAllFactorsByPersonId(long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f => f.PolicyRequest.RequestPersonId == personId).Include(i => i.Payment).ThenInclude(th => th.PaymentGateway).Include(i => i.PolicyRequestFactorDetails).Include(i => i.PolicyRequest).ThenInclude(th => th.Insurer).ThenInclude(th => th.Company).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequestFactor>> GetAllFactorsByPersonAndStatusId(long personId, long statusId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f => f.PolicyRequest.RequestPersonId == personId && f.Payment.PaymentStatusId == statusId).Include(i => i.Payment).ThenInclude(th => th.PaymentGateway).Include(i => i.PolicyRequestFactorDetails).Include(i => i.PolicyRequest).ThenInclude(th => th.Insurer).ThenInclude(th => th.Company).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequestFactor>> GetAllCompanyFactorsByStatusId(long companyId, long statusId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f=> f.PolicyRequest.Insurer.CompanyId == companyId && f.Payment.PaymentStatusId == statusId).Include(i => i.Payment).ThenInclude(th => th.PaymentGateway).Include(i => i.PolicyRequestFactorDetails).Include(i => i.PolicyRequest).ThenInclude(th => th.Insurer).ThenInclude(th => th.Company).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PagedResult<PolicyRequestFactor>> GetAllCompanyFactors(long companyId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(f => f.PolicyRequest.Insurer.CompanyId == companyId).Include(i => i.Payment).ThenInclude(th => th.PaymentGateway).Include(i => i.PolicyRequestFactorDetails).Include(i => i.PolicyRequest).ThenInclude(th => th.Insurer).ThenInclude(th => th.Company).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }
    }
}
