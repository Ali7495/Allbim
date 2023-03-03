using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public interface IPaymentStatusRepository : IRepository<PaymentStatus>
    {
        Task<List<PaymentStatus>> GetAllStatusesNoTraking(CancellationToken cancellationToken);
        Task<PaymentStatus> GetStatusByIdNoTracking(long statusId, CancellationToken cancellationToken);
    }
}
