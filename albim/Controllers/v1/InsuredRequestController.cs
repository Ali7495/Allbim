using albim.Result;
using Common.Extensions;
using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Models.InsuranceRequest;
using Models.PolicyRequest;
using Models.PolicyRequestSupplement;
using Services;
using Services.PolicyRequest;
using System.Threading;
using System.Threading.Tasks;

namespace albim.Controllers.v1
{

    /// <summary>
    /// * Not Used Yet
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class InsuredRequestController : BaseController
    {
        
        #region Property
        private readonly IConfiguration _configuration;
        private readonly IInsuredRequestService _insuredRequestService;
        private readonly IPolicyRequestService _policyRequestService;

        #endregion

        #region Constructor

        public InsuredRequestController(IConfiguration configuration, IInsuredRequestService insuredRequestService, IPolicyRequestService policyRequestService)
        {
            _configuration = configuration;
            _insuredRequestService = insuredRequestService;
            _policyRequestService = policyRequestService;
        }
        #endregion

        #region InsuredRequest Actions
        [HttpPost()]
        public async Task<ApiResult<InsuredRequest>> Create(InsuranceRequestViewModel insuredRequestViewModel, CancellationToken cancellationToken)
        {
            var insuredRequest = await _insuredRequestService.CreateInsuranceRequest(insuredRequestViewModel, cancellationToken);
            return insuredRequest;
        }


        [HttpGet("{id}")]
        public async Task<ApiResult<InsuredRequest>> GetDetail(long code, CancellationToken cancellationToken)
        {
            var insuredRequest = await _insuredRequestService.DetailInsuranceRequest(code, cancellationToken);
            return insuredRequest;
        }

        [HttpGet()]
        public async Task<ApiResult<PagedResult<InsuredRequest>>> GetAll(int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var model = await _insuredRequestService.GetInsuranceRequest(page, pageSize,  cancellationToken);
            return model;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult<string>> Delete(long code, CancellationToken cancellationToken)
        {
            var res = await _insuredRequestService.DeleteInsuranceRequest(code, cancellationToken);
            return res.ToString();
        }
        [HttpPut()]
        public async Task<ApiResult<InsuredRequest>> Update(long Id, InsuranceRequestViewModel cityViewModel, CancellationToken cancellationToken)
        {
            var result = await _insuredRequestService.UpdateInsuranceRequest(Id, cityViewModel, cancellationToken);
            return result;
        }
        #endregion


        #region InsuredRequestCompany Actions
        /// <summary>
        /// To Create a User
        /// </summary>
        /// <param name="insuredRequestCompanyViewModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("PolicyRequest/{Code}/Insured/{Id}/Company")]
        public async Task<ApiResult<InsuredRequestCompany>> CreateInsuredRequestCompany(InsuranceRequestCompanyViewModel insuredRequestCompanyViewModel, CancellationToken cancellationToken)
        {
            var insuredRequestCompany = await _insuredRequestService.CreateInsuranceRequestCompany(insuredRequestCompanyViewModel, cancellationToken);
            return insuredRequestCompany;
        }


        [HttpGet("{InsuredRequestId}/Company")]
        public async Task<ApiResult<InsuredRequest>> GetDetailInsuredRequestCompany(long code, CancellationToken cancellationToken)
        {
            var insuredRequestCompany = await _insuredRequestService.DetailInsuranceRequest(code, cancellationToken);
            return insuredRequestCompany;
        }

        [HttpGet("{InsuredRequestId}/Company")]
        public async Task<ApiResult<PagedResult<InsuredRequestCompany>>> GetAllInsuredRequestCompany([FromQuery] int page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var model = await _insuredRequestService.GetInsuredRequestCompany(page, pageSize, cancellationToken);
            return model;
        }

        [HttpDelete("{InsuredRequestId}/Company")]
        public async Task<ApiResult<string>> DeleteInsuredRequestCompany(long code, CancellationToken cancellationToken)
        {
            var res = await _insuredRequestService.DeleteInsuredRequestCompany(code, cancellationToken);
            return res.ToString();
        }
        [HttpPut("PolicyRequest/{Code}/Insured/{Id}/Company/{CompanyCode}")]
        public async Task<ApiResult<InsuredRequestCompany>> UpdateInsuredRequestCompany(long Id, InsuranceRequestCompanyViewModel ViewModel, CancellationToken cancellationToken)
        {
            var result = await _insuredRequestService.UpdateInsuredRequestCompany(Id, ViewModel, cancellationToken);
            return result;
        }
        #endregion


        #region InsuredRequestPerson Actions
        /// <summary>
        /// To Create a User
        /// </summary>
        /// <param name="insuredRequestViewModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("PolicyRequest/{Code}/Insured/{Id}/Person")]
        public async Task<ApiResult<InsuredRequestPerson>> CreatePolicyRequestPerson(InsuranceRequestPersonViewModel insuredRequestViewModel, CancellationToken cancellationToken)
        {
            var insuredRequest = await _insuredRequestService.CreateInsuredRequestPerson(insuredRequestViewModel, cancellationToken);
            return insuredRequest;
        }


        [HttpGet("{InsuredRequestId}/Person")]
        public async Task<ApiResult<InsuredRequestPerson>> GetDetailInsuredRequestPerson(long code, CancellationToken cancellationToken)
        {
            var insuredRequest = await _insuredRequestService.DetailInsuredRequestPerson(code, cancellationToken);
            return insuredRequest;
        }

        [HttpGet("{InsuredRequestId}/Person")]
        public async Task<ApiResult<PagedResult<InsuredRequestPerson>>> GetAll([FromQuery] int page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var model = await _insuredRequestService.GetInsuredRequestPerson(page, pageSize, cancellationToken);
            return model;
        }

        [HttpDelete("{InsuredRequestId}/Person")]
        public async Task<ApiResult<string>> DeleteInsuredRequestPerson(long code, CancellationToken cancellationToken)
        {
            var res = await _insuredRequestService.DeleteInsuredRequestPerson(code, cancellationToken);
            return res.ToString();
        }
        [HttpPut("PolicyRequest/{Code}/Insured/{Id}/Person/{PersonCode}")]
        public async Task<ApiResult<InsuredRequestPerson>> Update(long Id, InsuranceRequestPersonViewModel ViewModel, CancellationToken cancellationToken)
        {
            var result = await _insuredRequestService.UpdateInsuredRequestPerson(Id, ViewModel, cancellationToken);
            return result;
        }

        
        #endregion
        #region InsuredRequestPlace Actions
        /// <summary>
        /// To Create a User
        /// </summary>
        /// <param name="insuredRequestPlaceViewModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("PolicyRequest/{Code}/Insured/{Id}/Place")]
        public async Task<ApiResult<InsuredRequestPlace>> CreatePolicyRequestPlace(InsuranceRequestPlaceViewModel insuredRequestPlaceViewModel, CancellationToken cancellationToken)
        {
            var insuredRequest = await _insuredRequestService.CreateInsuredRequestPlace(insuredRequestPlaceViewModel, cancellationToken);
            return insuredRequest;
        }


        [HttpGet("{InsuredRequestId}/Place")]
        public async Task<ApiResult<InsuredRequestPlace>> GetDetailInsuredRequestPlace(long code, CancellationToken cancellationToken)
        {
            var insuredRequest = await _insuredRequestService.DetailInsuredRequestPlace(code, cancellationToken);
            return insuredRequest;
        }

        [HttpGet("{InsuredRequestId}/Place")]
        public async Task<ApiResult<PagedResult<InsuredRequestPlace>>> GetAllInsuredRequestPlace([FromQuery] int page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var model = await _insuredRequestService.GetInsuredRequestPlace(page, pageSize, cancellationToken);
            return model;
        }

        [HttpDelete("{InsuredRequestId}/Place")]
        public async Task<ApiResult<string>> DeletePlace(long code, CancellationToken cancellationToken)
        {
            var res = await _insuredRequestService.DeleteInsuredRequestPlace(code, cancellationToken);
            return res.ToString();
        }
        [HttpPut("PolicyRequest/{Code}/Insured/{Id}/Place")]
        public async Task<ApiResult<InsuredRequestPlace>> UpdatePlace(long Id, InsuranceRequestPlaceViewModel ViewModel, CancellationToken cancellationToken)
        {
            var result = await _insuredRequestService.UpdateInsuredRequestPlace(Id, ViewModel, cancellationToken);
            return result;
        }

        #endregion
        #region InsuredRequestRelatedPerson Actions
        /// <summary>
        /// To Create a User
        /// </summary>
        /// <param name="insuredRequestViewModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("PolicyRequest/{Code}/Insured/{Id}/Related")]
        public async Task<ApiResult<InsuredRequestRelatedPerson>> CreateRelatedPerson(InsuredRequestRelatedPersonViewModel insuredRequestViewModel, CancellationToken cancellationToken)
        {
            var insuredRequest = await _insuredRequestService.CreateInsuredRequestRelatedPerson(insuredRequestViewModel, cancellationToken);
            return insuredRequest;
        }


        [HttpGet("{InsuredRequestId}/RelatedPerson")]
        public async Task<ApiResult<InsuredRequestRelatedPerson>> GetDetailRelatedPerson(long code, CancellationToken cancellationToken)
        {
            var insuredRequest = await _insuredRequestService.DetailInsuredRequestRelatedPerson(code, cancellationToken);
            return insuredRequest;
        }

        [HttpGet("{InsuredRequestId}/RelatedPerson")]
        public async Task<ApiResult<PagedResult<InsuredRequestRelatedPerson>>> GetAllRelatedPerson([FromQuery] int page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var model = await _insuredRequestService.GetInsuredRequestRelatedPerson(page, pageSize, cancellationToken);
            return model;
        }

        [HttpDelete("{InsuredRequestId}/RelatedPerson")]
        public async Task<ApiResult<string>> DeleteRelatedPerson(long code, CancellationToken cancellationToken)
        {
            var res = await _insuredRequestService.DeleteInsuredRequestRelatedPerson(code, cancellationToken);
            return res.ToString();
        }
        [HttpPut("PolicyRequest/{Code}/Insured/{Id}/Related/{PersonCode}")]
        public async Task<ApiResult<InsuredRequestRelatedPerson>> UpdateRelatedPerson(long Id, InsuredRequestRelatedPersonViewModel ViewModel, CancellationToken cancellationToken)
        {
            var result = await _insuredRequestService.UpdateInsuredRequestRelatedPerson(Id, ViewModel, cancellationToken);
            return result;
        }

        #endregion

        #region InsuredRequestVehicle

        [HttpPost("PolicyRequest/{Code}/Insured/{Id}/Vehicle")]
        public async Task<ApiResult<InsuredRequestVehicle>> CreatePolicyRequestVehicle(InsuredRequestVehicleViewModel insuredRequestViewModel, CancellationToken cancellationToken)
        {
            var insuredRequest = await _insuredRequestService.CreateInsuredRequestVehicle(insuredRequestViewModel, cancellationToken);
            return insuredRequest;
        }

        [HttpPut("PolicyRequest/{Code}/Insured/{Id}/Vehicle/{VehicleCode}")]
        public async Task<ApiResult<InsuredRequestVehicle>> Update(long Id, InsuredRequestVehicleViewModel ViewModel, CancellationToken cancellationToken)
        {
            var result = await _insuredRequestService.UpdateInsuredRequestVehicle(Id, ViewModel, cancellationToken);
            return result;
        }
        #endregion

    }
}
