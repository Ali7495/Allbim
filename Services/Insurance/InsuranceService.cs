using AutoMapper;
using Common.Exceptions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Models;
using Microsoft.Extensions.Options;
using Models.CentralRuleType;
using Models.Insurance;
using Models.InsuranceTermType;
using Models.PageAble;
using Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Insurancee = DAL.Models.Insurance;

namespace Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IInsurerRepository _insurerRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IRepository<InsurerTerm> _insurerTermRepository;
        private readonly IInsuranceTermTypeRepository _insuranceTermTypeRepository;
        private readonly ICentralRuleTypeRepository _centralRuleTypeRepository;
        private readonly PagingSettings _pagingSettings;
        private readonly IMapper _mapper;
        public InsuranceService(IInsuranceRepository insuranceRepository, IOptionsSnapshot<PagingSettings> pagingSettings, IInsurerRepository insurerRepository, ICompanyRepository companyRepository, IRepository<InsurerTerm> insurerTermRepository, IInsuranceTermTypeRepository insuranceTermTypeRepository, ICentralRuleTypeRepository centralRuleTypeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _insuranceRepository = insuranceRepository;
            _pagingSettings = pagingSettings.Value;
            _insurerRepository = insurerRepository;
            _companyRepository = companyRepository;
            _insurerTermRepository = insurerTermRepository;
            _insuranceTermTypeRepository = insuranceTermTypeRepository;
            _centralRuleTypeRepository = centralRuleTypeRepository;
        }


        #region Create
        public async Task<InsuranceViewModel> CreateInsurance(InsuranceInputViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            var model = new Insurancee
            {
                Name = insuranceViewModel.Name,
                Description = insuranceViewModel.Description,

            };
            await _insuranceRepository.AddAsync(model, cancellationToken);
            
            return _mapper.Map<InsuranceViewModel>(model);
        }
        // public async Task<InsurerViewModel> CreateInsurerAsync(InsurerViewModel insurerViewModel, CancellationToken cancellationToken)
        // {
        //     var companyIdIsValid = await _companyRepository.GetByIdAsync(cancellationToken, insurerViewModel.CompanyId) != null;
        //     if (!companyIdIsValid)
        //         throw new CustomException("شرکت ");
        //
        //     var insuranceIdIsValid = await _insuranceRepository.GetByIdAsync(cancellationToken, insurerViewModel.InsuranceId) != null;
        //     if (!insuranceIdIsValid)
        //         throw new CustomException("بیمه");
        //
        //     Insurer model = new Insurer
        //     {
        //         CompanyId = insurerViewModel.CompanyId,
        //         InsuranceId = insurerViewModel.InsuranceId,
        //         CreatedBy = insurerViewModel.CreatedBy
        //     };
        //
        //     await _insurerRepository.AddAsync(model, cancellationToken);
        //
        //     return _mapper.Map<InsurerViewModel>(model); 
        // }
        public async Task<InsurerTermViewModel> CreateInsurerTermAsync(InsurerTermViewModel insurerTermViewModel, CancellationToken cancellationToken)
        {
            var insurerIdIsValid = await _insurerRepository.GetByIdAsync(cancellationToken, insurerTermViewModel.InsurerId) != null;
            if (!insurerIdIsValid)
                throw new CustomException("بیمه گر ");

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

        #endregion

        #region Update
        public async Task<InsuranceViewModel> UpdateInsurance(long id, InsuranceInputViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            var model = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("بیمه");

            model.Name = insuranceViewModel.Name;
            model.Description = insuranceViewModel.Description;



            await _insuranceRepository.UpdateAsync(model, cancellationToken);
            return _mapper.Map<InsuranceViewModel>(model);
        }
        // public async Task<InsurerViewModel> UpdateInsurerAsync(long id, InsurerViewModel insurerViewModel, CancellationToken cancellationToken)
        // {
        //     var model = await _insurerRepository.GetByIdAsync(cancellationToken, id);
        //     if (model == null)
        //         throw new CustomException("بیمه گر ");
        //
        //     var insuranceIdIsValid = _insuranceRepository.GetByIdAsync(cancellationToken, insurerViewModel.InsuranceId) != null;
        //     if (!insuranceIdIsValid)
        //         throw new CustomException("بیمه");
        //
        //     var companyIdIsValid = _companyRepository.GetByIdAsync(cancellationToken, insurerViewModel.CompanyId) != null;
        //     if (!companyIdIsValid)
        //         throw new CustomException("شرکت ");
        //
        //
        //     model.UpdatedAt = DateTime.Now;
        //     model.CompanyId = insurerViewModel.CompanyId;
        //     model.InsuranceId = insurerViewModel.InsuranceId;
        //     insurerViewModel.UpdatedBy = insurerViewModel.UpdatedBy;
        //
        //     await _insurerRepository.UpdateAsync(model, cancellationToken);
        //
        //     return _mapper.Map<InsurerViewModel>(model);
        // }
        public async Task<InsurerTermViewModel> UpdateInsurerTermAsync(long id, InsurerTermViewModel insurerTermViewModel, CancellationToken cancellationToken)
        {
            var model = await _insurerTermRepository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                throw new CustomException("قوانین بیمه گر ");

            var insurerIdIsValid = await _insurerRepository.GetByIdAsync(cancellationToken, insurerTermViewModel.InsurerId) != null;
            if (!insurerIdIsValid)
                throw new CustomException("بیمه گر ");

            //model.Type = insurerTermViewModel.Type;
            //model.Field = insurerTermViewModel.Field;
            //model.Criteria = insurerTermViewModel.Criteria;
            model.Value = insurerTermViewModel.Value;
            model.Discount = insurerTermViewModel.Discount;
            model.CalculationTypeId = insurerTermViewModel.CalculationTypeId;
            model.InsurerId = insurerTermViewModel.InsurerId;
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = insurerTermViewModel.UpdatedBy;

            await _insurerTermRepository.UpdateAsync(model, cancellationToken);

            return _mapper.Map<InsurerTermViewModel>(model);
        }
        #endregion

        #region Delete
        public async Task<bool> DeleteInsurance(long id, CancellationToken cancellationToken)
        {
            var insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, id);
            if (insurance == null)
                throw new BadRequestException(" این بیمه وجود ندارد");

            var insurer = await _insuranceRepository.GetInsurerWithRelation(id, cancellationToken);
            //int rulesCount = insurer.InsuranceCentralRules.Count;
            int rulesCount = 0;
            if (rulesCount != 0)
            {
                throw new BadRequestException(" این بیمه شامل قوانین می باشد و قابل حذف نیست");
            }
            
            int fieldsCount = insurer.InsuranceFields.Count;
            if (fieldsCount != 0)
            {
                throw new BadRequestException(" این بیمه شامل فیلد می باشد و قابل حذف نیست");
            }

            int stepsCount = insurer.InsuranceSteps.Count;
            if (stepsCount != 0)
            {
                throw new BadRequestException(" این بیمه شامل مرحله می باشد و قابل حذف نیست");
            }
            
            int insurersCount = insurer.Insurers.Count;
            if (insurersCount != 0)
            {
                throw new BadRequestException(" این بیمه شامل بیمه گر می باشد و قابل حذف نیست");
            }

            int remindersCount = insurer.Reminders.Count;
            if (remindersCount != 0)
            {
                throw new BadRequestException(" این بیمه شامل یادآور می باشد و قابل حذف نیست");
            }

            await _insuranceRepository.DeleteAsync(insurance, cancellationToken);
            return true;
        }
        //public async Task<bool> DeleteInsurerAsync(long id, CancellationToken cancellationToken)
        //{
        //    var insurer = await _insurerRepository.GetByIdAsync(cancellationToken, id);
        //    if (insurer == null)
        //        throw new CustomException("بیمه گر ");

        //    await _insurerRepository.DeleteAsync(insurer, cancellationToken);

        //    return true;
        //}
        //public async Task<bool> DeleteInsurerTermAsync(long id, CancellationToken cancellationToken)
        //{
        //    var insurerTerm = await _insurerTermRepository.GetByIdAsync(cancellationToken, id);
        //    if (insurerTerm == null)
        //        throw new CustomException("قوانین بیمه گر ");

        //    await _insurerTermRepository.DeleteAsync(insurerTerm, cancellationToken);

        //    return true;
        //}
        #endregion

        #region Get
        public async  Task<List<InsurerViewModel>> GetInsuranceInsurer(string slug,CancellationToken cancellationToken)
        {
            List<Insurer> model = await _insurerRepository.GetByInsuranceSlug(slug,cancellationToken);
            if (model == null)
                throw new CustomException(" شرکت");

            return _mapper.Map<List<InsurerViewModel>>(model.ToList());
        }
        
        public async Task<InsuranceViewModel> GetInsurance(long id, CancellationToken cancellationToken)
        {
            var model = await _insuranceRepository.GetInsuranceById(id, cancellationToken);
            if (model == null)
                throw new CustomException(" شرکت");
            return _mapper.Map<InsuranceViewModel>(model);
        }
        
        // public async Task<PagedResult<InsuranceViewModel>> GetAllInsurances(int? page, int? pageSize, CancellationToken cancellationToken)
        // {
        //     int pageNotNull = page ?? _pagingSettings.DefaultPage;
        //     int pageSizeNotNull = pageSize ?? _pagingSettings.PageSize;
        //     var model = await _insuranceRepository.GetPagedAsync(pageNotNull, pageSizeNotNull, cancellationToken);
        //     return _mapper.Map<PagedResult<InsuranceViewModel>>(model); 
        // }

        public async Task<List<InsuranceViewModel>> GetAllInsurances( CancellationToken cancellationToken)
        {
            List<Insurance> model = await _insuranceRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<InsuranceViewModel>>(model); 
        }
        public async Task<List<InsuranceViewModel>> GetAllWithoutPaginate( CancellationToken cancellationToken)
        {
            List<Insurance> model = await _insuranceRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<InsuranceViewModel>>(model); 
        }


        public async Task<InsurerViewModel> GetInsurerAsync(long id, CancellationToken cancellationToken)
        {
            Insurer model = await _insurerRepository.GetInsurerById(id, cancellationToken);

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


        public async Task<InsurerTermViewModel> GetInsurerTermAsync(long id, CancellationToken cancellationToken)
        {
            var model = await _insurerTermRepository.GetByIdAsync(cancellationToken, id);

            if (model == null)
                throw new CustomException("قوانین بیمه گر  ");

            return _mapper.Map<InsurerTermViewModel>(model); 
        }
        public async Task<PagedResult<InsurerTermViewModel>> GetAllInsurerTermsAsync(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<InsurerTerm> model;

            if (string.IsNullOrEmpty(pageAbleResult.OrderBy))
                model = await _insurerTermRepository.GetPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, cancellationToken);
            else
                model = await _insurerTermRepository.GetOrderedPagedAsync(pageAbleResult.Page, pageAbleResult.PageSize, pageAbleResult.OrderBy, cancellationToken);

            return _mapper.Map<PagedResult<InsurerTermViewModel>>(model); ;
        }

        Task<InsurerTerm> IInsuranceService.GetInsurerTermAsync(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InsuranceDetailsViewModel>> GetInsuranceDetails(string slug, CancellationToken cancellationToken)
        {
            List<Insurance> insurances = await _insuranceRepository.GetInsuranceDetails(slug, cancellationToken);
            if (insurances == null)
            {
                throw new BadRequestException("بیمه ای یافت نشد");
            }

            return _mapper.Map<List<InsuranceDetailsViewModel>>(insurances);
        }

        public async Task<List<InsuranceTermTypeViewModel>> GetAllInsuranceTermTypes(long insuranceId, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه ای یافت نشد");
            }

            List<InsuranceTermType> insuranceTerms = await _insuranceTermTypeRepository.GetInsuranceTermTypesByInsuranceId(insuranceId, cancellationToken);

            return _mapper.Map<List<InsuranceTermTypeViewModel>>(insuranceTerms);
        }

        public async Task<List<CentralRuleTypeViewModel>> GetAllCentralRuleTypes(long insuranceId, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه ای یافت نشد");
            }

            List<CentralRuleType> centralRuleTypes = await _centralRuleTypeRepository.GetCentralRuleTypesByInsuranceId(insuranceId, cancellationToken);

            return _mapper.Map<List<CentralRuleTypeViewModel>>(centralRuleTypes);
        }
        #endregion
    }
}
