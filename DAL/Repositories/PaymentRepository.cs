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
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AlbimDbContext dbContext)
            : base(dbContext)
        {
        }



        //public async Task<PagedResult<Payment>> GetAllPaymentsByAllParameters(Guid companyCode, long statusId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        //{
        //    return await Table.AsNoTracking().Where(p=> p.).Include(i => i.PolicyRequestFactors).ThenInclude(th => th.PolicyRequestFactorDetails).Include(i => i.PolicyRequestFactors).ThenInclude(th => th.PolicyRequest).ThenInclude(th => th.Insurer).ThenInclude(th => th.Company).Include(i => i.PaymentGateway).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        //}

        //public async Task<PagedResult<Payment>> GetAllPaymentsPersonId(long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        //{
        //    return await Table.AsNoTracking().Where(p => p.PolicyRequestFactors.FirstOrDefault().PolicyRequest.RequestPersonId == personId).Include(i => i.PolicyRequestFactors).ThenInclude(th => th.PolicyRequestFactorDetails).Include(i => i.PolicyRequestFactors).ThenInclude(th => th.PolicyRequest).ThenInclude(th => th.Insurer).ThenInclude(th => th.Company).Include(i => i.PaymentGateway).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        //}
    }
    
}
