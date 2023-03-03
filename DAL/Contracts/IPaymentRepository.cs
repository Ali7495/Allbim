using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using DAL.Models;
using Models.PageAble;

namespace DAL.Contracts
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        //Task<PagedResult<Payment>> GetAllPaymentsByAllParameters(Guid companyCode,long statusId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
        //Task<PagedResult<Payment>> GetAllPaymentsPersonId(long personId, PageAbleModel pageAbleModel, CancellationToken cancellationToken);
    }
}