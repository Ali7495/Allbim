using albim.Controllers;
using albim.Result;
using AutoMapper.Configuration;
using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Models.PageAble;
using Services.Damage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Albim.Controllers.v1
{
    public class EnumController : BaseController
    {

        private readonly IEnumService _enumService;
        public EnumController(IEnumService enumService)
        {
            _enumService = enumService;
        }

        #region CRUD

        [HttpPost("")]
        public async Task<ApiResult<EnumerationResultViewModel>> CreateEnumeration([FromBody] EnumerationInputViewModel enumerationInput, CancellationToken cancellationToken)
        {
           EnumerationResultViewModel result = await _enumService.CreateEnumeration(enumerationInput, cancellationToken);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<EnumerationResultViewModel>> GetEnumeration(long id, CancellationToken cancellationToken)
        {
            EnumerationResultViewModel result = await _enumService.GetEnumeration(id, cancellationToken);

            return result;
        }

        [HttpGet("")]
        public async Task<ApiResult<PagedResult<EnumerationResultViewModel>>> GetAllEnumeration(PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<EnumerationResultViewModel> result = await _enumService.GetAllEnumeration(pageAbleResult, cancellationToken);

            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResult<EnumerationResultViewModel>> UpdateEnumeration(long id, [FromBody] EnumerationInputViewModel enumerationInput, CancellationToken cancellationToken)
        {
            EnumerationResultViewModel result = await _enumService.UpdateEnumeration(id, enumerationInput, cancellationToken);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult<string>> DeleteEnumeration(long id, CancellationToken cancellationToken)
        {
            string result = await _enumService.DeleteEnumeration(id, cancellationToken);

            return result;
        }

        #endregion

        #region Get Single Enums

        [AllowAnonymous]
        [HttpGet("DamageToLife")]
        public async Task<ApiResult<List<EnumViewModel>>> GetDamageToLifeEnums(CancellationToken cancellationToken)
        {
            return await _enumService.GetDamageEnumsAsync(cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("FinancialDamage")]
        public async Task<ApiResult<List<EnumViewModel>>> GetFinancialDamageEnums(CancellationToken cancellationToken)
        {
            return await _enumService.GetFinancialEnumsAsync(cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("DriverDamage")]
        public async Task<ApiResult<List<EnumViewModel>>> GetDriverDamageEnums(CancellationToken cancellationToken)
        {
            return await _enumService.GetDriverEnumsAsync(cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("DriverDiscountOnInsurance")]
        public async Task<ApiResult<List<EnumViewModel>>> GetDriverDiscountOnInsuranceEnums(CancellationToken cancellationToken)
        {
            return await _enumService.GetDriverDiscountOnInsuranceEnumsAsync(cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("ThirdDiscountOnInsurance")]
        public async Task<ApiResult<List<EnumViewModel>>> GetThirdDiscountOnInsuranceEnums(CancellationToken cancellationToken)
        {
            return await _enumService.GetThirdDiscountOnInsuranceEnumsAsync(cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("NoBodyDamageDiscount")]
        public async Task<ApiResult<List<EnumViewModel>>> GetNoBodyDamageDiscountEnums(CancellationToken cancellationToken)
        {
            return await _enumService.GetNoBodyDamageDiscountEnumsAsync(cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("PriceFluctuation")]
        public async Task<ApiResult<List<EnumViewModel>>> GetPriceFluctuationEnums(CancellationToken cancellationToken)
        {
            return await _enumService.GetMarketFluctuationEnumsAsync(cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("InsurerTermsTypes")]
        public async Task<ApiResult<List<EnumViewModel>>> GetInsurerTermsTypesEnum(CancellationToken cancellationToken)
        {
            return await _enumService.GetInsurerTermsTypesEnumAsync(cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("BodyNoDamageDiscountYear")]
        public async Task<ApiResult<List<BodyNoDamageDiscountYearOutPutViewModel>>> GetBodyNoDamageDiscountYear(CancellationToken cancellationToken)
        {
            return await _enumService.GetBodyNoDamageDiscountYearAsync(cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("ThirdInsuranceCreditMonth")]
        public async Task<ApiResult<List<ThirdInsuranceCreditMonthViewModel>>> GetThirdInsuranceCreditMonth(CancellationToken cancellationToken)
        {
            return await _enumService.GetThirdInsuranceCreditMonthAsync(cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("ThirdMaxFinancialCover")]
        public async Task<ApiResult<List<ThirdMaxFinancialCoverViewModel>>> GetThirdMaxFinancialCover(CancellationToken cancellationToken)
        {
            return await _enumService.GetThirdMaxFinancialCoverAsync(cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("InspectionType")]
        public async Task<ApiResult<List<EnumViewModel>>> GetInspectionType(CancellationToken cancellationToken)
        {
            return await _enumService.GetInspectionTypes(cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("AgentSelectionType")]
        public async Task<ApiResult<List<EnumViewModel>>> GetAgentSelectionType(CancellationToken cancellationToken)
        {
            return await _enumService.GetAgentSelectionType(cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("relatedRessourceType")]
        public async Task<ApiResult<List<EnumViewModel>>> GetRelatedResourceType(CancellationToken cancellationToken)
        {
            return await _enumService.GetRelatedResourceType(cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("pricingType")]
        public async Task<ApiResult<List<EnumViewModel>>> GetPricingType(CancellationToken cancellationToken)
        {
            return await _enumService.GetPricingType(cancellationToken);
        }


        [AllowAnonymous]
        [HttpGet("calculationType")]
        public async Task<ApiResult<List<EnumViewModel>>> GetCalculationType(CancellationToken cancellationToken)
        {
            return await _enumService.GetCalculationType(cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("conditionType")]
        public async Task<ApiResult<List<EnumViewModel>>> GetConditionType(CancellationToken cancellationToken)
        {
            return await _enumService.GetConditionType(cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("isWithoutInsurance")]
        public async Task<ApiResult<List<EnumViewModel>>> GetIsWithoutInsurance(CancellationToken cancellationToken)
        {
            return await _enumService.GetIsWithoutInsurance(cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("isChangedOwner")]
        public async Task<ApiResult<List<EnumViewModel>>> GetIsChangedOwner(CancellationToken cancellationToken)
        {
            return await _enumService.GetIsChangedOwner(cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("thirdLifeDamage")]
        public async Task<ApiResult<List<EnumViewModel>>> GetThirdLifeDamage(CancellationToken cancellationToken)
        {
            return await _enumService.GetThirdLifeDamage(cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("thirdFinancialDamage")]
        public async Task<ApiResult<List<EnumViewModel>>> GetThirdFinancialDamage(CancellationToken cancellationToken)
        {
            return await _enumService.GetThirdFinancialDamage(cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("driverLifeDamage")]
        public async Task<ApiResult<List<EnumViewModel>>> GetDriverLifeDamage(CancellationToken cancellationToken)
        {
            return await _enumService.GetDriverLifeDamage(cancellationToken);
        }


        [AllowAnonymous]
        [HttpGet("isZeroKilometer")]
        public async Task<ApiResult<List<EnumViewModel>>> GetIsZeroKilometer(CancellationToken cancellationToken)
        {
            return await _enumService.GetIsZeroKilometer(cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("isPrevDamaged")]
        public async Task<ApiResult<List<EnumViewModel>>> GetIsPrevDamaged(CancellationToken cancellationToken)
        {
            return await _enumService.GetIsPrevDamaged(cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("isCash")]
        public async Task<ApiResult<List<EnumViewModel>>> GetIsCash(CancellationToken cancellationToken)
        {
            return await _enumService.GetIsCash(cancellationToken);
        }

        #endregion

    }
}
