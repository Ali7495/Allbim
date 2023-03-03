using AutoMapper;
using Common.Exceptions;
using Common.Extensions;
using Common.Utilities;
using DAL.Contracts;
using DAL.Contracts.EnumIRepositories;
using DAL.Contracts.EnumRepositories;
using DAL.Models;
using Models.Enums;
using Models.PageAble;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Damage
{
    public class EnumService : IEnumService
    {
        private readonly IMapper _mapper;
        private readonly IDamageRepository _damageRepository;
        private readonly IFinancialRepository _financialRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IDriverDiscountRepository _driverDiscountRepository;
        private readonly IThirdDiscountRepository _thirdDiscountRepository;
        private readonly INoBodyDamageDiscountRepository _noBodyDamageDiscountRepository;
        private readonly IMarketFluctuationRepository _marketFluctuationRepository;
        private readonly IInsurerTermEnumRepository _insurerTermEnumRepository;
        private readonly IThirdMaxFinancialCoverRepository _thirdMaxFinancialCoverRepository;
        private readonly IThirdInsuranceCreditMonthRepository _thirdInsuranceCreditMonthRepository;
        private readonly IBodyNoDamageDiscountYearRepository _bodyNoDamageDiscountYearRepository;
        private readonly IInspectionTypeRepository _inspectionTypeRepository;
        private readonly IAgentSelectRepository _agentSelectRepository;
        private readonly IRelatedResourceRepository _relatedResourceRepository;
        private readonly IPricingRepository _pricingRepository;
        private readonly ICalculationRepository _calculationRepository;
        private readonly IConditionRepository _conditionRepository;
        private readonly IWithoutInsuranceRepository _withoutInsuranceRepository;
        private readonly IIsChagedOwnerRepository _isChagedOwnerRepository;
        private readonly IThirdLifeDamageRepository _thirdLifeDamageRepository;
        private readonly IThirdFinancialDamageRepository _thirdFinancialDamageRepository;
        private readonly IDriverLifeDamageRepository _driverLifeDamageRepository;
        private readonly IIsZeroKilometerRepository _isZeroKilometerRepository;
        private readonly IIsPrevDamagedRepository _isPrevDamagedRepository;
        private readonly IIsCashRepository _isCashRepository;
        private readonly IEnumerationRepository _enumerationRepository;

        public EnumService(IAgentSelectRepository agentSelectRepository, IMapper mapper ,
            IDamageRepository damageRepository, IBodyNoDamageDiscountYearRepository bodyNoDamageDiscountYearRepository, 
            IThirdInsuranceCreditMonthRepository thirdInsuranceCreditMonthRepository, IFinancialRepository financialRepository, 
            IDriverRepository driverRepository, IDriverDiscountRepository driverDiscountRepository,
            IThirdDiscountRepository thirdDiscountRepository, INoBodyDamageDiscountRepository noBodyDamageDiscountRepository,
            IInsurerTermEnumRepository insurerTermEnumRepository, IThirdMaxFinancialCoverRepository thirdMaxFinancialCoverRepository, 
            IInspectionTypeRepository inspectionTypeRepository,IMarketFluctuationRepository marketFluctuationRepository, IRelatedResourceRepository relatedResourceRepository, IPricingRepository pricingRepository, ICalculationRepository calculationRepository, IConditionRepository conditionRepository, IWithoutInsuranceRepository withoutInsuranceRepository, IIsChagedOwnerRepository isChagedOwnerRepository, IThirdLifeDamageRepository thirdLifeDamageRepository, IThirdFinancialDamageRepository thirdFinancialDamageRepository, IDriverLifeDamageRepository driverLifeDamageRepository, IIsZeroKilometerRepository isZeroKilometerRepository, IIsPrevDamagedRepository isPrevDamagedRepository, IIsCashRepository isCashRepository, IEnumerationRepository enumerationRepository)
        {
            _mapper = mapper;
            _damageRepository = damageRepository;
            _bodyNoDamageDiscountYearRepository = bodyNoDamageDiscountYearRepository;
            _thirdInsuranceCreditMonthRepository = thirdInsuranceCreditMonthRepository;
            _financialRepository = financialRepository;
            _driverRepository = driverRepository;
            _driverDiscountRepository = driverDiscountRepository;
            _thirdDiscountRepository = thirdDiscountRepository;
            _noBodyDamageDiscountRepository = noBodyDamageDiscountRepository;
            _insurerTermEnumRepository = insurerTermEnumRepository;
            _thirdMaxFinancialCoverRepository = thirdMaxFinancialCoverRepository;
            _inspectionTypeRepository = inspectionTypeRepository;
            _agentSelectRepository = agentSelectRepository;
            _marketFluctuationRepository = marketFluctuationRepository;
            _relatedResourceRepository = relatedResourceRepository;
            _pricingRepository = pricingRepository;
            _calculationRepository = calculationRepository;
            _conditionRepository = conditionRepository;
            _withoutInsuranceRepository = withoutInsuranceRepository;
            _isChagedOwnerRepository = isChagedOwnerRepository;
            _thirdLifeDamageRepository = thirdLifeDamageRepository;
            _thirdFinancialDamageRepository = thirdFinancialDamageRepository;
            _driverLifeDamageRepository = driverLifeDamageRepository;
            _isZeroKilometerRepository = isZeroKilometerRepository;
            _isPrevDamagedRepository = isPrevDamagedRepository;
            _isCashRepository = isCashRepository;
            _enumerationRepository = enumerationRepository;
        }


        #region CRUD

        public async Task<EnumerationResultViewModel> CreateEnumeration(EnumerationInputViewModel enumerationInput, CancellationToken cancellationToken)
        {
            Enumeration enumeration = _mapper.Map<Enumeration>(enumerationInput);

            await _enumerationRepository.AddAsync(enumeration, cancellationToken);

            return _mapper.Map<EnumerationResultViewModel>(enumeration);
        }

        public async Task<EnumerationResultViewModel> GetEnumeration(long id, CancellationToken cancellationToken)
        {
            Enumeration enumeration = await _enumerationRepository.GetByIdAsync(cancellationToken, id);
            if (enumeration == null)
            {
                throw new BadRequestException("کلید مورد نظر وجود ندارد");
            }

            return _mapper.Map<EnumerationResultViewModel>(enumeration);

        }

        public async Task<PagedResult<EnumerationResultViewModel>> GetAllEnumeration(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<Enumeration> enums = await _enumerationRepository.GetAllEnumerationPaging(pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<EnumerationResultViewModel>>(enums);
        }

        public async Task<EnumerationResultViewModel> UpdateEnumeration(long id, EnumerationInputViewModel enumerationInput, CancellationToken cancellationToken)
        {
            Enumeration enumeration = await _enumerationRepository.GetByIdAsync(cancellationToken, id);
            if (enumeration == null)
            {
                throw new BadRequestException("کلید مورد نظر وجود ندارد");
            }

            enumeration.CategoryCaption = enumerationInput.CategoryCaption;
            enumeration.CategoryName = enumerationInput.CategoryName;
            enumeration.Description = enumerationInput.Description;
            enumeration.EnumCaption = enumeration.EnumCaption;
            enumeration.EnumId = enumerationInput.EnumId;
            enumeration.IsEnable = enumerationInput.IsEnable;
            enumeration.Order = enumerationInput.Order;
            enumeration.ParentId = enumerationInput.ParentId;
            enumeration.UpdatedAt = DateTime.Now;

            await _enumerationRepository.UpdateAsync(enumeration, cancellationToken);

            return _mapper.Map<EnumerationResultViewModel>(enumeration);
        }

        public async Task<string> DeleteEnumeration(long id, CancellationToken cancellationToken)
        {
            Enumeration enumeration = await _enumerationRepository.GetByIdAsync(cancellationToken, id);
            if (enumeration == null)
            {
                throw new BadRequestException("کلید مورد نظر وجود ندارد");
            }

            await _enumerationRepository.DeleteAsync(enumeration, cancellationToken);

            return "true";
        }
        #endregion





        #region Get Single Enums

        public async Task<List<EnumViewModel>> GetDamageEnumsAsync(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _damageRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetDriverDiscountOnInsuranceEnumsAsync(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _driverDiscountRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetDriverEnumsAsync(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _driverRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetFinancialEnumsAsync(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _financialRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetThirdDiscountOnInsuranceEnumsAsync(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _thirdDiscountRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetNoBodyDamageDiscountEnumsAsync(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _noBodyDamageDiscountRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetMarketFluctuationEnumsAsync(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _marketFluctuationRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetInsurerTermsTypesEnumAsync(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _insurerTermEnumRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<BodyNoDamageDiscountYearOutPutViewModel>> GetBodyNoDamageDiscountYearAsync(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _bodyNoDamageDiscountYearRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<BodyNoDamageDiscountYearOutPutViewModel>>(model);
        }

        public async Task<List<ThirdInsuranceCreditMonthViewModel>> GetThirdInsuranceCreditMonthAsync(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _thirdInsuranceCreditMonthRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ThirdInsuranceCreditMonthViewModel>>(model);
        }

        public async Task<List<ThirdMaxFinancialCoverViewModel>> GetThirdMaxFinancialCoverAsync(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _thirdMaxFinancialCoverRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ThirdMaxFinancialCoverViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetInspectionTypes(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _inspectionTypeRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetAgentSelectionType(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _agentSelectRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetRelatedResourceType(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _relatedResourceRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetPricingType(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _pricingRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetCalculationType(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _calculationRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetConditionType(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _conditionRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetIsWithoutInsurance(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _withoutInsuranceRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetIsChangedOwner(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _isChagedOwnerRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetThirdLifeDamage(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _thirdLifeDamageRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetThirdFinancialDamage(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _thirdFinancialDamageRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetDriverLifeDamage(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _driverLifeDamageRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetIsZeroKilometer(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _isZeroKilometerRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetIsPrevDamaged(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _isPrevDamagedRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        public async Task<List<EnumViewModel>> GetIsCash(CancellationToken cancellationToken)
        {
            List<Enumeration> model = await _isCashRepository.Get(cancellationToken);
            return _mapper.Map<List<EnumViewModel>>(model);
        }

        


        #endregion
    }
}
