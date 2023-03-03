using albim.Controllers;
using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.PageAble;
using Models.Policy;
using Services.Policy;
using System.Threading;
using System.Threading.Tasks;

namespace Albim.Controllers.v1
{
    /// <summary>
    /// * Not Used Yet
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [AllowAnonymous]

    public class PolicyController : BaseController
    {
        #region Fields

        private readonly IPolicyService _policyService;
        #endregion

        #region CTOR

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }
        #endregion

        #region Policy Actions
        [HttpPost("/Policy")]
        public async Task<PolicyViewModel> CreatePolicy(PolicyViewModel policyViewModel, CancellationToken cancellationToken)
        {
            var result = await _policyService.CreatePolicyAsync(policyViewModel, cancellationToken);
            return result;
        }

        [HttpDelete("/Policy/{id}")]
        public async Task<bool> DeletePolicy(long id, CancellationToken cancellationToken)
        {
            var result = await _policyService.DeletePolicyAsync(id, cancellationToken);
            return result;
        }

        [HttpPut("/Policy/{id}")]
        public async Task<PolicyViewModel> UpdatePolicy(long id, PolicyViewModel policyViewModel, CancellationToken cancellationToken)
        {
            var result = await _policyService.UpdatePolicyAsync(id, policyViewModel, cancellationToken);
            return result;
        }

        [HttpGet("/Policy/{id}")]
        public async Task<PolicyViewModel> GetPolicy(long id, CancellationToken cancellationToken)
        {
            var result = await _policyService.GetPolicyAsync(id, cancellationToken);
            return result;
        }

