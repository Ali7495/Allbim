using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Models.FAQ;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class InsuranceFAQService : IInsuranceFAQServices
    {
        private readonly IInsuranceFAQRepository _fAQRepository;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IMapper _mapper;

        public InsuranceFAQService(IInsuranceFAQRepository fAQRepository, IInsuranceRepository insuranceRepository, IMapper mapper)
        {
            _fAQRepository = fAQRepository;
            _insuranceRepository = insuranceRepository;
            _mapper = mapper;
        }

        

        public async Task<PagedResult<FAQResultViewModel>> GetAllInsuranceFAQs(long insuranceId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetInsuranceByIdNoTracking(insuranceId, cancellationToken);
            if (insurance == null)
            {
                throw new BadRequestException("این بیمه وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<InsuranceFaq> FAQs = await _fAQRepository.GetAllFAQ(insuranceId, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<FAQResultViewModel>>(FAQs);
        }

        public async Task<FAQResultViewModel> GetInsuranceFAQ(long insuranceId, long faqId, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetInsuranceByIdNoTracking(insuranceId, cancellationToken);
            if (insurance == null)
            {
                throw new BadRequestException("این بیمه وجود ندارد");
            }

            InsuranceFaq insuranceFaq = await _fAQRepository.GetByIdNoTracking(faqId, cancellationToken);

            return _mapper.Map<FAQResultViewModel>(insuranceFaq);
        }

        public async Task<FAQResultViewModel> PostNewInsuranceFAQ(long insuranceId, FAQInputViewModel model, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetInsuranceByIdNoTracking(insuranceId, cancellationToken);
            if (insurance == null)
            {
                throw new BadRequestException("این بیمه وجود ندارد");
            }

            InsuranceFaq insuranceFaq = _mapper.Map<InsuranceFaq>(model);
            insuranceFaq.InsuranceId = insuranceId;
            await _fAQRepository.AddAsync(insuranceFaq, cancellationToken);

            return _mapper.Map<FAQResultViewModel>(insuranceFaq);
        }

        public async Task<FAQResultViewModel> UpdateInsuranceFAQ(long insuranceId, long faqId, FAQInputViewModel model, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetInsuranceByIdNoTracking(insuranceId, cancellationToken);
            if (insurance == null)
            {
                throw new BadRequestException("این بیمه وجود ندارد");
            }

            InsuranceFaq insuranceFaq = await _fAQRepository.GetByIdNoTracking(faqId, cancellationToken);
            if (insuranceFaq == null)
            {
                throw new BadRequestException("این پرسش وجود ندارد");
            }

            insuranceFaq.Answer = model.Answer;
            insuranceFaq.Question = model.Question;

            await _fAQRepository.UpdateAsync(insuranceFaq, cancellationToken);

            return _mapper.Map<FAQResultViewModel>(insuranceFaq);
        }

        public async Task<bool> DeleteInsuranceFAQ(long insuranceId, long faqId, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetInsuranceByIdNoTracking(insuranceId, cancellationToken);
            if (insurance == null)
            {
                throw new BadRequestException("این بیمه وجود ندارد");
            }

            InsuranceFaq insuranceFaq = await _fAQRepository.GetByIdNoTracking(faqId, cancellationToken);
            if (insuranceFaq == null)
            {
                throw new BadRequestException("این پرسش وجود ندارد");
            }

            insuranceFaq.IsDeleted = true;

            await _fAQRepository.UpdateAsync(insuranceFaq, cancellationToken);

            return true;
        }
    }
}
