using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PaymentStatusRepository : Repository<PaymentStatus>, IPaymentStatusRepository
    {
        public PaymentStatusRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<PaymentStatus>> GetAllStatusesNoTraking(CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<PaymentStatus> GetStatusByIdNoTracking(long statusId, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(s=> s.Id == statusId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
