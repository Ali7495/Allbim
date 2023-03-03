using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Models.PageAble;
using Models.Policy;
using Models.PolicyRequest;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Insurance;
using Models.Product;
using Models.QueryParams;
using Services.PipeLine;
using System;
using Models.PolicyRequestIssue;
using Models.PolicyRequestSupplement;
using Models.PolicyRequestInspection;
using Models.PolicyRequestPaymentInfo;
using Models.BodySupplementInfo;

namespace Services.PolicyRequest
{
    public interface IPolicyRequestService
    {
        public Task<Pipe<ThirdProductInputViewModel, OutputViewModel>> RunThirdPipeLine(ProductInsuranceViewModel insurer, OutputViewModel outPut, List<string> StepNames,
            ThirdProductInputViewModel thirdProductInputViewModel,
            CancellationToken cancellationToken);
        public Task<Pipe<BodyProductInputViewModel, OutputViewModel>> RunBodyPipeLine(ProductInsuranceViewModel insurer, OutputViewModel outPut, List<string> StepNames,
                BodyProductInputViewModel bodyProductInputViewModel,
                CancellationToken cancellationToken);

        public Task<List<ThirdInsuranceResultViewModel>> GetAvailableThirdInsuranceInsurers(
            ThirdProductInputViewModel thirdProductInputViewModel,
            CancellationToken cancellationToken);

        public Task<List<BodyInsuranceResultViewModel>> GetAvailableBodyInsurers(string slug,
            BodyProductInputViewModel bodyProductInputViewModel,
            CancellationToken cancellationToken);



        #region Create

        Task<PolicyRequestSummaryViewModel> Create(PolicyRequestInputViewModel viewModel, long userId,
            CancellationToken cancellationToken);

        Task<PolicyRequestViewModel> CreatePolicyRequestAsync(PolicyRequestViewModel policyViewModel,
            CancellationToken cancellationToken);

        Task<PolicyRequestDetailViewModel> CreatePolicyRequestDetailAsync(
            PolicyRequestDetailViewModel policyDetailViewModel, CancellationToken cancellationToken);


        Task<PolicyRequestSummaryViewModel> CreatePolicyRequestMine(PolicyRequestInputViewModel viewModel, long userId,
                    CancellationToken cancellationToken);


        #endregion

        #region Delete

        Task<bool> DeletePolicyRequestAsync(long id, CancellationToken cancellationToken);
        Task<bool> DeletePolicyRequestDetailAsync(long id, CancellationToken cancellationToken);



        #endregion

        #region Get

        Task<PolicyRequestViewModel> GetEveryThing(Guid code, CancellationToken cancellationToken);

        //Task<PagedResult<PolicyRequestViewModel>> GetAllPolicyRequestsAsync(long userId, long roleId, PageAbleResult pageAbleResult,
        //    CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequestViewModel>> GetAllPolicyRequestsAsync(long userId, long roleId,List<Guid> companyCodes, PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<PagedResult<PolicyRequestViewModel>> GetAllPolicyRequestsAsyncBySlug(string slug, long userId, long roleId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequestDetailViewModel>> GetAllPolicyRequestDetailsAsync(PageAbleResult pageAbleResult,
            CancellationToken cancellationToken);

        Task<PolicyRequestViewModel> GetPolicyRequestAsync(long id, CancellationToken cancellationToken);
        Task<List<MyPolicyRequestViewModel>> GetMinePolicyInsurance(long userId, CancellationToken cancellationToken);

        Task<PolicyRequestDetailViewModel> GetPolicyRequestDetailAsync(long id, CancellationToken cancellationToken);


        Task<PolicyRequestPaymentViewModel> GetPolicyRequestPaymentDetails(Guid code, CancellationToken cancellationToken);
        Task<PolicyRequestPaymentViewModel> GetPolicyRequestPaymentDetailsMine(long userId, Guid code, CancellationToken cancellationToken);

