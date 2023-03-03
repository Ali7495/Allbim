using Models.PublicFaq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using Models.PageAble;

namespace Services.PublicFaq
{
    public interface IPublicFaqService
    {
        Task<PublicFaqResultViewModel> Create(PublicFaqInputViewModel CreateViewModel, CancellationToken cancellationToken);
        Task<bool> Delete(long id, CancellationToken cancellationToken);
        Task<PublicFaqResultViewModel> Update(long id, PublicFaqInputViewModel UpdateViewModel, CancellationToken cancellationToken);
        Task<PublicFaqResultViewModel> Detail(long id, CancellationToken cancellationToken);
        Task<List<PublicFaqResultViewModel>> GetAllWithoutPaging(CancellationToken cancellationToken);

        Task<PagedResult<PublicFaqResultViewModel>> GetAllPaging(PageAbleResult pageAbleResult,
            CancellationToken cancellationToken);
    }
}
