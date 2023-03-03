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
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<PagedResult<Discount>> GetAllDiscounts(PageAbleModel pageAbleModel, CancellationToken cancellationToken);
    }
}
