using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PaymentGatewayRepository : Repository<PaymentGateway>, IPaymentGatewayRepository
    {
        public PaymentGatewayRepository(AlbimDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<PaymentGateway>> GetAllGatewaysWithDetailsList(CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(i=> i.PaymentGatewayDetails).ToListAsync(cancellationToken);
        }

        public async Task<PagedResult<PaymentGateway>> GetAllGatewaysWithDetailsPaging(PageAbleModel pageAbleModel, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(i => i.PaymentGatewayDetails).GetNewPagedAsync(cancellationToken, pageAbleModel, null, null);
        }

        public async Task<PaymentGateway> GetGatewayByIdNoTraking(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Where(g => g.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PaymentGateway> GetGatewayByIdWithDetailsNoTraking(long id, CancellationToken cancellationToken)
        {
            return await Table.AsNoTracking().Include(i => i.PaymentGatewayDetails).Where(g => g.Id == id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
