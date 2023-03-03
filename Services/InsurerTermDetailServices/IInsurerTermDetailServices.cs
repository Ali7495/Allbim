using Common.Utilities;
using Models.InsurerTernDetail;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.InsurerTermDetailServices
{
    public interface IInsurerTermDetailServices
    {
        #region Create
        Task<TermDetailResultViewModel> CreateInsurerTermDetail(Guid code, long insuranceId, long termId, TermDetailInputViewModel viewModel, CancellationToken cancellationToken);


        Task<TermDetailResultViewModel> CreateInsurerTermDetailMine(long userId, long insuranceId, long termId, TermDetailInputViewModel viewModel, CancellationToken cancellationToken);

        #endregion

        #region Get

        Task<PagedResult<TermDetailResultViewModel>> GetAllInsurerTermDetails(Guid code, long insuranceId, long insurerTermId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);


        Task<List<TermDetailResultViewModel>> GetAllInsurerTermDetailList(Guid code, long insuranceId, long insurerTermId, CancellationToken cancellationToken);

        Task<TermDetailResultViewModel> GetInsurerTermDetail(Guid code, long insuranceId, long insurerTermId, long detailId, CancellationToken cancellationToken);





        Task<PagedResult<TermDetailResultViewModel>> GetAllInsurerTermDetailsMine(long userId, long insuranceId, long insurerTermId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);


        Task<List<TermDetailResultViewModel>> GetAllInsurerTermDetailListMine(long userId, long insuranceId, long insurerTermId, CancellationToken cancellationToken);

        Task<TermDetailResultViewModel> GetInsurerTermDetailMine(long userId, long insuranceId, long insurerTermId, long detailId, CancellationToken cancellationToken);
        #endregion

        #region Update

        Task<TermDetailResultViewModel> UpdateInsurerTermDetailAsync(Guid code, long insuranceId, long termId, long detailId, TermDetailInputViewModel viewModel, CancellationToken cancellationToken);


        Task<TermDetailResultViewModel> UpdateInsurerTermDetailAsyncMine(long userId, long insuranceId, long termId, long detailId, TermDetailInputViewModel viewModel, CancellationToken cancellationToken);

        #endregion

        #region Delete

        Task<bool> DeleteInsurerTermDetailAsync(Guid code, long insuranceId, long termId, long detailId, CancellationToken cancellationToken);


        Task<bool> DeleteInsurerTermDetailAsyncMine(long userId, long insuranceId, long termId, long detailId, CancellationToken cancellationToken);

        #endregion
    }
}
