using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.Extensions.Options;
using Models.Insurance;
using Models.InsuranceCentralRule;
using Models.PageAble;
using Models.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class InsuranceCenteralRuleService : IInsuranceCenteralRuleService
    {
        private readonly IRepository<Insurer> _insurerRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICentralRulesRepository _insuranceCenteralRuleRepository;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly PagingSettings _pagingSettings;
        private readonly ICentralRuleTypeRepository _centralRuleTypeRepository;
        private readonly IMapper _mapper;

        public InsuranceCenteralRuleService(ICentralRulesRepository insuranceCenteralRuleRepository, IOptionsSnapshot<PagingSettings> pagingSettings, IRepository<Insurer> insurerRepository, ICompanyRepository companyRepository, IInsuranceRepository insuranceRepository, IMapper mapper, ICentralRuleTypeRepository centralRuleTypeRepository)
        {
            _insuranceCenteralRuleRepository = insuranceCenteralRuleRepository;
            _pagingSettings = pagingSettings.Value;
            _insurerRepository = insurerRepository;
            _companyRepository = companyRepository;
            _insuranceRepository = insuranceRepository;
            _centralRuleTypeRepository = centralRuleTypeRepository;
            _mapper = mapper;
        }


        #region Create

        public async Task<InsuranceCentralRuleResultViewModel> CreateInsuranceCenteralRule(long insuranceId, InsuranceCentralRuleInputViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه مورد نظر وجود ندارد");
            }

            CentralRuleType centralRuleType = await _centralRuleTypeRepository.GetByIdAsync(cancellationToken, insuranceViewModel.CentralRuleTypeId);
            if (centralRuleType == null)
            {
                throw new BadRequestException("نوع قانون مورد نظر وجود ندارد");
            }

            InsuranceCentralRule insuranceCentralRule = new InsuranceCentralRule()
            {
                CentralRuleTypeId = insuranceViewModel.CentralRuleTypeId,
                CalculationTypeId = insuranceViewModel.CalculationTypeId,
                ConditionTypeId = insuranceViewModel.ConditionTypeId,
                Discount = insuranceViewModel.Discount,
                //FieldId = "Field",
                FieldType = " ",
                IsCumulative = insuranceViewModel.IsCumulative,
                PricingTypeId = insuranceViewModel.PricingTypeId,
                //Type = 0,
                JalaliYear = insuranceViewModel.JalaliYear,
                GregorianYear = insuranceViewModel.GregorianYear,
                Value = insuranceViewModel.Value
            };

            await _insuranceCenteralRuleRepository.AddAsync(insuranceCentralRule, cancellationToken);

            return _mapper.Map<InsuranceCentralRuleResultViewModel>(insuranceCentralRule);
        }



        #endregion

        #region Update
        public async Task<InsuranceCentralRuleResultViewModel> CentralRule(long insuranceId, long RuleId, InsuranceCentralRuleInputViewModel insuranceCentralRule, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه مورد نظر وجود ندارد");
            }

            CentralRuleType centralRuleType = await _centralRuleTypeRepository.GetByIdAsync(cancellationToken, insuranceCentralRule.CentralRuleTypeId);
            if (centralRuleType == null)
            {
                throw new BadRequestException("نوع قانون مورد نظر وجود ندارد");
            }

            InsuranceCentralRule model = await _insuranceCenteralRuleRepository.GetByIdAsync(cancellationToken, RuleId);
            if (model == null)
                throw new BadRequestException("این قانون وجود ندارد");

            model.CentralRuleTypeId = insuranceCentralRule.CentralRuleTypeId;
            model.Value = insuranceCentralRule.Value;
            model.JalaliYear = insuranceCentralRule.JalaliYear;
            model.GregorianYear = insuranceCentralRule.GregorianYear;
            model.CalculationTypeId = insuranceCentralRule.CalculationTypeId;
            model.ConditionTypeId = insuranceCentralRule.ConditionTypeId;
            model.Discount = insuranceCentralRule.Discount;
            //model.FieldId = "Field";
            model.IsCumulative = insuranceCentralRule.IsCumulative;
            model.PricingTypeId = insuranceCentralRule.PricingTypeId;


            await _insuranceCenteralRuleRepository.UpdateAsync(model, cancellationToken);
            return _mapper.Map<InsuranceCentralRuleResultViewModel>(model);
        }
        
        
        #endregion

        #region Delete
        public async Task<bool> DeleteInsuranceCenteralRule(long insuranceId, long RuleId, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه مورد نظر وجود ندارد");
            }

            InsuranceCentralRule model = await _insuranceCenteralRuleRepository.GetByIdAsync(cancellationToken, RuleId);
            if (model == null)
                throw new BadRequestException("این قانون وجود ندارد");


            await _insuranceCenteralRuleRepository.DeleteAsync(model, cancellationToken);
            return true;
        }
        public async Task<bool> DeleteInsurerAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _insurerRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("بیمه گر ");

            await _insurerRepository.DeleteAsync(model, cancellationToken);

            return true;
        }
        public async Task<bool> DeleteInsurerTermAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _insuranceCenteralRuleRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("قوانین بیمه گر ");

            await _insuranceCenteralRuleRepository.DeleteAsync(model, cancellationToken);

            return true;
        }
        #endregion

        #region Get

        public async Task<InsuranceCentralRuleResultViewModel> GetInsuranceCenteralRule(long insuranceId, long roleId, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه مورد نظر وجود ندارد");
            }

            InsuranceCentralRule model = await _insuranceCenteralRuleRepository.GetRuleByInsuranceIdAndId(insuranceId,roleId,cancellationToken);

            return _mapper.Map<InsuranceCentralRuleResultViewModel>(model);
        }
        
        public async Task<PagedResult<InsuranceCentralRuleResultViewModel>> GetAllInsuranceCenteralRules(long insuranceId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه مورد نظر وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<InsuranceCentralRule> centralRules = await _insuranceCenteralRuleRepository.GetAllCentalRules(insuranceId, pageAbleModel, cancellationToken);


            return _mapper.Map<PagedResult<InsuranceCentralRuleResultViewModel>>(centralRules);
        }


        public async Task<InsurerViewModel> GetInsurerAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _insurerRepository.GetByIdAsync(cancellationToken, id);

            if (model == null)
                throw new CustomException("بیمه گر");

            return _mapper.Map<InsurerViewModel>(model); 
        }
        public async Task<PagedResult<InsurerViewModel>> GetAllInsurersAsync(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<Insurer> model;

            if (string.IsNullOrEmpty(pageAbleResult.OrderBy))
                model = await _insurerRepository.GetPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, cancellationToken);
            else
                model = await _insurerRepository.GetOrderedPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, pageAbleResult.OrderBy, cancellationToken);

            return _mapper.Map<PagedResult<InsurerViewModel>>(model);
        }


        public async Task<InsuranceCenteralRuleViewModel> GetInsurerTermAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _insuranceCenteralRuleRepository.GetByIdAsync(cancellationToken, id);

            if (model == null)
                throw new CustomException("قوانین بیمه گر  ");

            return _mapper.Map<InsuranceCenteralRuleViewModel>(model);
        }
        public async Task<PagedResult<InsuranceCenteralRuleViewModel>> GetAllInsurerTermsAsync(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<InsuranceCentralRule> model;

            if (string.IsNullOrEmpty(pageAbleResult.OrderBy))
                model = await _insuranceCenteralRuleRepository.GetPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, cancellationToken);
            else
                model = await _insuranceCenteralRuleRepository.GetOrderedPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, pageAbleResult.OrderBy, cancellationToken);

            return _mapper.Map<PagedResult<InsuranceCenteralRuleViewModel>>(model);
        }

       

       

      



        #endregion
    }
}
