using Common.Utilities;
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
    public interface IEnumService
    {
        Task<EnumerationResultViewModel> CreateEnumeration(EnumerationInputViewModel enumerationInput, CancellationToken cancellationToken);
        Task<EnumerationResultViewModel> GetEnumeration(long id, CancellationToken cancellationToken);
        Task<PagedResult<EnumerationResultViewModel>> GetAllEnumeration(PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<EnumerationResultViewModel> UpdateEnumeration(long id, EnumerationInputViewModel enumerationInput, CancellationToken cancellationToken);
        Task<string> DeleteEnumeration(long id, CancellationToken cancellationToken);




        Task<List<EnumViewModel>> GetDamageEnumsAsync(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetFinancialEnumsAsync(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetDriverEnumsAsync(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetDriverDiscountOnInsuranceEnumsAsync(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetThirdDiscountOnInsuranceEnumsAsync(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetNoBodyDamageDiscountEnumsAsync(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetMarketFluctuationEnumsAsync(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetInsurerTermsTypesEnumAsync(CancellationToken cancellationToken);
        Task<List<ThirdInsuranceCreditMonthViewModel>> GetThirdInsuranceCreditMonthAsync( CancellationToken cancellationToken);
        Task<List<BodyNoDamageDiscountYearOutPutViewModel>> GetBodyNoDamageDiscountYearAsync( CancellationToken cancellationToken);
        Task<List<ThirdMaxFinancialCoverViewModel>> GetThirdMaxFinancialCoverAsync( CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetInspectionTypes(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetAgentSelectionType(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetRelatedResourceType(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetPricingType(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetCalculationType(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetConditionType(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetIsWithoutInsurance(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetIsChangedOwner(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetThirdLifeDamage(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetThirdFinancialDamage(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetDriverLifeDamage(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetIsZeroKilometer(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetIsPrevDamaged(CancellationToken cancellationToken);
        Task<List<EnumViewModel>> GetIsCash(CancellationToken cancellationToken);
    }
}
