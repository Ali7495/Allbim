using Common.Utilities;
using Models.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Models.PageAble;

namespace Services.Info
{
    public interface IInfoService
    {
        Task<InfoResultViewModel> Create(InfoInputViewModel CreateViewModel, CancellationToken cancellationToken);
        Task<bool> Delete(long id, CancellationToken cancellationToken);
        Task<InfoResultViewModel> Update(long id, InfoInputViewModel UpdateViewModel, CancellationToken cancellationToken);
        Task<InfoResultViewModel> Detail(long ID, CancellationToken cancellationToken);
        //Task<PagedResult<InfoResultViewModel>> all(int? page, int? pageSize, CancellationToken cancellationToken);
        Task<List<InfoResultViewModel>> GetAllWithoutPaging(CancellationToken cancellationToken);

        Task<PagedResult<InfoResultViewModel>> GetAllPaging(PageAbleResult pageAbleResult,
            CancellationToken cancellationToken);
    }
}
