using Common.Utilities;
using DAL.Models;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IPaymentGatewayRepository : IRepository<PaymentGateway>
    {
        Task<PagedResult<PaymentGateway>> GetAllGatewaysWithDetailsPaging(PageAbleModel pageAbleModel, CancellationToken cancellationToken);

        Task<PaymentGateway> GetGatewayByIdNoTraking(long id, CancellationToken cancellationToken);

        Task<PaymentGateway> GetGatewayByIdWithDetailsNoTraking(long id, CancellationToken cancellationToken);

        Task<List<PaymentGateway>> GetAllGatewaysWithDetailsList(CancellationToken cancellationToken);

    }
}