        [HttpGet("/Policy")]
        public async Task<PagedResult<PolicyViewModel>> GetAllPolicies([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            var result = await _policyService.GetAllPoliciesAsync(pageAbleResult, cancellationToken);
            return result;
        }
        #endregion

        #region PolicyDetail Actions
        [HttpPost("/PolicyDetail")]
        public async Task<PolicyDetailViewModel> CreatePolicyDetail(PolicyDetailViewModel PolicyDetailViewModel, CancellationToken cancellationToken)
        {
            var result = await _policyService.CreatePolicyDetailAsync(PolicyDetailViewModel, cancellationToken);
            return result;
        }

        [HttpDelete("/PolicyDetail/{id}")]
        public async Task<bool> DeletePolicyDetail(long id, CancellationToken cancellationToken)
        {
            var result = await _policyService.DeletePolicyDetailAsync(id, cancellationToken);
            return result;
        }

        [HttpPut("/PolicyDetail/{id}")]
        public async Task<PolicyDetailViewModel> UpdatePolicyDetail(long id, PolicyDetailViewModel PolicyDetailViewModel, CancellationToken cancellationToken)
        {
            var result = await _policyService.UpdatePolicyDetailAsync(id, PolicyDetailViewModel, cancellationToken);
            return result;
        }

        [HttpGet("/PolicyDetail/{id}")]
        public async Task<PolicyDetailViewModel> GetPolicyDetail(long id, CancellationToken cancellationToken)
        {
            var result = await _policyService.GetPolicyDetailAsync(id, cancellationToken);
            return result;
        }

        [HttpGet("/PolicyDetail")]
        public async Task<PagedResult<PolicyDetailViewModel>> GetAllPolicyDetails([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            var result = await _policyService.GetAllPolicyDetailsAsync(pageAbleResult, cancellationToken);
            return result;
        }
        #endregion

        #region PolicyHolder Actions
        [HttpPost("/PolicyHolder")]
        public async Task<PolicyHolderViewModel> CreatePolicyHolder(PolicyHolderViewModel ViewModel, CancellationToken cancellationToken)
        {
            var result = await _policyService.CreatePolicyHolderAsync(ViewModel, cancellationToken);
            return result;
        }

        [HttpDelete("/PolicyHolder/{id}")]
        public async Task<bool> DeletePolicyHolder(long id, CancellationToken cancellationToken)
        {
            var result = await _policyService.DeletePolicyHolderAsync(id, cancellationToken);
            return result;
        }

        [HttpPut("/PolicyHolder/{id}")]
        public async Task<PolicyHolderViewModel> UpdatePolicyHolder(long id, PolicyHolderViewModel ViewModel, CancellationToken cancellationToken)
        {
            var result = await _policyService.UpdatePolicyHolderAsync(id, ViewModel, cancellationToken);
            return result;
        }

        [HttpGet("/PolicyHolder/{id}")]
        public async Task<PolicyHolderViewModel> GetPolicyHolder(long id, CancellationToken cancellationToken)
        {
            var result = await _policyService.GetPolicyHolderAsync(id, cancellationToken);
            return result;
        }

        [HttpGet("/PolicyHolder")]
        public async Task<PagedResult<PolicyHolderViewModel>> GetAllPolicyHolder([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            var result = await _policyService.GetAllPolicyHolderAsync(pageAbleResult, cancellationToken);
            return result;
        }
        #endregion
        #region PolicyHolderCompany Actions
        [HttpPost("{PolicyHolderId}/Company")]
        public async Task<PolicyHolderCompanyViewModel> CreatePolicyHolderCompany(PolicyHolderCompanyViewModel ViewModel, CancellationToken cancellationToken)
        {
            var result = await _policyService.CreatePolicyHolderCompanyAsync(ViewModel, cancellationToken);
            return result;
        }

        [HttpDelete("{PolicyHolderId}/Company/{id}")]
        public async Task<bool> DeletePolicyHolderCompany(long id, CancellationToken cancellationToken)
        {
            var result = await _policyService.DeletePolicyHolderCompanyAsync(id, cancellationToken);
            return result;
        }

        [HttpPut("{PolicyHolderId}/Company/{id}")]
        public async Task<PolicyHolderCompanyViewModel> UpdatePolicyHolderCompany(long id, PolicyHolderCompanyViewModel ViewModel, CancellationToken cancellationToken)
        {
            var result = await _policyService.UpdatePolicyHolderCompanyAsync(id, ViewModel, cancellationToken);
            return result;
        }

        [HttpGet("{PolicyHolderId}/Company/{id}")]
        public async Task<PolicyHolderCompanyViewModel> GetPolicyHolderCompany(long id, CancellationToken cancellationToken)
        {
            var result = await _policyService.GetPolicyHolderCompanyAsync(id, cancellationToken);
            return result;
        }

        [HttpGet("{PolicyHolderId}/Company")]
        public async Task<PagedResult<PolicyHolderCompanyViewModel>> GetAllPolicyHolderCompany([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            var result = await _policyService.GetAllPolicyHolderCompanyAsync(pageAbleResult, cancellationToken);
            return result;
        }
        #endregion

        #region PolicyHolder Person Actions
        [HttpPost("{PolicyHolderId}/Person")]
        public async Task<PolicyHolderPersonViewModel> CreatePolicyHolderPerson(PolicyHolderPersonViewModel ViewModel, CancellationToken cancellationToken)
        {
            var result = await _policyService.CreatePolicyHolderPersonAsync(ViewModel, cancellationToken);
            return result;
        }

        [HttpDelete("{PolicyHolderId}/Person/{id}")]
        public async Task<bool> DeletePolicyHolderPerson(long id, CancellationToken cancellationToken)
        {
            var result = await _policyService.DeletePolicyHolderPersonAsync(id, cancellationToken);
            return result;
        }

        [HttpPut("{PolicyHolderId}/Person/{id}")]
        public async Task<PolicyHolderPersonViewModel> UpdatePolicyHolderPerson(long id, PolicyHolderPersonViewModel ViewModel, CancellationToken cancellationToken)
        {
            var result = await _policyService.UpdatePolicyHolderPersonAsync(id, ViewModel, cancellationToken);
            return result;
        }

        [HttpGet("{PolicyHolderId}/Person/{id}")]
        public async Task<PolicyHolderPersonViewModel> GetPolicyHolderPerson(long id, CancellationToken cancellationToken)
        {
            var result = await _policyService.GetPolicyHolderPersonAsync(id, cancellationToken);
            return result;
        }

        [HttpGet("{PolicyHolderId}/Person")]
        public async Task<PagedResult<PolicyHolderPersonViewModel>> GetAllPolicyHolderPerson([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            var result = await _policyService.GetAllPolicyHolderPersonAsync(pageAbleResult, cancellationToken);
            return result;
        }
        #endregion

    }
}
