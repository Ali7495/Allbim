using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using DAL.Repositories;
using Microsoft.Extensions.Options;
using Models.Insurance;
using Models.InsurerTerm;
using Models.PageAble;
using Models.Settings;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class InsurerTermService : IInsurerTermService
    {
        private readonly IInsurerRepository _insurerRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IInsurerTermRepository _insurerTermRepository;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPersonCompanyRepository _personCompanyRepository;
        private readonly PagingSettings _pagingSettings;
        private readonly IMapper _mapper;

        public InsurerTermService(IInsurerTermRepository insurerTermRepository, IOptionsSnapshot<PagingSettings> pagingSettings, IInsurerRepository insurerRepository, ICompanyRepository companyRepository, IInsuranceRepository insuranceRepository, IUserRepository userRepository, IPersonCompanyRepository personCompanyRepository, IMapper mapper)
        {
            _mapper = mapper;
            _insurerTermRepository = insurerTermRepository;
            _pagingSettings = pagingSettings.Value;
            _insurerRepository = insurerRepository;
            _companyRepository = companyRepository;
            _insuranceRepository = insuranceRepository;
            _userRepository = userRepository;
            _personCompanyRepository = personCompanyRepository;
        }


        #region Create
        public async Task<InsurerTermDetailedResultViewModel> CreateInsurerTerm(Guid code, long insuranceId, InsurerTermInputViewModel viewModel, CancellationToken cancellationToken)
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


            InsurerTerm insurerTerm = _mapper.Map<InsurerTerm>(viewModel);
            insurerTerm.InsurerId = insurer.Id;


            await _insurerTermRepository.AddAsync(insurerTerm, cancellationToken);

            return _mapper.Map<InsurerTermDetailedResultViewModel>(insurerTerm);
        }

        public async Task<InsurerTermViewModel> CreateInsurerTermAsync(InsurerTermViewModel insurerTermViewModel, CancellationToken cancellationToken)
        {
            bool insurerIdIsValid = await _insurerRepository.GetByIdAsync(cancellationToken, insurerTermViewModel.InsurerId) != null;
            if (!insurerIdIsValid)
                throw new BadRequestException("بیمه گر وجود ندارد");

            InsurerTerm model = new InsurerTerm
            {
                //Type = insurerTermViewModel.Type,
                //Field = insurerTermViewModel.Field,
                //Criteria = insurerTermViewModel.Criteria,
                Value = insurerTermViewModel.Value,
                Discount = insurerTermViewModel.Discount,
                CalculationTypeId = insurerTermViewModel.CalculationTypeId,
                CreatedBy = insurerTermViewModel.CreatedBy,
                InsurerId = insurerTermViewModel.InsurerId
            };

            await _insurerTermRepository.AddAsync(model, cancellationToken);

            return _mapper.Map<InsurerTermViewModel>(model);
        }





        public async Task<InsurerTermDetailedResultViewModel> CreateInsurerTermMine(long userId, long insuranceId, InsurerTermInputViewModel viewModel, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);
            if (personCompany == null)
            {
                throw new BadRequestException("شما با هیچ شرکتی ارتباطی ندارید");
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

            Insurer insurer = await _insurerRepository.GetWithInsuranceIdAndCompanyCodeNoTracking(company.Code, insurance.Id, cancellationToken);
            if (insurer == null)
            {
                throw new BadRequestException("بیمه گر وجود ندارد");
            }

            //InsurerTerm insurerTerm = new InsurerTerm()
            //{
            //    InsurerId = insurer.Id,
            //    Type = viewModel.Type,
            //    Field = viewModel.Field,
            //    Criteria = viewModel.Criteria,
            //    Value = viewModel.Value,
            //    Discount = viewModel.Discount,
            //    CalculationType = viewModel.CalculationType,
            //    //Insurer = insurer                
            //};

            InsurerTerm insurerTerm = _mapper.Map<InsurerTerm>(viewModel);
            //insurerTerm.Type = 1;
            insurerTerm.InsurerId = insurer.Id;

            await _insurerTermRepository.AddAsync(insurerTerm, cancellationToken);

            return _mapper.Map<InsurerTermDetailedResultViewModel>(insurerTerm);
        }

        #endregion

        #region Update
        public async Task<InsurerTermViewModel> UpdateInsurace(long id, InsurerTermViewModel insurerTermViewModel, CancellationToken cancellationToken)
        {
            InsurerTerm model = await _insurerTermRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new BadRequestException("بیمه وجود ندارد");

            model.Value = insurerTermViewModel.Value;
            model.CalculationTypeId = insurerTermViewModel.CalculationTypeId;



            await _insurerTermRepository.UpdateAsync(model, cancellationToken);
            return _mapper.Map<InsurerTermViewModel>(model);
        }
        // public async Task<InsurerViewModel> UpdateInsurerAsync(long id, InsurerViewModel insurerViewModel, CancellationToken cancellationToken)
        // {
        //     var model = await _insurerRepository.GetByIdAsync(cancellationToken, id);
        //     if (model == null)
        //         throw new BadRequestException("بیمه گر ");
        //
        //     var insurerTermIdIsValid = _insurerTermRepository.GetByIdAsync(cancellationToken, insurerViewModel.CompanyId) != null;
        //     if (!insurerTermIdIsValid)
        //         throw new BadRequestException("بیمه");
        //
        //     var companyIdIsValid = _companyRepository.GetByIdAsync(cancellationToken, insurerViewModel.CompanyId) != null;
        //     if (!companyIdIsValid)
        //         throw new BadRequestException("شرکت ");
        //
        //
        //     model.UpdatedAt = DateTime.Now;
        //
        //     model.CompanyId = insurerViewModel.CompanyId;
        //     model.CompanyId = insurerViewModel.CompanyId;
        //     insurerViewModel.UpdatedBy = insurerViewModel.UpdatedBy;
        //
        //     await _insurerRepository.UpdateAsync(model, cancellationToken);
        //
        //     return _mapper.Map<InsurerViewModel>(model);
        // }
        public async Task<InsurerTermDetailedResultViewModel> UpdateInsurerTermAsync(Guid code, long insuranceId, long id, InsurerTermInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
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

            InsurerTerm insurerTerm = await _insurerTermRepository.GetByIdAsync(cancellationToken, id);

            insurerTerm.InsuranceTermTypeId = viewModel.InsuranceTermTypeId;
            insurerTerm.Value = viewModel.Value;
            insurerTerm.Discount = viewModel.Discount;
            insurerTerm.IsCumulative = viewModel.IsCumulative;
            insurerTerm.ConditionTypeId = viewModel.ConditionTypeId;


            await _insurerTermRepository.UpdateAsync(insurerTerm, cancellationToken);

            return _mapper.Map<InsurerTermDetailedResultViewModel>(insurerTerm);
        }





        public async Task<InsurerTermDetailedResultViewModel> UpdateInsurerTermAsyncMine(long userId, long insuranceId, long id, InsurerTermInputViewModel viewModel, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);
            if (personCompany == null)
            {
                throw new BadRequestException("شما ارتباطی با هیج شرکتی ندارید");
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

            Insurer insurer = await _insurerRepository.GetWithInsuranceIdAndCompanyCodeNoTracking(company.Code, insurance.Id, cancellationToken);
            if (insurer == null)
            {
                throw new BadRequestException("بیمه گر وجود ندارد");
            }

            InsurerTerm insurerTerm = await _insurerTermRepository.GetByIdAsync(cancellationToken, id);

            insurerTerm.InsuranceTermTypeId = viewModel.InsuranceTermTypeId;
            insurerTerm.Value = viewModel.Value;
            insurerTerm.Discount = viewModel.Discount;
            insurerTerm.IsCumulative = viewModel.IsCumulative;
            insurerTerm.ConditionTypeId = viewModel.ConditionTypeId;

            await _insurerTermRepository.UpdateAsync(insurerTerm, cancellationToken);

            return _mapper.Map<InsurerTermDetailedResultViewModel>(insurerTerm);
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteInsurerTerm(long id, CancellationToken cancellationToken)
        {
            InsurerTerm insurerTerm = await _insurerTermRepository.GetByIdAsync(cancellationToken, id);
            if (insurerTerm == null)
                throw new BadRequestException("مقررات وجود ندارد");

            await _insurerTermRepository.DeleteAsync(insurerTerm, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteInsurerAsync(long id, CancellationToken cancellationToken)
        {
            Insurer insurer = await _insurerRepository.GetByIdAsync(cancellationToken, id);
            if (insurer == null)
                throw new BadRequestException("بیمه گر وجود ندارد");

            await _insurerRepository.DeleteAsync(insurer, cancellationToken);

            return true;
        }
        public async Task<bool> DeleteInsurerTermAsync(long id, CancellationToken cancellationToken)
        {
            InsurerTerm insurerTerm = await _insurerTermRepository.GetByIdAsync(cancellationToken, id);
            if (insurerTerm == null)
                throw new BadRequestException("قوانین بیمه گر وجود ندارد");

            await _insurerTermRepository.DeleteAsync(insurerTerm, cancellationToken);

            return true;
        }






        public async Task<bool> DeleteInsurerTermMine(long userId, long insuranceId, long id, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);
            if (personCompany == null)
            {
                throw new BadRequestException("شما ارتباطی با هیج شرکتی ندارید");
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

            Insurer insurer = await _insurerRepository.GetWithInsuranceIdAndCompanyCodeNoTracking(company.Code, insurance.Id, cancellationToken);
            if (insurer == null)
            {
                throw new BadRequestException("بیمه گر وجود ندارد");
            }

            InsurerTerm insurerTerm = await _insurerTermRepository.GetByIdAsync(cancellationToken, id);
            if (insurerTerm == null)
                throw new BadRequestException("مقررات وجود ندارد");

            await _insurerTermRepository.DeleteAsync(insurerTerm, cancellationToken);
            return true;
        }
        #endregion

        #region Get
        public async Task<InsurerTermDetailedResultViewModel> GetInsurerTerm(long id, CancellationToken cancellationToken)
        {
            InsurerTerm model = await _insurerTermRepository.GetWithDetailsById(id,cancellationToken);
            if (model == null)
                throw new BadRequestException("مقررات بیمه گر وجود ندارد");
            return _mapper.Map<InsurerTermDetailedResultViewModel>(model);
        }


        public async Task<PagedResult<InsurerTermResultViewModel>> GetAllInsurerTerms(Guid code, long insuranceId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه وجود ندارد");
            }

            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<InsurerTerm> insurerTerms = await _insurerTermRepository.GetAllInsurerTerms(insuranceId, pageAbleModel, cancellationToken);
            return _mapper.Map<PagedResult<InsurerTermResultViewModel>>(insurerTerms);
        }


        public async Task<InsurerViewModel> GetInsurerAsync(long id, CancellationToken cancellationToken)
        {
            Insurer model = await _insurerRepository.GetByIdAsync(cancellationToken, id);

            if (model == null)
                throw new BadRequestException("بیمه گر وجود ندارد");

            return _mapper.Map<InsurerViewModel>(model);
        }


        public async Task<InsurerTermViewModel> GetInsurerTermAsync(long id, CancellationToken cancellationToken)
        {
            InsurerTerm model = await _insurerTermRepository.GetByIdAsync(cancellationToken, id);

            if (model == null)
                throw new BadRequestException("قوانین بیمه گر وجود ندارد");

            return _mapper.Map<InsurerTermViewModel>(model);
        }

        public async Task<InsurerTermDetailedResultViewModel> GetInsurerTermMine(long userId, long insuranceId, long id, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);
            if (personCompany == null)
            {
                throw new BadRequestException("شما با هیچ شرکتی ارتباطی ندارید");
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

            Insurer insurer = await _insurerRepository.GetWithInsuranceIdAndCompanyCodeNoTracking(company.Code, insurance.Id, cancellationToken);
            if (insurer == null)
            {
                throw new BadRequestException("بیمه گر وجود ندارد");
            }

            InsurerTerm model = await _insurerTermRepository.GetWithDetailsById(id, cancellationToken);
            if (model == null)
                throw new BadRequestException("مقررات بیمه گر وجود ندارد");
            return _mapper.Map<InsurerTermDetailedResultViewModel>(model);
        }

        public async Task<PagedResult<InsurerTermResultViewModel>> GetAllInsurerTermsMine(long userId, long insuranceId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetByIdAsync(cancellationToken, userId);

            PersonCompany personCompany = await _personCompanyRepository.GetByPersonId(user.PersonId, cancellationToken);
            if (personCompany == null)
            {
                throw new BadRequestException("شما با هیچ شرکتی ارتباطی ندارید");
            }

            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه وجود ندارد");
            }

            Company company = await _companyRepository.GetByIdNoTracking(personCompany.CompanyId.GetValueOrDefault(), cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<InsurerTerm> insurerTerms = await _insurerTermRepository.GetAllInsurerTerms(insuranceId, pageAbleModel, cancellationToken);
            return _mapper.Map<PagedResult<InsurerTermResultViewModel>>(insurerTerms);
        }

        
        #endregion
    }
}
