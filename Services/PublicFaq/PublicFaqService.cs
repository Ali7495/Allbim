using AutoMapper;
using Common.Exceptions;
using DAL.Contracts;
using DAL.Models;
using Models.PublicFaq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using Models.FAQ;
using Models.PageAble;

namespace Services.PublicFaq
{
    
    public class PublicFaqService : IPublicFaqService
    {
        #region Property
        private readonly IPublicFaqRepository _publicFaqRepository;
        private readonly IMapper _mapper;
        #endregion
        #region Ctor
        public PublicFaqService(IPublicFaqRepository publicFaqRepository, IMapper mapper)
        {
            _publicFaqRepository = publicFaqRepository;
            _mapper = mapper;
        }

        public async Task<PublicFaqResultViewModel> Create(PublicFaqInputViewModel CreateViewModel, CancellationToken cancellationToken)
        {
            await _publicFaqRepository.AddAsync(new DAL.Models.Faq { Answer = CreateViewModel.Answer , IsDeleted = false , Question = CreateViewModel.Question } ,cancellationToken);
            return _mapper.Map<PublicFaqResultViewModel>(CreateViewModel);
        }

        public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            var FaqResult = await _publicFaqRepository.GetByIdAsync(cancellationToken,id);
            if (FaqResult == null)
                throw new NotFoundException("موردی یافت نشد");
            FaqResult.IsDeleted = true;
            await _publicFaqRepository.UpdateAsync(FaqResult,cancellationToken);
            return true;
        }

        public async Task<List<PublicFaqResultViewModel>> GetAllWithoutPaging(CancellationToken cancellationToken)
        {
            List<Faq> GetAll = await _publicFaqRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<PublicFaqResultViewModel>>(GetAll);
        }
        
        public async Task<PagedResult<PublicFaqResultViewModel>> GetAllPaging(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<Faq> FAQs = await _publicFaqRepository.GetAll( pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<PublicFaqResultViewModel>>(FAQs);
        }

        public async Task<PublicFaqResultViewModel> Detail(long id, CancellationToken cancellationToken)
        {
            var FaqResult = await _publicFaqRepository.GetByIdAsync(cancellationToken,id);
            if (FaqResult == null)
                throw new NotFoundException("موردی یافت نشد");
            return _mapper.Map<PublicFaqResultViewModel>(FaqResult);
        }

        public async Task<PublicFaqResultViewModel> Update(long id, PublicFaqInputViewModel UpdateViewModel, CancellationToken cancellationToken)
        {
            var FaqResult = await _publicFaqRepository.GetByIdAsync(cancellationToken,id);
            if (FaqResult == null)
                throw new NotFoundException("موردی یافت نشد");
            FaqResult.Answer = UpdateViewModel.Answer;
            FaqResult.Question = UpdateViewModel.Question;
            await _publicFaqRepository.UpdateAsync(FaqResult, cancellationToken);
            return _mapper.Map<PublicFaqResultViewModel>(UpdateViewModel);
        }
        #endregion
    }
}
