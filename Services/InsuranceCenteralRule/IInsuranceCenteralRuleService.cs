using Common.Utilities;
using DAL.Models;
using Models.Insurance;
using Models.InsuranceCentralRule;
using Models.PageAble;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public interface IInsuranceCenteralRuleService
    {
        #region Create
        Task<InsuranceCentralRuleResultViewModel> CreateInsuranceCenteralRule(long insuranceId, InsuranceCentralRuleInputViewModel insuranceViewModel, CancellationToken cancellationToken);

        #endregion

        #region Update
        Task<InsuranceCentralRuleResultViewModel> CentralRule(long insuranceId, long RuleId, InsuranceCentralRuleInputViewModel insuranceCentralRule, CancellationToken cancellationToken);


        #endregion

        #region Delete
        Task<bool> DeleteInsuranceCenteralRule(long insuranceId, long RuleId, CancellationToken cancellationToken);

        #endregion

        #region Get
        Task<InsuranceCentralRuleResultViewModel> GetInsuranceCenteralRule(long insuranceId,long roleId, CancellationToken cancellationToken);
        Task<PagedResult<InsuranceCentralRuleResultViewModel>> GetAllInsuranceCenteralRules(long insuranceId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        #endregion
    }
}
