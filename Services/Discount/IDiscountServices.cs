using Common.Utilities;
using Models.Discount;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IDiscountServices
    {
        Task<DiscountResultViewModel> CreateDiscount(DiscountInputViewModel viewModel, CancellationToken cancellationToken);
        Task<DiscountResultViewModel> GetDiscount(long id, CancellationToken cancellationToken);
        Task<PagedResult<DiscountResultViewModel>> GetAllDiscounts(PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<DiscountResultViewModel> UpdateDiscount(long id, DiscountInputViewModel viewModel, CancellationToken cancellationToken);
        Task<bool> DeleteDiscount(long id, CancellationToken cancellationToken);
    }
}
