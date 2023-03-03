using Common.Utilities;
using DAL.Models;
using Models.Insurance;
using Models.InsurerTerm;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IInsurerTermService
    {
        #region Create
        Task<InsurerTermDetailedResultViewModel> CreateInsurerTerm(Guid code, long insuranceId, InsurerTermInputViewModel viewModel, CancellationToken cancellationToken);
        Task<InsurerTermViewModel> CreateInsurerTermAsync(InsurerTermViewModel insurerTermViewModel, CancellationToken cancellationToken);


        Task<InsurerTermDetailedResultViewModel> CreateInsurerTermMine(long userId, long insuranceId, InsurerTermInputViewModel viewModel, CancellationToken cancellationToken);
        #endregion

        #region Update

        Task<InsurerTermDetailedResultViewModel> UpdateInsurerTermAsync(Guid code, long insuranceId, long id, InsurerTermInputViewModel viewModel, CancellationToken cancellationToken);



        Task<InsurerTermDetailedResultViewModel> UpdateInsurerTermAsyncMine(long userId, long insuranceId, long id, InsurerTermInputViewModel viewModel, CancellationToken cancellationToken);
        #endregion

        #region Delete
        Task<bool> DeleteInsurerTerm(long id, CancellationToken cancellationToken);
        Task<bool> DeleteInsurerTermAsync(long id, CancellationToken cancellationToken);


        Task<bool> DeleteInsurerTermMine(long userId, long insuranceId, long id, CancellationToken cancellationToken);
        #endregion

        #region Get
        Task<InsurerTermDetailedResultViewModel> GetInsurerTerm(long id, CancellationToken cancellationToken);
        //Task<PagedResult<InsurerTermViewModel>> GetAllInsurerTerms(int? page, int? pageSize, CancellationToken cancellationToken);


        Task<PagedResult<InsurerTermResultViewModel>> GetAllInsurerTerms(Guid code, long insuranceId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<InsurerViewModel> GetInsurerAsync(long id, CancellationToken cancellationToken);
        Task<InsurerTermViewModel> GetInsurerTermAsync(long id, CancellationToken cancellationToken);





        Task<InsurerTermDetailedResultViewModel> GetInsurerTermMine(long userId, long insuranceId, long id, CancellationToken cancellationToken);
        Task<PagedResult<InsurerTermResultViewModel>> GetAllInsurerTermsMine(long userId, long insuranceId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        #endregion
    }
}
