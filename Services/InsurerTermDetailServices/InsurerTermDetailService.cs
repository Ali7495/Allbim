using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
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
    public class InsurerTermDetailService : IInsurerTermDetailServices
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IInsurerTermRepository _insurerTermRepository;
        private readonly IInsurerRepository _insurerRepository;
        private readonly IInsurerTermDetailRepository _insurerTermDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPersonCompanyRepository _personCompanyRepository;
        private readonly IMapper _mapper;


        public InsurerTermDetailService(ICompanyRepository companyRepository, IInsuranceRepository insuranceRepository, IInsurerTermRepository insurerTermRepository, IInsurerRepository insurerRepository, IInsurerTermDetailRepository insurerTermDetailRepository, IUserRepository userRepository, IPersonCompanyRepository personCompanyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _insuranceRepository = insuranceRepository;
            _insurerTermRepository = insurerTermRepository;
            _insurerRepository = insurerRepository;
            _insurerTermDetailRepository = insurerTermDetailRepository;
            _userRepository = userRepository;
            _personCompanyRepository = personCompanyRepository;
            _mapper = mapper;
        }

        public async Task<bool> IsDataValidCommon(Guid code, long insuranceId, long termId, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد ");
            }

            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه وجود ندارد");
            }

            Insurer insurer = await _insurerRepository.GetWithInsuranceIdAndCompanyCodeNoTracking(code, insurance.Id, cancellationToken);
            if (insurer == null)
            {
                throw new BadRequestException("بیمه گر وجود ندارد");
            }

            InsurerTerm insurerTerm = await _insurerTermRepository.GetByIdAsync(cancellationToken, termId);
            if (insurerTerm == null)
            {
                throw new BadRequestException("قانوین بیمه گر وجود ندارد");
            }

            return true;
        }

        public async Task<bool> IsDataValidCommonByUserIdId(long userId, long insuranceId, long termId, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserByIdNoTracking(userId, cancellationToken);
            if (user == null)
            {
                throw new BadRequestException("شما در سیستم نقشی ندارید");
            }

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);
            if (personCompany == null)
            {
                throw new BadRequestException("شما با هیچ شرکتی در ارتباط نیستید");
            }

            Company company = await _companyRepository.GetByIdNoTracking(personCompany.CompanyId.Value, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد ");
            }

            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه وجود ندارد");
            }

            Insurer insurer = await _insurerRepository.GetWithInsuranceIdAndCompanyCodeNoTracking(company.Code, insurance.Id, cancellationToken);
            if (insurer == null)
            {
                throw new BadRequestException("بیمه گر وجود ندارد");
            }

            InsurerTerm insurerTerm = await _insurerTermRepository.GetByIdAsync(cancellationToken, termId);
            if (insurerTerm == null)
            {
                throw new BadRequestException("قانوین بیمه گر وجود ندارد");
            }

            return true;
        }


        public async Task<TermDetailResultViewModel> CreateInsurerTermDetail(Guid code, long insuranceId, long termId, TermDetailInputViewModel viewModel, CancellationToken cancellationToken)
        {
            await IsDataValidCommon(code, insuranceId, termId, cancellationToken);

            InsurerTermDetail termDetail = _mapper.Map<InsurerTermDetail>(viewModel);

            termDetail.InsurerTermId = termId;

            await _insurerTermDetailRepository.AddAsync(termDetail, cancellationToken);

            return _mapper.Map<TermDetailResultViewModel>(termDetail);
        }

        public async Task<PagedResult<TermDetailResultViewModel>> GetAllInsurerTermDetails(Guid code, long insuranceId, long insurerTermId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            await IsDataValidCommon(code, insuranceId, insurerTermId, cancellationToken);

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<InsurerTermDetail> insurerTermDetails = await _insurerTermDetailRepository.GetAllTermDetails(insurerTermId, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<TermDetailResultViewModel>>(insurerTermDetails);
        }

        public async Task<List<TermDetailResultViewModel>> GetAllInsurerTermDetailList(Guid code, long insuranceId, long insurerTermId, CancellationToken cancellationToken)
        {
            await IsDataValidCommon(code, insuranceId, insurerTermId, cancellationToken);

            List<InsurerTermDetail> details = await _insurerTermDetailRepository.GetAllTermDetailList(insurerTermId, cancellationToken);

            return _mapper.Map<List<TermDetailResultViewModel>>(details);
        }

        public async Task<InsurerTermDetail> GetDetialCommon(long id, CancellationToken cancellationToken)
        {
            InsurerTermDetail insurerTermDetail = await _insurerTermDetailRepository.GetTermDetailNoTracking(id, cancellationToken);
            if (insurerTermDetail == null)
            {
                throw new BadRequestException("جزئیات قانون بیمه گر وجود ندارد");
            }

            return insurerTermDetail;
        }

        public async Task<TermDetailResultViewModel> UpdateInsurerTermDetailAsync(Guid code, long insuranceId, long termId, long detailId, TermDetailInputViewModel viewModel, CancellationToken cancellationToken)
        {
            await IsDataValidCommon(code, insuranceId, termId, cancellationToken);

            InsurerTermDetail insurerTermDetail = await GetDetialCommon(detailId, cancellationToken);

            insurerTermDetail = _mapper.Map<InsurerTermDetail>(viewModel);
            insurerTermDetail.Id = detailId;
            insurerTermDetail.InsurerTermId = termId;

            await _insurerTermDetailRepository.UpdateAsync(insurerTermDetail, cancellationToken);

            return _mapper.Map<TermDetailResultViewModel>(insurerTermDetail);
        }

        public async Task<bool> DeleteInsurerTermDetailAsync(Guid code, long insuranceId, long termId, long detailId, CancellationToken cancellationToken)
        {
            await IsDataValidCommon(code, insuranceId, termId, cancellationToken);

            InsurerTermDetail insurerTermDetail = await GetDetialCommon(detailId, cancellationToken);

            await _insurerTermDetailRepository.DeleteAsync(insurerTermDetail, cancellationToken);

            return true;
        }

        public async Task<TermDetailResultViewModel> GetInsurerTermDetail(Guid code, long insuranceId, long insurerTermId, long detailId, CancellationToken cancellationToken)
        {
            await IsDataValidCommon(code, insuranceId, insurerTermId, cancellationToken);

            InsurerTermDetail insurerTermDetail = await GetDetialCommon(detailId, cancellationToken);

            return _mapper.Map<TermDetailResultViewModel>(insurerTermDetail);
        }


        public async Task<TermDetailResultViewModel> CreateInsurerTermDetailMine(long userId, long insuranceId, long termId, TermDetailInputViewModel viewModel, CancellationToken cancellationToken)
        {
            await IsDataValidCommonByUserIdId(userId, insuranceId, termId, cancellationToken);

            InsurerTermDetail termDetail = _mapper.Map<InsurerTermDetail>(viewModel);

            termDetail.InsurerTermId = termId;

            await _insurerTermDetailRepository.AddAsync(termDetail, cancellationToken);

            return _mapper.Map<TermDetailResultViewModel>(termDetail);
        }

        public async Task<PagedResult<TermDetailResultViewModel>> GetAllInsurerTermDetailsMine(long userId, long insuranceId, long insurerTermId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            await IsDataValidCommonByUserIdId(userId, insuranceId, insurerTermId, cancellationToken);

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<InsurerTermDetail> insurerTermDetails = await _insurerTermDetailRepository.GetAllTermDetails(insurerTermId, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<TermDetailResultViewModel>>(insurerTermDetails);
        }

        public async Task<List<TermDetailResultViewModel>> GetAllInsurerTermDetailListMine(long userId, long insuranceId, long insurerTermId, CancellationToken cancellationToken)
        {
            await IsDataValidCommonByUserIdId(userId, insuranceId, insurerTermId, cancellationToken);

            List<InsurerTermDetail> details = await _insurerTermDetailRepository.GetAllTermDetailList(insurerTermId, cancellationToken);

            return _mapper.Map<List<TermDetailResultViewModel>>(details);
        }

        public async Task<TermDetailResultViewModel> GetInsurerTermDetailMine(long userId, long insuranceId, long insurerTermId, long detailId, CancellationToken cancellationToken)
        {
            await IsDataValidCommonByUserIdId(userId, insuranceId, insurerTermId, cancellationToken);

            InsurerTermDetail insurerTermDetail = await GetDetialCommon(detailId, cancellationToken);

            return _mapper.Map<TermDetailResultViewModel>(insurerTermDetail);
        }

        public async Task<TermDetailResultViewModel> UpdateInsurerTermDetailAsyncMine(long userId, long insuranceId, long termId, long detailId, TermDetailInputViewModel viewModel, CancellationToken cancellationToken)
        {
            await IsDataValidCommonByUserIdId(userId, insuranceId, termId, cancellationToken);

            InsurerTermDetail insurerTermDetail = await GetDetialCommon(detailId, cancellationToken);

            insurerTermDetail = _mapper.Map<InsurerTermDetail>(viewModel);
            insurerTermDetail.Id = detailId;
            insurerTermDetail.InsurerTermId = termId;

            await _insurerTermDetailRepository.UpdateAsync(insurerTermDetail, cancellationToken);

            return _mapper.Map<TermDetailResultViewModel>(insurerTermDetail);
        }

        public async Task<bool> DeleteInsurerTermDetailAsyncMine(long userId, long insuranceId, long termId, long detailId, CancellationToken cancellationToken)
        {
            await IsDataValidCommonByUserIdId(userId, insuranceId, termId, cancellationToken);

            InsurerTermDetail insurerTermDetail = await GetDetialCommon(detailId, cancellationToken);

            await _insurerTermDetailRepository.DeleteAsync(insurerTermDetail, cancellationToken);

            return true;
        }
    }
}
