using DAL.Contracts;
using Models.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using DAL.Models;
using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using Microsoft.EntityFrameworkCore;
using Models.PageAble;
using Models.Settings;

namespace Services.Info
{
    public class InfoService : IInfoService
    {
        private readonly IInfoRepository _infoRepository;
        private readonly IMapper _mapper;

        public InfoService(IInfoRepository infoRepository, IMapper mapper)
        {
            _infoRepository = infoRepository;
            _mapper = mapper;
        }


        //public async Task<PagedResult<InfoResultViewModel>> all(int? page, int? pageSize, CancellationToken cancellationToken)
        //{
        //    int pageNotNull = page ?? _pagingSettings.DefaultPage;
        //    int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;
        //    var result = await _infoRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
        //    return _mapper.Map<PagedResult<InfoResultViewModel>>(result);
        //}

        public async Task<InfoResultViewModel> Create(InfoInputViewModel CreateViewModel,
            CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DAL.Models.Info InfoInputData = _mapper.Map<DAL.Models.Info>(CreateViewModel);
                InfoInputData.IsDeleted = false;
                await _infoRepository.AddAsync(InfoInputData, cancellationToken);
                InfoResultViewModel InsertResult = _mapper.Map<InfoResultViewModel>(InfoInputData);
                transaction.Complete();
                return InsertResult;
            }
        }

        public async Task<bool> Delete(long ID, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var InfoData = await _infoRepository.GetByIdAsync(cancellationToken, ID);
                if (InfoData == null)
                    throw new CustomException("Info Not Found");
                InfoData.IsDeleted = true;
                await _infoRepository.UpdateAsync(InfoData, cancellationToken);
                transaction.Complete();
                return true;
            }
        }

        public async Task<InfoResultViewModel> Detail(long ID, CancellationToken cancellationToken)
        {
            try
            {
                var InfoData = await _infoRepository.GetByIdAsync(cancellationToken, ID);
                if (InfoData == null)
                    throw new CustomException("Info Not Found");
                return _mapper.Map<InfoResultViewModel>(InfoData);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public async Task<List<InfoResultViewModel>> GetAllWithoutPaging(CancellationToken cancellationToken)
        {
            var result = await _infoRepository.Table.AsNoTracking().ToListAsync(cancellationToken);
            return _mapper.Map<List<InfoResultViewModel>>(result);
        }

        
        public async Task<PagedResult<InfoResultViewModel>> GetAllPaging(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            PagedResult<DAL.Models.Info> comments = await _infoRepository.GetAllPaging(pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<InfoResultViewModel>>(comments);
        }
        
        
        public async Task<InfoResultViewModel> Update(long id, InfoInputViewModel UpdateViewModel,
            CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var InfoData = await _infoRepository.GetByIdAsync(cancellationToken, id);
                if (InfoData == null)
                    throw new CustomException("Info Not Found");
                InfoData.Key = UpdateViewModel.Key;
                InfoData.Slug = UpdateViewModel.Slug;
                InfoData.Value = UpdateViewModel.Value;
                await _infoRepository.UpdateAsync(InfoData, cancellationToken);
                InfoResultViewModel UpdateResult = _mapper.Map<InfoResultViewModel>(InfoData);
                transaction.Complete();
                return UpdateResult;
            }
        }
    }
}