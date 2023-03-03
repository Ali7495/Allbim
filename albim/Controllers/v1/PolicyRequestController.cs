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
using Services.PolicyRequest;
using albim.Result;
using Models.PolicyRequest;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using Common.Exceptions;
using Common.Extensions;
using Models.PolicyRequestIssue;
using Models.PolicyRequestSupplement;
using Models.PolicyRequestInspection;
using Models.PolicyRequestPaymentInfo;
using Services;
using Services.PolicyRequestStatus;
using Models.BodySupplementInfo;
using Models.PolicyRequestCommet;
using Services.PolicyRequestComment;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;

namespace Albim.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]

    public class PolicyRequestController : BaseController
    {
        #region Fields
        private readonly IPolicyRequestCommentService _policyRequestCommentService;
        private readonly IConfiguration _configuration;
        private readonly IPaymentService _paymentService;
        private readonly IPolicyRequestService _policyRequestService;
        private readonly IBodyRequestService _bodyRequestService;
        private readonly IPolicyRequestStatusService _policyRequestStatusService;
        private readonly IPolicyRequestFactorService _policyRequestFactorService;
        private readonly IPolicyRequestDetailService _policyRequestDetailService;
        private readonly IPersonService _personService;
        #endregion

        #region CTOR

        public PolicyRequestController(IPolicyRequestService policyRequestService, IConfiguration configuration, IPolicyRequestStatusService policyRequestStatusService,
            IPolicyRequestDetailService policyRequestDetailService,
            IPolicyRequestFactorService policyRequestFactorService, IPaymentService paymentService, IPersonService personService,IPolicyRequestCommentService policyRequestCommentService,
            IBodyRequestService bodyRequestService
            
            )
        {
            _policyRequestCommentService = policyRequestCommentService;
            _policyRequestStatusService = policyRequestStatusService;
            _personService = personService;
            _paymentService = paymentService;
            _policyRequestFactorService = policyRequestFactorService;
            _policyRequestService = policyRequestService;
            _configuration = configuration;
            _policyRequestDetailService = policyRequestDetailService;
            _bodyRequestService = bodyRequestService;
        }

        #endregion

        #region PolicyRequestStatus
        [HttpGet("Status")]
        public async Task<List<PolicyRequestStatusViewModel>> GetPolicyRequestStatus(CancellationToken cancellationToken)
        {
            var result = await _policyRequestStatusService.GetPolicyRequestStatus(cancellationToken);
            return result;
        }
        #endregion

        #region MyRequests
        [HttpGet("MinePolicyInsurance")]
        public async Task<ApiResult<List<MyPolicyRequestViewModel>>> MinePolicyInsurance(CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            var result = await _policyRequestService.GetMinePolicyInsurance(userId, cancellationToken);
            return result;
        }

        #endregion

        #region Policy Actions

        [HttpPost("")]
        public async Task<PolicyRequestSummaryViewModel> CreatePolicyRequest(
            PolicyRequestInputViewModel policyRequestInputViewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            
            var result = await _policyRequestService.Create(policyRequestInputViewModel, userId, cancellationToken);
            return result;
        }




        [HttpPost("{code}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> CreateHolderSupplementInfo(string code, PolicySupplementViewModel viewModel, CancellationToken cancellationToken)
        {
            PolicySupplementViewModel result = await _policyRequestService.CreatePolicyRequestHolderSupplementInfoAsync(code, viewModel, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }

        [HttpGet("{code}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> GetHolderSupplementInfo(string code, CancellationToken cancellationToken)
        {
            PolicySupplementViewModel result = await _policyRequestService.GetPolicyRequestHolderSupplementInfo(code, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }


        [HttpPost("mine/{code}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> CreateHolderSupplementInfoMine(Guid code, PolicySupplementViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PolicySupplementViewModel result = await _policyRequestService.CreatePolicyRequestHolderSupplementInfoAsyncMine(userId, code, viewModel, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }

        [HttpGet("mine/{code}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> GetHolderSupplementInfoMine(string code, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PolicySupplementViewModel result = await _policyRequestService.GetPolicyRequestHolderSupplementInfoMine(userId, code, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }







        [HttpPut("{code}/Issue")]
        public async Task<ApiResult<PolicyRequestIssueViewModel>> AddOrUpdatePolicyRequestIssue(string code, PolicyRequestIssueInputViewModel viewModel, CancellationToken cancellationToken)
        {
            PolicyRequestIssueViewModel result = await _policyRequestService.CreateOrUpdatePolicyRequestHolderIssueAsync(code, viewModel, cancellationToken);
            // return result;
            return result;
        }


        [HttpGet("{code}/Issue")]
        public async Task<ApiResult<PolicyRequestIssueViewModel>> GetPolicyRequestIssue(string code, CancellationToken cancellationToken)
        {
            PolicyRequestIssueViewModel result = await _policyRequestService.GetPolicyRequestHolderIssueAsync(code, cancellationToken);
            return result;
            // return new PolicyRequestIssueViewModel();
        }

        [HttpPut("mine/{code}/Issue")]
        public async Task<ApiResult<PolicyRequestIssueViewModel>> AddOrUpdatePolicyRequestIssueMine(string code, PolicyRequestIssueInputViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PolicyRequestIssueViewModel result = await _policyRequestService.CreateOrUpdatePolicyRequestHolderIssueAsyncMine(userId, code, viewModel, cancellationToken);
            // return result;
            return result;
        }

        [HttpGet("mine/{code}/Issue")]
        public async Task<ApiResult<PolicyRequestIssueViewModel>> GetPolicyRequestIssueMine(string code, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PolicyRequestIssueViewModel result = await _policyRequestService.GetPolicyRequestHolderIssueAsyncMine(userId, code, cancellationToken);
            return result;
            // return new PolicyRequestIssueViewModel();
        }





        [HttpGet("{code}/PaymentInfo")]
    
        public async Task<PolicyRequestPaymentViewModel> GetPolicyRequestPaymentInfo([FromRoute] Guid code, CancellationToken cancellationToken)
        {

            PolicyRequestPaymentViewModel result = await _policyRequestService.GetPolicyRequestPaymentDetails(code, cancellationToken);
            return result;
        }

        [HttpGet("mine/{code}/PaymentInfo")]

        public async Task<PolicyRequestPaymentViewModel> GetPolicyRequestPaymentInfoMine([FromRoute] Guid code, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PolicyRequestPaymentViewModel result = await _policyRequestService.GetPolicyRequestPaymentDetailsMine(userId, code, cancellationToken);
            return result;
        }



        [HttpPost("{code}/Attachment")]
        public async Task<ApiResult<PolicyRequestAttachmentViewModel>> CreatePolicyRequestAttachment([FromRoute] string code,
            [FromForm] PolicyRequestInputAttachmentViewModel viewModel, CancellationToken cancellationToken)
        {
            var result =
                await _policyRequestService.CreatePolicyRequestAttachment(cancellationToken, viewModel.File, code, viewModel.TypeId);
            return result;
        }

        [HttpGet("{code}/Attachment")]
        public async Task<ApiResult<List<PolicyRequestAttachmentDownloadViewModel>>> GetPolicyRequestAttachments([FromRoute] string code, CancellationToken cancellationToken)
        {
            var result = await _policyRequestService.GetPolicyRequestAttachments(code, cancellationToken);
            return result;
        }


        // [HttpDelete("{id}")]
        // public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        // {
        //     var result = await _policyRequestService.Delete(id, cancellationToken);
        //     return result;
        // }
        //
        // [HttpPut("{id}")]
        // public async Task<PolicyRequestViewModel> Update(long id, PolicyRequestViewModel ViewModel,
        //     CancellationToken cancellationToken)
        // {
        //     var result = await _policyRequestService.Update(id, ViewModel, cancellationToken);
        //     return result;
        // }

        // [HttpGet("{id}")]
        // public async Task<PolicyRequestViewModel> Get(long id, CancellationToken cancellationToken)
        // {
        //     var result = await _policyRequestService.GetById(id, cancellationToken);
        //     return result;
        // }

        // [HttpGet("")]
        // public async Task<PagedResult<PolicyRequestViewModel>> GetAll([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        // {
        //     var result = await _policyRequestService.GetAll(pageAbleResult, cancellationToken);
        //     return result;
        // }

        [HttpGet("")]
        public async Task<ApiResult<PagedResult<PolicyRequestViewModel>>> GetAllPlolicyRequests([FromQuery] List<Guid> companyCodes, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            long roleId = long.Parse(userRole);

            var result = await _policyRequestService.GetAllPolicyRequestsAsync(userId, roleId,companyCodes, pageAbleResult, cancellationToken);
            return result;
        }
        [HttpGet("Status/{slug}")]
        public async Task<ApiResult<PagedResult<PolicyRequestViewModel>>> GetAllPlolicyRequestsStatus([FromRoute] string slug, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            long roleId = long.Parse(userRole);

            var result = await _policyRequestService.GetAllPolicyRequestsAsyncBySlug(slug, userId, roleId, pageAbleResult, cancellationToken);
            return result;
        }


        #region PolicyRequestDetail Actions

        [HttpPost("/PolicyRequest/{id}/Detail")]
        public async Task<PolicyRequestDetailViewModel> Create(PolicyRequestDetailViewModel viewModel,
            CancellationToken cancellationToken)
        {
            var result = await _policyRequestDetailService.Create(viewModel, cancellationToken);
            return result;
        }

        [HttpDelete("/PolicyRequest/{id}/Detail/{detailId}")]
        public async Task<bool> DeletePolicyDetail(long id, long detailId, CancellationToken cancellationToken)
        {
            var result = await _policyRequestDetailService.Delete(detailId, cancellationToken);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<PolicyRequestDetailViewModel> UpdatePolicyDetail(long id,
            PolicyRequestDetailViewModel ViewModel, CancellationToken cancellationToken)
        {
            var result = await _policyRequestDetailService.Update(id, ViewModel, cancellationToken);
            return result;
        }

        // [HttpGet("{id}")]
        // public async Task<PolicyRequestDetailViewModel> GetPolicyDetail(long id, CancellationToken cancellationToken)
        // {
        //     var result = await _policyRequestDetailService.GetPolicyDetail(id, cancellationToken);
        //     return result;
        // }

        // [HttpGet("")]
        // public async Task<PagedResult<PolicyRequestDetailViewModel>> GetAllPolicyDetails(
        //     [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        // {
        //     var result = await _policyRequestDetailService.GetAll(pageAbleResult, cancellationToken);
        //     return result;
        // }

        #endregion


        [HttpPost("{Code}/Holder")]
        public async Task<PolicyRequestHolderViewModel> CreatePolicyRequestHolder(PolicyRequestHolderViewModel viewModel, CancellationToken cancellationToken)
        {
            var result = await _policyRequestService.CreatePolicyRequestHolderAsync(viewModel, cancellationToken);
            return result;
        }
        [HttpPut("{Code}/Holder/{id}")]
        public async Task<PolicyRequestHolderViewModel> UpdatePolicyRequestHolder(long id, PolicyRequestHolderViewModel viewModel, CancellationToken cancellationToken)
        {
            var result = await _policyRequestService.UpdatePolicyRequestHolderAsync(id, viewModel, cancellationToken);
            return result;
        }
        [HttpPost("{Code}/AttachmentRequest")]
        public async Task<PolicyRequestAttachmentViewModel> CreatePolicyRequestAttachment(PolicyRequestAttachmentViewModel viewModel, CancellationToken cancellationToken)
        {
            var result = await _policyRequestService.CreatePolicyRequestAttachmentAsync(viewModel, cancellationToken);
            return result;
        }
        [HttpPut("{Code}/AttachmentRequest/{id}")]
        public async Task<PolicyRequestAttachmentViewModel> UpdatePolicyRequestAttachment(long id, PolicyRequestAttachmentViewModel viewModel, CancellationToken cancellationToken)
        {
            var result = await _policyRequestService.UpdatePolicyRequestAttachmentAsync(id, viewModel, cancellationToken);
            return result;
        }

        //
        // #region PolicyHolder Actions
        // [HttpPost("/PolicyHolder")]
        // public async Task<PolicyHolderViewModel> CreatePolicyHolder(PolicyHolderViewModel ViewModel, CancellationToken cancellationToken)
        // {
        //     var result = await _policyService.CreatePolicyHolderAsync(ViewModel, cancellationToken);
        //     return result;
        // }
        //
        // [HttpDelete("/PolicyHolder/{id}")]
        // public async Task<bool> DeletePolicyHolder(long id, CancellationToken cancellationToken)
        // {
        //     var result = await _policyService.DeletePolicyHolderAsync(id, cancellationToken);
        //     return result;
        // }
        //
        // [HttpPut("/PolicyHolder/{id}")]
        // public async Task<PolicyHolderViewModel> UpdatePolicyHolder(long id, PolicyHolderViewModel ViewModel, CancellationToken cancellationToken)
        // {
        //     var result = await _policyService.UpdatePolicyHolderAsync(id, ViewModel, cancellationToken);
        //     return result;
        // }
        //
        // [HttpGet("/PolicyHolder/{id}")]
        // public async Task<PolicyHolderViewModel> GetPolicyHolder(long id, CancellationToken cancellationToken)
        // {
        //     var result = await _policyService.GetPolicyHolderAsync(id, cancellationToken);
        //     return result;
        // }
        //
        // [HttpGet("/PolicyHolder")]
        // public async Task<PagedResult<PolicyHolderViewModel>> GetAllPolicyHolder([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        // {
        //     var result = await _policyService.GetAllPolicyHolderAsync(pageAbleResult, cancellationToken);
        //     return result;
        // }
        // #endregion
        // #region PolicyHolderCompany Actions
        // [HttpPost("{PolicyHolderId}/Company")]
        // public async Task<PolicyHolderCompanyViewModel> CreatePolicyHolderCompany(PolicyHolderCompanyViewModel ViewModel, CancellationToken cancellationToken)
        // {
        //     var result = await _policyService.CreatePolicyHolderCompanyAsync(ViewModel, cancellationToken);
        //     return result;
        // }
        //
        // [HttpDelete("{PolicyHolderId}/Company/{id}")]
        // public async Task<bool> DeletePolicyHolderCompany(long id, CancellationToken cancellationToken)
        // {
        //     var result = await _policyService.DeletePolicyHolderCompanyAsync(id, cancellationToken);
        //     return result;
        // }
        //
        // [HttpPut("{PolicyHolderId}/Company/{id}")]
        // public async Task<PolicyHolderCompanyViewModel> UpdatePolicyHolderCompany(long id, PolicyHolderCompanyViewModel ViewModel, CancellationToken cancellationToken)
        // {
        //     var result = await _policyService.UpdatePolicyHolderCompanyAsync(id, ViewModel, cancellationToken);
        //     return result;
        // }
        //
        // [HttpGet("{PolicyHolderId}/Company/{id}")]
        // public async Task<PolicyHolderCompanyViewModel> GetPolicyHolderCompany(long id, CancellationToken cancellationToken)
        // {
        //     var result = await _policyService.GetPolicyHolderCompanyAsync(id, cancellationToken);
        //     return result;
        // }
        //
        // [HttpGet("{PolicyHolderId}/Company")]
        // public async Task<PagedResult<PolicyHolderCompanyViewModel>> GetAllPolicyHolderCompany([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        // {
        //     var result = await _policyService.GetAllPolicyHolderCompanyAsync(pageAbleResult, cancellationToken);
        //     return result;
        // }
        // #endregion
        //
        // #region PolicyHolder Person Actions
        // [HttpPost("{PolicyHolderId}/Person")]
        // public async Task<PolicyHolderPersonViewModel> CreatePolicyHolderPerson(PolicyHolderPersonViewModel ViewModel, CancellationToken cancellationToken)
        // {
        //     var result = await _policyService.CreatePolicyHolderPersonAsync(ViewModel, cancellationToken);
        //     return result;
        // }
        //
        // [HttpDelete("{PolicyHolderId}/Person/{id}")]
        // public async Task<bool> DeletePolicyHolderPerson(long id, CancellationToken cancellationToken)
        // {
        //     var result = await _policyService.DeletePolicyHolderPersonAsync(id, cancellationToken);
        //     return result;
        // }
        //
        // [HttpPut("{PolicyHolderId}/Person/{id}")]
        // public async Task<PolicyHolderPersonViewModel> UpdatePolicyHolderPerson(long id, PolicyHolderPersonViewModel ViewModel, CancellationToken cancellationToken)
        // {
        //     var result = await _policyService.UpdatePolicyHolderPersonAsync(id, ViewModel, cancellationToken);
        //     return result;
        // }
        //
        // [HttpGet("{PolicyHolderId}/Person/{id}")]
        // public async Task<PolicyHolderPersonViewModel> GetPolicyHolderPerson(long id, CancellationToken cancellationToken)
        // {
        //     var result = await _policyService.GetPolicyHolderPersonAsync(id, cancellationToken);
        //     return result;
        // }
        //
        // [HttpGet("{PolicyHolderId}/Person")]
        // public async Task<PagedResult<PolicyHolderPersonViewModel>> GetAllPolicyHolderPerson([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        // {
        //     var result = await _policyService.GetAllPolicyHolderPersonAsync(pageAbleResult, cancellationToken);
        //     return result;
        // }

        #endregion



        #region My PolicyRequest

        [HttpPost("mine")]
        public async Task<PolicyRequestSummaryViewModel> CreatePolicyRequestMine(
            PolicyRequestInputViewModel policyRequestInputViewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            var result = await _policyRequestService.CreatePolicyRequestMine(policyRequestInputViewModel, userId, cancellationToken);
            return result;
        }

        [HttpGet("mine")]
        public async Task<ApiResult<PagedResult<PolicyRequestViewModel>>> GetAllPlolicyRequestsMine([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }
            

            long roleId = long.Parse(userRole);

            var result = await _policyRequestService.GetAllPolicyRequestsAsyncMine(userId, roleId, pageAbleResult, cancellationToken);
            return result;
        }


        [HttpGet("mine/{code}")]
        public async Task<PolicyRequestViewModel> GetPolicyRequestByCodeMine(Guid code, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }
            

            var result = await _policyRequestService.GetPolicyRequestByCodeMine(userId, code, cancellationToken);
            return result;
        }



        [HttpPut("mine/{code}")]
        public async Task<PolicyRequestViewModel> UpdateRequestMine(Guid code, PolicyRequestViewModel ViewModel,
            CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            var result = await _policyRequestService.UpdateRequestMine(userId, code, ViewModel, cancellationToken);
            return result;
        }

        //[HttpDelete("mine")]
        //public async Task<bool> DeleteRequestMine(Guid code, CancellationToken cancellationToken)
        //{
        //    long userId = long.Parse(HttpContext.User.Identity.GetUserId());
        //    var claims = HttpContext.User.Claims.ToList();
        //    var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
        //    if (userRole == null)
        //    {
        //        throw new BadRequestException("شما نقشی در این سیستم ندارید");
        //    }

        //    var result = await _policyRequestService.DeleteMyRequest(code, cancellationToken);
        //    return result;
        //}
        #endregion




        #region Policy Request factor Actions

        [HttpPost("{code}/factor")]
        public async Task<PaymentViewModel> CreateFactor([FromRoute] Guid code, PaymentViewModel ViewModel,
            CancellationToken cancellationToken)
        {
            var result = await _paymentService.Create(ViewModel, code, cancellationToken);
            return result;
        }

        [HttpGet("{code}/factor")]
        public async Task<PolicyRequestFactorViewModel> GetFactor([FromRoute] Guid code,
            CancellationToken cancellationToken)
        {
            var result = await _policyRequestFactorService.GetByCode(code, cancellationToken);
            return result;
        }


        [HttpGet("{code}/factor/{Id}")]
        public async Task<PolicyRequestFactorViewModel> GetFactorId([FromRoute] Guid code, [FromRoute] long Id,
            CancellationToken cancellationToken)
        {
            var result = await _paymentService.GetByFactorId(code, Id, cancellationToken);
            return result;
        }

        [HttpPut("{code}/factor/{Id}")]
        public async Task<PaymentViewModel> PutFactor([FromRoute] Guid code, [FromRoute] long Id,
            PaymentViewModel viewmodel, CancellationToken cancellationToken)
        {
            var result = await _paymentService.Update(code, Id, viewmodel, cancellationToken);
            return result;
        }

        [HttpDelete("{code}/factor/{Id}")]
        public async Task<ApiResult<string>> DeleteFactor([FromRoute] Guid code, [FromRoute] long Id,
            CancellationToken cancellationToken)
        {
            var result = await _policyRequestFactorService.Delete(code, Id, cancellationToken);
            return result.ToString();
        }


        #endregion

        #region Address

        [AllowAnonymous]
        [HttpGet("province/{provinceId}")]
        public async Task<List<PolicyRequestByProvinceViewModel>> GetPolicyRequestsBasedOnProvince(long provinceId, CancellationToken cancellationToken)
        {
            return await _policyRequestService.GetPolicyRequestBasedOnProvince(provinceId, cancellationToken);
        }

        #endregion


        #region PolicyRequest

        [HttpGet("company/{code}")]
        public async Task<PagedResult<MyPolicyRequestViewModel>> GetPolicyByCompanyCode(Guid code, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            var result = await _policyRequestService.GetAllByCompanyId(code, pageAbleResult, cancellationToken);
            return result;
        }


        #endregion

        [HttpGet("{code}")]
        public async Task<PolicyRequestViewModel> GetPolicyRequestByCode(Guid code, CancellationToken cancellationToken)
        {
            var result = await _policyRequestService.getPolicyRequestByCode(code, cancellationToken);
            return result;
        }


        [HttpPatch("{code}/status")]
        public async Task<ApiResult<PolicyRequestSummaryOutputViewModel>> PlicyRequestStatusChange([FromRoute] Guid code , [FromBody] PolicyReqiestStatusInputViewModel _policyReqiestStatusInputViewModel, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User.Identity.GetUserId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }
            long roleId = long.Parse(userRole);
            return await _policyRequestService.PlicyRequestStatusChange(code, roleId, UserID, _policyReqiestStatusInputViewModel, cancellationToken);
        }






        #region Body

        [HttpPut("{code}/BodySupplement")] 
        public async Task<ApiResult<BodySupplementInfoViewModel>> AddOrUpdateBodySupplementIssue(string code, BodySupplementInfoViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());
            var result = await _policyRequestService.AddOrUpdateBodySupplement(code,userId, viewModel, cancellationToken);
            // return result;
            return result;
        }

        [HttpGet("{code}/BodySupplement")]
        public async Task<ApiResult<BodySupplementInfoViewModel>> GetBodySupplementIssue(string code, CancellationToken cancellationToken)
        {
            BodySupplementInfoViewModel result = await _policyRequestService.GetBodySupplement(code, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }




        [HttpPut("mine/{code}/BodySupplement")]
        public async Task<ApiResult<BodySupplementInfoViewModel>> AddOrUpdateBodySupplementIssueMine(string code, BodySupplementInfoViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());


            var result = await _policyRequestService.AddOrUpdateBodySupplementMine(code, userId, viewModel, cancellationToken);
            // return result;
            return result;
        }

        [HttpGet("mine/{code}/BodySupplement")]
        public async Task<ApiResult<BodySupplementInfoViewModel>> GetBodySupplementIssueMine(string code, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());


            BodySupplementInfoViewModel result = await _policyRequestService.GetBodySupplementMine(userId, code, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }







        [HttpPut("{code}/Inspection")]
        public async Task<ApiResult<PolicyRequestInspectionResultViewModel>> AddOrUpdatePolicyRequestInspection(string code, PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken)
        {
            PolicyRequestInspectionResultViewModel result = await _policyRequestService.CreateOrUpdatePolicyRequestHolderInspectionAsync(code, viewModel, cancellationToken);

            return result;
        }

        [HttpGet("{code}/Inspection")]
        public async Task<ApiResult<PolicyRequestInspectionResultViewModel>> GetPolicyRequestInspection(string code, CancellationToken cancellationToken)
        {
            PolicyRequestInspectionResultViewModel result = await _policyRequestService.GetPolicyRequestHolderInspectionAsync(code, cancellationToken);
            return result;
        }

        [HttpPut("mine/{code}/Inspection")]
        public async Task<ApiResult<PolicyRequestInspectionResultViewModel>> AddOrUpdatePolicyRequestInspectionMine(string code, PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());


            PolicyRequestInspectionResultViewModel result = await _policyRequestService.CreateOrUpdatePolicyRequestHolderInspectionAsyncMine(userId, code, viewModel, cancellationToken);

            return result;
        }

        [HttpGet("mine/{code}/Inspection")]
        public async Task<ApiResult<PolicyRequestInspectionResultViewModel>> GetPolicyRequestInspectionMine(string code, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());


            PolicyRequestInspectionResultViewModel result = await _policyRequestService.GetPolicyRequestHolderInspectionAsyncMine(userId, code, cancellationToken);
            return result;
        }






        //[HttpGet("{code}/BodyPaymentInfo")]
        //public async Task<PolicyRequestPaymentViewModel> GetBodyRequestPaymentInfo([FromRoute] Guid code, CancellationToken cancellationToken)
        //{
        //    var result = await _bodyRequestService.GetPolicyRequestPaymentDetails(code, cancellationToken);
        //    return result;
        //}
        //[HttpGet("mine/{code}/BodyPaymentInfo")]
        //public async Task<PolicyRequestPaymentViewModel> GetBodyRequestPaymentInfoMine([FromRoute] Guid code, CancellationToken cancellationToken)
        //{
        //    long userId = long.Parse(HttpContext.User.Identity.GetUserId());


        //    var result = await _bodyRequestService.GetPolicyRequestPaymentDetailsMine(userId, code, cancellationToken);
        //    return result;
        //}



        #endregion




        #region Policy Request Comment
        [HttpPost("{code}/comment")]
        public async Task<ApiResult<PolicyRequestCommentGetAllOutputViewModel>> Create([FromRoute] Guid code, [FromBody] PolicyRequestCommentInputViewModel _PolicyRequestCommentInputViewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());
            

            var result = await _policyRequestCommentService.Create(userId, code, _PolicyRequestCommentInputViewModel , cancellationToken);
            return result;
        }

        [HttpGet("{code}/comment")]
        public async Task<ApiResult<List<PolicyRequestCommentGetAllOutputViewModel>>> GetAllWithoutPaging([FromRoute] Guid code, CancellationToken cancellationToken)
        {
            return await _policyRequestCommentService.GetAllWithoutPaging(code, cancellationToken);
        }

        #endregion


        #region PolicyRequest AgentSelect

        [HttpGet("{code}/AgentSelect")]
        public async Task<PolicyRequestAgetSelectGetViewModel> PolicyRequestAgentSelect(Guid code, CancellationToken cancellationToken)
        {
            var result = await _policyRequestService.GetPolicyRequestAgentSelect(code, cancellationToken);
            return result;
        }
        [HttpPut("{code}/AgentSelect")]
        public async Task<PolicyRequestAgetSelectUpdateOutputViewModel> PolicyRequestAgentSelectUpdate(Guid code , [FromBody] PolicyRequestAgetSelectUpdateInputViewModel PolicyRequestAgetSelectUpdate, CancellationToken cancellationToken)
        {
            var result = await _policyRequestService.PolicyRequestAgentSelectUpdate(code, PolicyRequestAgetSelectUpdate, cancellationToken);
            return result;
        }

        #endregion
    }
}