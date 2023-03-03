using Common.Utilities;
using DAL.Models;
using Models.PageAble;
using Models.Policy;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Policy
{
    public interface IPolicyService
    {
        #region Create

        Task<PolicyViewModel> CreatePolicyAsync(PolicyViewModel policyViewModel, CancellationToken cancellationToken);
        Task<PolicyDetailViewModel> CreatePolicyDetailAsync(PolicyDetailViewModel policyDetailViewModel, CancellationToken cancellationToken);
        #endregion

        #region Delete

        Task<bool> DeletePolicyAsync(long id, CancellationToken cancellationToken);
        Task<bool> DeletePolicyDetailAsync(long id, CancellationToken cancellationToken);
        #endregion

        #region Get

        Task<PagedResult<PolicyViewModel>> GetAllPoliciesAsync(PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<PagedResult<PolicyDetailViewModel>> GetAllPolicyDetailsAsync(PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<PolicyViewModel> GetPolicyAsync(long id, CancellationToken cancellationToken);
        Task<PolicyDetailViewModel> GetPolicyDetailAsync(long id, CancellationToken cancellationToken);
        #endregion

        #region Update

        Task<PolicyViewModel> UpdatePolicyAsync(long id, PolicyViewModel policyViewModel, CancellationToken cancellationToken);
        Task<PolicyDetailViewModel> UpdatePolicyDetailAsync(long id, PolicyDetailViewModel policyDetailViewModel, CancellationToken cancellationToken);
        Task<PagedResult<PolicyHolderViewModel>> GetAllPolicyHolderAsync(PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<PolicyHolderViewModel> CreatePolicyHolderAsync(PolicyHolderViewModel ViewModel, CancellationToken cancellationToken);
        Task<bool> DeletePolicyHolderAsync(long id, CancellationToken cancellationToken);
        Task<PolicyHolderViewModel> UpdatePolicyHolderAsync(long id, PolicyHolderViewModel ViewModel, CancellationToken cancellationToken);
        Task<PolicyHolderViewModel> GetPolicyHolderAsync(long id, CancellationToken cancellationToken);
        Task<PolicyHolderCompanyViewModel> CreatePolicyHolderCompanyAsync(PolicyHolderCompanyViewModel ViewModel, CancellationToken cancellationToken);
        Task<bool> DeletePolicyHolderCompanyAsync(long id, CancellationToken cancellationToken);
        Task<PolicyHolderCompanyViewModel> UpdatePolicyHolderCompanyAsync(long id, PolicyHolderCompanyViewModel policyDetailViewModel, CancellationToken cancellationToken);
        Task<PolicyHolderCompanyViewModel> GetPolicyHolderCompanyAsync(long id, CancellationToken cancellationToken);
        Task<PagedResult<PolicyHolderCompanyViewModel>> GetAllPolicyHolderCompanyAsync(PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<PolicyHolderPersonViewModel> CreatePolicyHolderPersonAsync(PolicyHolderPersonViewModel viewModel, CancellationToken cancellationToken);
        Task<bool> DeletePolicyHolderPersonAsync(long id, CancellationToken cancellationToken);
        Task<PolicyHolderPersonViewModel> UpdatePolicyHolderPersonAsync(long id, PolicyHolderPersonViewModel viewModel, CancellationToken cancellationToken);
        Task<PolicyHolderPersonViewModel> GetPolicyHolderPersonAsync(long id, CancellationToken cancellationToken);
        Task<PagedResult<PolicyHolderPersonViewModel>> GetAllPolicyHolderPersonAsync(PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        #endregion
    }
}