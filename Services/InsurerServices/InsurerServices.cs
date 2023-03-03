using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Models.Center;
using Models.Insurer;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.InsurerServices
{
    public class InsurerServices : IInsurerServices
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IInsurerRepository _insurerRepository;
        private readonly IInsurerTermRepository _insurerTermRepository;
        private readonly IPolicyRequestRepository _policyRequestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPersonCompanyRepository _personCompanyRepository;
        private readonly IMapper _mapper;

        public InsurerServices(ICompanyRepository companyRepository, IInsuranceRepository insuranceRepository, IInsurerRepository insurerRepository, IInsurerTermRepository insurerTermRepository, IPolicyRequestRepository policyRequestRepository, IUserRepository userRepository, IPersonCompanyRepository personCompanyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _insuranceRepository = insuranceRepository;
            _insurerRepository = insurerRepository;
            _insurerTermRepository = insurerTermRepository;
            _policyRequestRepository = policyRequestRepository;
            _userRepository = userRepository;
            _personCompanyRepository = personCompanyRepository;
            _mapper = mapper;
        }

        public async Task<InsurerResultViewModel> CreateInsurer(Guid code, long id, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");

            }

            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه وجود ندارد");
            }

            Insurer insurer = await _insurerRepository.GetWithInsuranceIdAndCompanyCodeNoTracking(code, id, cancellationToken);
            if (insurer != null)
            {
                throw new BadRequestException("بیمه گر وجود دارد");
            }

            Insurer model = new Insurer()
            {
                InsuranceId = id,
                CompanyId = company.Id,
            };

            await _insurerRepository.AddAsync(model, cancellationToken);

            return _mapper.Map<InsurerResultViewModel>(model);

        }



        public async Task<InsurerResultViewModel> GetInsurer(Guid code, long id, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه وجود ندارد");
            }

            Insurer insurer = await _insurerRepository.GetWithInsuranceIdAndCompanyCodeNoTracking(code, id, cancellationToken);

            return _mapper.Map<InsurerResultViewModel>(insurer);
        }

        public async Task<PagedResult<InsurerResultViewModel>> GetInsurersByCompany(Guid code, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<Insurer> insurers = await _insurerRepository.GetAsyncAdvanced(cancellationToken,
                pageAbleModel, x => x.CompanyId == company.Id, i => i.Company, i => i.Insurance);

            return _mapper.Map<PagedResult<InsurerResultViewModel>>(insurers);
        }
        public async Task<string> DeleteInsurer(Guid code, long id, CancellationToken cancellationToken)
        {

            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه وجود ندارد");
            }

            Insurer insurer = await _insurerRepository.GetWithInsuranceIdAndCompanyCode(code, id, cancellationToken);
            if (insurer == null)
            {
                throw new BadRequestException("بیمه گر وجود ندارد");
            }

            int insurerTermCount = await _insurerTermRepository.GetCountByInsurerId(insurer.Id, cancellationToken);
            if (insurerTermCount != 0)
            {
                throw new BadRequestException("برای این بیمه گر قوانین وجود دارد و قابل حذف نیست");
            }

            int policyCount = await _policyRequestRepository.GetCountByInsurerId(insurer.Id, cancellationToken);
            if (policyCount != 0)
            {
                throw new BadRequestException("برای این بیمه گر درخواست وجود دارد و قابل حذف نیست");
            }

            await _insurerRepository.DeleteAsync(insurer, cancellationToken);

            return true.ToString();
        }





        public async Task<InsurerResultViewModel> CreateInsurerMine(long userId, long insuranceId, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);
            if (personCompany == null)
            {
                throw new BadRequestException("شما ارتباطی با هیچ شرکتی ندارید");
            }

            Company company = await _companyRepository.GetByIdNoTracking(personCompany.CompanyId.GetValueOrDefault(), cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");

            }

            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه وجود ندارد");
            }

            Insurer insurer = await _insurerRepository.GetWithInsuranceIdAndCompanyCodeNoTracking(company.Code, insuranceId, cancellationToken);
            if (insurer != null)
            {
                throw new BadRequestException("بیمه گر وجود دارد");
            }

            Insurer model = new Insurer()
            {
                InsuranceId = insuranceId,
                CompanyId = company.Id,
            };

            await _insurerRepository.AddAsync(model, cancellationToken);

            return _mapper.Map<InsurerResultViewModel>(model);
        }

        public async Task<InsurerResultViewModel> GetInsurerMine(long userId, long insuranceId, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);
            if (personCompany == null)
            {
                throw new BadRequestException("شما ارتباطی با هیچ شرکتی ندارید");
            }

            Company company = await _companyRepository.GetByIdNoTracking(personCompany.CompanyId.GetValueOrDefault(), cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");

            }

            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه وجود ندارد");
            }

            Insurer insurer = await _insurerRepository.GetWithInsuranceIdAndCompanyCodeNoTracking(company.Code, insuranceId, cancellationToken);

            return _mapper.Map<InsurerResultViewModel>(insurer);
        }

        public async Task<PagedResult<InsurerResultViewModel>> GetInsurersByCompanyMine(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);
            if (personCompany == null)
            {
                throw new BadRequestException("شما ارتباطی با هیچ شرکتی ندارید");
            }

            Company company = await _companyRepository.GetByIdNoTracking(personCompany.CompanyId.GetValueOrDefault(), cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");

            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<Insurer> insurers = await _insurerRepository.GetAsyncAdvanced(cancellationToken,
                pageAbleModel, x => x.CompanyId == company.Id, i => i.Company, i => i.Insurance);

            return _mapper.Map<PagedResult<InsurerResultViewModel>>(insurers);
        }

        public async Task<string> DeleteInsurerMine(long userId, long insuranceId, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);
            if (personCompany == null)
            {
                throw new BadRequestException("شما ارتباطی با هیچ شرکتی ندارید");
            }

            Company company = await _companyRepository.GetByIdNoTracking(personCompany.CompanyId.GetValueOrDefault(), cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");

            }

            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه وجود ندارد");
            }

            Insurer insurer = await _insurerRepository.GetWithInsuranceIdAndCompanyCode(company.Code, insuranceId, cancellationToken);
            if (insurer == null)
            {
                throw new BadRequestException("بیمه گر وجود ندارد");
            }

            int insurerTermCount = await _insurerTermRepository.GetCountByInsurerId(insurer.Id, cancellationToken);
            if (insurerTermCount != 0)
            {
                throw new BadRequestException("برای این بیمه گر قوانین وجود دارد و قابل حذف نیست");
            }

            int policyCount = await _policyRequestRepository.GetCountByInsurerId(insurer.Id, cancellationToken);
            if (policyCount != 0)
            {
                throw new BadRequestException("برای این بیمه گر درخواست وجود دارد و قابل حذف نیست");
            }

            await _insurerRepository.DeleteAsync(insurer, cancellationToken);

            return true.ToString();
        }
    }
}