        Task<List<PolicyRequestByProvinceViewModel>> GetPolicyRequestBasedOnProvince(long provinceId, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequestViewModel>> GetAllPolicyRequestsAsyncMine(long userId, long roleId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<PolicyRequestViewModel> GetPolicyRequestByCodeMine(long userId, Guid code, CancellationToken cancellationToken);

        Task<PolicyRequestViewModel> UpdateRequestMine(long userId, Guid code, PolicyRequestViewModel viewModel,
            CancellationToken cancellationToken);

        //Task<bool> DeleteMyRequest(Guid code, CancellationToken cancellationToken);
        #endregion

        #region Update

        Task<PolicyRequestViewModel> UpdatePolicyRequestAsync(long id, PolicyRequestViewModel policyRequestViewModel,
            CancellationToken cancellationToken);

        Task<PolicyRequestDetailViewModel> UpdatePolicyRequestDetailAsync(long id,
            PolicyRequestDetailViewModel policyRequestDetailViewModel, CancellationToken cancellationToken);



        Task<PagedResult<PolicyRequestHolderViewModel>> GetAllPolicyRequestHolderAsync(PageAbleResult pageAbleResult,
            CancellationToken cancellationToken);

        Task<bool> Delete(long id, CancellationToken cancellationToken);

        Task<PolicyRequestHolderViewModel> CreatePolicyRequestHolderAsync(PolicyRequestHolderViewModel viewModel,
            CancellationToken cancellationToken);

        Task<PolicyRequestViewModel> Update(long id, PolicyRequestViewModel policyViewModel,
            CancellationToken cancellationToken);

        Task<bool> DeletePolicyRequestHolderAsync(long id, CancellationToken cancellationToken);

        Task<PolicyRequestHolderViewModel> UpdatePolicyRequestHolderAsync(long id,
            PolicyRequestHolderViewModel viewModel, CancellationToken cancellationToken);

        Task<PolicyRequestViewModel> getPolicyRequestByCode(Guid Code, CancellationToken cancellationToken);
        Task<PolicyRequestHolderViewModel> GetPolicyRequestHolderAsync(long id, CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequestViewModel>> GetAll(PageAbleResult pageAbleResult,
            CancellationToken cancellationToken);

        Task<PolicyRequestHolderCompanyViewModel> CreatePolicyRequestHolderCompanyAsync(
            PolicyRequestHolderCompanyViewModel ViewModel, CancellationToken cancellationToken);

        Task<bool> DeletePolicyRequestHolderCompanyAsync(long id, CancellationToken cancellationToken);

        Task<PolicyRequestHolderCompanyViewModel> UpdatePolicyRequestHolderCompanyAsync(long id,
            PolicyRequestHolderCompanyViewModel policyRequestDetailViewModel, CancellationToken cancellationToken);

        Task<PolicyRequestHolderCompanyViewModel> GetPolicyRequestHolderCompanyAsync(long id,
            CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequestHolderCompanyViewModel>> GetAllPolicyRequestHolderCompanyAsync(
            PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<PolicyRequestHolderPersonViewModel> CreatePolicyRequestHolderPersonAsync(
            PolicyRequestHolderPersonViewModel viewModel, CancellationToken cancellationToken);

        Task<bool> DeletePolicyRequestHolderPersonAsync(long id, CancellationToken cancellationToken);

        Task<PolicyRequestHolderPersonViewModel> UpdatePolicyRequestHolderPersonAsync(long id,
            PolicyRequestHolderPersonViewModel viewModel, CancellationToken cancellationToken);

        Task<PolicyRequestHolderPersonViewModel> GetPolicyRequestHolderPersonAsync(long id,
            CancellationToken cancellationToken);

        Task<PagedResult<PolicyRequestHolderPersonViewModel>> GetAllPolicyRequestHolderPersonAsync(
            PageAbleResult pageAbleResult, CancellationToken cancellationToken);


        #endregion


        #region Attachment

        Task<PolicyRequestAttachmentViewModel> CreatePolicyRequestAttachment(CancellationToken cancellationToken, IFormFile file,
            string policyCode, int typeId);


        Task<List<PolicyRequestAttachmentDownloadViewModel>> GetPolicyRequestAttachments(string code,
            CancellationToken cancellationToken);


        Task<PolicyRequestAttachmentViewModel> UpdatePolicyRequestAttachmentAsync(long id,
            PolicyRequestAttachmentViewModel viewModel, CancellationToken cancellationToken);

        Task<PolicyRequestAttachmentViewModel> CreatePolicyRequestAttachmentAsync(
            PolicyRequestAttachmentViewModel viewModel, CancellationToken cancellationToken);
        #endregion

        #region Supplement

        Task<Person> addOrUpdatePolicyRequestHolderPerson(PolicySupplementPersonViewModel personViewModel,
            CancellationToken cancellationToken);

        Task<Address> addOrUpdatePolicyRequestHolderAddress(
            PolicyRequestHolderPersonAddressViewModel personAddressViewModel, CancellationToken cancellationToken);

        Task<PolicySupplementViewModel> CreatePolicyRequestHolderSupplementInfoAsync(string code,
            PolicySupplementViewModel viewModel, CancellationToken cancellationToken);

        Task<PolicySupplementViewModel> GetPolicyRequestHolderSupplementInfo(string code,
            CancellationToken cancellationToken);



        Task<PolicySupplementViewModel> CreatePolicyRequestHolderSupplementInfoAsyncMine(long userId, Guid code,
            PolicySupplementViewModel viewModel, CancellationToken cancellationToken);

        Task<PolicySupplementViewModel> GetPolicyRequestHolderSupplementInfoMine(long userId, string code,
                    CancellationToken cancellationToken);
        #endregion


        #region Issue

        Task<PolicyRequestIssueViewModel> CreateOrUpdatePolicyRequestHolderIssueAsync(string code,
            PolicyRequestIssueInputViewModel viewModel, CancellationToken cancellationToken);

        Task<PolicyRequestIssueViewModel> GetPolicyRequestHolderIssueAsync(string code,
            CancellationToken cancellationToken);


        Task<PolicyRequestIssueViewModel> CreateOrUpdatePolicyRequestHolderIssueAsyncMine(long userId, string code,
            PolicyRequestIssueInputViewModel viewModel, CancellationToken cancellationToken);
        Task<PolicyRequestIssueViewModel> GetPolicyRequestHolderIssueAsyncMine(long userId, string code,
            CancellationToken cancellationToken);

        #endregion


        #region Inspection

        Task<PolicyRequestInspectionResultViewModel> CreateOrUpdatePolicyRequestHolderInspectionAsync(string code,
            PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken);

        Task<PolicyRequestInspectionResultViewModel> GetPolicyRequestHolderInspectionAsync(string code,
            CancellationToken cancellationToken);



        Task<PolicyRequestInspectionResultViewModel> CreateOrUpdatePolicyRequestHolderInspectionAsyncMine(long userId, string code,
            PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken);
        Task<PolicyRequestInspectionResultViewModel> GetPolicyRequestHolderInspectionAsyncMine(long userId, string code,
            CancellationToken cancellationToken);

        #endregion



        #region PolicyRequest

        Task<PagedResult<MyPolicyRequestViewModel>> GetAllByCompanyId(Guid code, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<PolicyRequestSummaryOutputViewModel> PlicyRequestStatusChange(Guid code, long RoleID, long UserID, PolicyReqiestStatusInputViewModel _policyReqiestStatusInputViewModel, CancellationToken cancellationToken);


        #endregion

        #region BodySupplement

        Task<BodySupplementInfoViewModel> AddOrUpdateBodySupplement(string code, long AuthenticatedUserId, BodySupplementInfoViewModel viewModel, CancellationToken cancellationToken);
        Task<BodySupplementInfoViewModel> AddOrUpdateBodySupplementMine(string code, long userId, BodySupplementInfoViewModel viewModel, CancellationToken cancellationToken);


        Task<BodySupplementInfoViewModel> GetBodySupplement(string code,
            CancellationToken cancellationToken);

        Task<BodySupplementInfoViewModel> GetBodySupplementMine(long userId, string code,
            CancellationToken cancellationToken);

        #endregion


        #region PolicyRequestAgentSelect
        Task<PolicyRequestAgetSelectGetViewModel> GetPolicyRequestAgentSelect(Guid code, CancellationToken cancellationToken);
        Task<PolicyRequestAgetSelectUpdateOutputViewModel> PolicyRequestAgentSelectUpdate(Guid code, PolicyRequestAgetSelectUpdateInputViewModel PolicyRequestAgetSelectUpdate, CancellationToken cancellationToken);

        #endregion
    }
}