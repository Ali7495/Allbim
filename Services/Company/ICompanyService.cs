using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using DAL.Models;
using Models.BodySupplementInfo;
using Models.Company;
using Models.CompanyComission;
using Models.CompanyComment;
using Models.CompanyFactor;
using Models.CompanyPolicyRequest;
using Models.CompanyPolicySuplement;
using Models.CompanyUser;
using Models.Customer;
using Models.PageAble;
using Models.Person;
using Models.PersonCompany;
using Models.PolicyRequest;
using Models.PolicyRequestCommet;
using Models.PolicyRequestInspection;
using Models.PolicyRequestIssue;
using Models.PolicyRequestSupplement;
using Models.User;
using Services.ViewModels;

namespace Services
{
    public interface ICompanyService
    {
        Task<CompanyDetailViewModel> create(CompanyInputViewModel companyViewModel, CancellationToken cancellationToken);
        Task<CompanyDetailViewModel> update(string code, CompanyInputViewModel companyViewModel, CancellationToken cancellationToken);
        Task<PersonCompanyDTOViewModel> UpdatePersonCompany(Guid companyCode, Guid personCode, PersonCompanyInputViewModel personCompanyInputViewModel, CancellationToken cancellationToken);
        Task<PersonCompanyDTOViewModel> UpdatePersonCompanyMine(Guid companyCode, long UserID, PersonCompanyInputViewModel personCompanyInputViewModel, CancellationToken cancellationToken);
        Task<CompanyDetailViewModel> updateMine(long UserID, CompanyInputViewModel companyViewModel, CancellationToken cancellationToken);
        Task<bool> delete(string code, CancellationToken cancellationToken);
        Task<bool> DeletePersonCompany(Guid companyCode, Guid personCode, CancellationToken cancellationToken);
        Task<bool> DeletePersonCompanyMine(Guid companyCode, long UserID, CancellationToken cancellationToken);
        Task<CompanyDetailViewModel> detail(string code, CancellationToken cancellationToken);
        Task<CompanyDetailViewModel> detailMine(long UserID, CancellationToken cancellationToken);
        Task<PagedResult<CompanyViewModel>> all(int? page, int? pageSize, CancellationToken cancellationToken);

        Task<List<CompanyViewModel>> allWithoutPaging(
            CancellationToken cancellationToken);

        Task<PagedResult<CustomerViewModel>> GetAllCustomersMine(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<PagedResult<CustomerViewModel>> GetAllCustomersOfCompany(Guid code, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<PagedResult<CompanyPolicyRequestViewModel>> GetCompanyPolicyReqeusts(Guid code, PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<CompanyPolicyRequestViewModel> GetCompanyPolicyReqeust(Guid code, Guid policyCode, CancellationToken cancellationToken);

        Task<PagedResult<CompanyPolicyRequestViewModel>> GetCompanyPolicyReqeustsMine(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<CompanyPolicyRequestViewModel> GetCompanyPolicyReqeustMine(long userId, Guid policyCode, CancellationToken cancellationToken);

        Task<PolicySupplementViewModel> CreateCompanyPolicyRequestHolderSupplementInfoAsyncMine(long userId, Guid policyCode, PolicySupplementViewModel viewModel, CancellationToken cancellationToken);
        Task<PolicySupplementViewModel> CreateCompanyPolicyRequestHolderSupplementInfoAsync(Guid code, Guid policyCode, PolicySupplementViewModel viewModel, CancellationToken cancellationToken);





        Task<PolicySupplementViewModel> GetCompanyPolicyRequestHolderSupplementInfoMine(long userId, Guid policyCode,
           CancellationToken cancellationToken);
        Task<PolicySupplementViewModel> GetCompanyPolicyRequestHolderSupplementInfo(Guid code, Guid policyCode,
                   CancellationToken cancellationToken);


        Task<CompanyBodySupplementInfoViewModel> AddOrUpdateCompanyBodySupplementMine(Guid code, long userId,
            CompanyBodySupplementInfoViewModel viewModel, CancellationToken cancellationToken);

        Task<CompanyBodySupplementInfoViewModel> AddOrUpdateCompanyBodySupplement(Guid policyCode, Guid code,
                    CompanyBodySupplementInfoViewModel viewModel, CancellationToken cancellationToken);

        Task<BodySupplementInfoViewModel> GetCompanyBodySupplementMine(long userId, Guid policyCode,
            CancellationToken cancellationToken);
        Task<BodySupplementInfoViewModel> GetCompanyBodySupplement(Guid code, Guid policyCode,
                    CancellationToken cancellationToken);


        Task<PolicyRequestIssueViewModel> CreateOrUpdateCompanyPolicyRequestHolderIssueAsyncMine(long userId, Guid policyCode, PolicyRequestIssueInputViewModel viewModel, CancellationToken cancellationToken);
        Task<PolicyRequestIssueViewModel> CreateOrUpdateCompanyPolicyRequestHolderIssueAsync(Guid code, Guid policyCode, PolicyRequestIssueInputViewModel viewModel, CancellationToken cancellationToken);
        Task<PolicyRequestIssueViewModel> GetCompanyPolicyRequestHolderIssueAsyncMine(long userId, Guid code, CancellationToken cancellationToken);

        Task<PolicyRequestPaymentViewModel> GetCompanyPolicyRequestPaymentDetailsMine(long userId, Guid policyCode,
            CancellationToken cancellationToken);

        Task<PolicyRequestInspectionResultViewModel> CreateOrUpdateCompanyPolicyRequestHolderInspectionAsyncMine(long userId,
            Guid policyCode, PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken);
        Task<PolicyRequestInspectionResultViewModel> CreateOrUpdateCompanyPolicyRequestHolderInspectionAsync(Guid code,
            Guid policyCode, PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken);


        Task<PolicyRequestInspectionResultViewModel> GetCompanyPolicyRequestHolderInspectionAsyncMine(long userId, Guid policyCode,
           CancellationToken cancellationToken);

        Task<PolicyRequestInspectionResultViewModel> GetCompanyPolicyRequestHolderInspectionAsync(Guid code, Guid policyCode,
           CancellationToken cancellationToken);

        Task<PolicyRequestPaymentViewModel> GetCompanyPolicyRequestBodyPaymentDetailsMine(long userId, Guid policyCode,
            CancellationToken cancellationToken);

        Task<PolicyRequestPaymentViewModel> GetCompanyPolicyRequestBodyPaymentDetails(Guid code, Guid policyCode,
                    CancellationToken cancellationToken);

        Task<PolicyRequestMineViewModel> GetCompanyPolicyDetailsMine(long userId, Guid policyCode, CancellationToken cancellationToken);

        Task<PolicyRequestMineViewModel> GetCompanyPolicyDetails(Guid code, Guid policyCode, CancellationToken cancellationToken);


        Task<PolicyRequestAgetSelectGetViewModel> GetCompanyPolicyRequestAgentSelectMine(long userId, Guid policyCode, CancellationToken cancellationToken);
        Task<PolicyRequestAgetSelectGetViewModel> GetCompanyPolicyRequestAgentSelect(Guid code, Guid policyCode, CancellationToken cancellationToken);

        Task<PolicyRequestAgetSelectUpdateOutputViewModel> CompanyPolicyRequestAgentSelectUpdateMine(long userId, Guid policyCode,
            PolicyRequestAgetSelectUpdateInputViewModel PolicyRequestAgetSelectUpdate,
            CancellationToken cancellationToken);
        Task<PolicyRequestAgetSelectUpdateOutputViewModel> CompanyPolicyRequestAgentSelectUpdate(Guid code, Guid policyCode,
            PolicyRequestAgetSelectUpdateInputViewModel PolicyRequestAgetSelectUpdate,
            CancellationToken cancellationToken);

        Task<List<PolicyRequestAttachmentDownloadViewModel>> GetCompanyPolicyRequestAttachmentsMine(long userId, Guid policyCode, CancellationToken cancellationToken);
        Task<List<PolicyRequestAttachmentDownloadViewModel>> GetCompanyPolicyRequestAttachments(Guid code, Guid policyCode, CancellationToken cancellationToken);

        Task<PagedResult<CompanyPolicyRequestViewModel>> GetCompanyPolicyReqeustsByStatusMine(long userId, long roleId, string status, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<PagedResult<CompanyUserResultViewModel>> GetCompanyUsers(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<UpdatedUserResultViewModel> UpdateCompanyUser(long userId, Guid userCode, CompanyUserUpdateInputViewModel viewModel, CancellationToken cancellationToken);

        Task<bool> DeleteCompanyUser(long userId, Guid userCode, CancellationToken cancellationToken);

        Task<CompanySingleUserResultViewModel> GetCompanyUser(long userId, Guid UserCode, CancellationToken cancellationToken);

        Task<string> ChangeUserPassword(long userId, Guid UserCode, UserChangePasswordViewModel viewModel, CancellationToken cancellationToken);

        Task<PagedResult<PersonResultViewModel>> GetAllPersonsWithoutUser(long userId, string search_text, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<UserResultViewModel> CreateCompanyUser(long userId, UserInputViewModel userInputViewModel, CancellationToken cancellationToken);


        Task<PolicyRequestCommentGetAllOutputViewModel> CreateCompanyComment(long userId, Guid companyCode, Guid policyCode, CompanyCommentInputViewModel viewModel, CancellationToken cancellationToken);

        Task<List<PolicyRequestCommentGetAllOutputViewModel>> GetAllPolicyComments(Guid companyCode, Guid policyCode, CancellationToken cancellationToken);

        Task<PolicyRequestCommentGetAllOutputViewModel> CreateCompanyCommentMine(long userId, Guid policyCode, CompanyCommentInputMineViewModel viewModel, CancellationToken cancellationToken);

        Task<List<PolicyRequestCommentGetAllOutputViewModel>> GetAllPolicyCommentsMine(long userId, Guid policyCode, CancellationToken cancellationToken);

        Task<CompanyComissionResultViewModel> UpdateCompanyPolicyComission(long userId, Guid policyCode, CompanyComissionInputViewModel viewModel, CancellationToken cancellationToken);

        Task<PolicyRequestSummaryOutputViewModel> CompanyPlicyRequestStatusChange(Guid code, Guid policyCode, long RoleID,
            long UserID, PolicyReqiestStatusInputViewModel _policyReqiestStatusInputViewModel,
            CancellationToken cancellationToken);

        Task<PolicyRequestSummaryOutputViewModel> CompanyPlicyRequestStatusChangeMine(Guid policyCode,
                    long UserID, PolicyReqiestStatusInputViewModel _policyReqiestStatusInputViewModel,
                    CancellationToken cancellationToken);



        Task<PagedResult<CompanyPolicyRequestViewModel>> GetCompanyPolicyReqeustsByStatus(Guid code, string status, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<PolicyRequestIssueViewModel> GetCompanyPolicyRequestHolderIssueAsync(Guid code, Guid policyCode, CancellationToken cancellationToken);

        Task<PolicyRequestPaymentViewModel> GetCompanyPolicyRequestPaymentDetails(Guid code, Guid policyCode,
            CancellationToken cancellationToken);

        Task<PagedResult<CompanyPolicyRequestFactorResultViewModel>> GetAllFactors(Guid code, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<PagedResult<CompanyPolicyRequestFactorResultViewModel>> GetAllFactorsMine(long userId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<PagedResult<CompanyPolicyRequestFactorResultViewModel>> GetAllPolicyFactors(Guid code, Guid policyCode, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<PagedResult<CompanyPolicyRequestFactorResultViewModel>> GetAllPolicyFactorsMine(long userId, Guid policyCode, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

        Task<CompanyPolicyRequestFactorResultViewModel> GetCompanyPolicyFactor(Guid code, Guid policyCode, long factorId, CancellationToken cancellationToken);

        Task<CompanyPolicyRequestFactorResultViewModel> GetCompanyPolicyFactorMine(long userId, Guid policyCode, long factorId, CancellationToken cancellationToken);

        Task<bool> DeleteFactor(Guid code, Guid policyCode, long factorId, CancellationToken cancellationToken);
        Task<bool> DeleteFactorDetail(Guid code, Guid policyCode, long factorId, long detailId, CancellationToken cancellationToken);
        Task<bool> DeleteFactorDetailMine(long userId, Guid policyCode, long factorId, long detailId, CancellationToken cancellationToken);
        Task<bool> DeleteFactorMine(long userId, Guid policyCode, long factorId, CancellationToken cancellationToken);


        Task<CompanyPolicyRequestFactorResultViewModel> CreatePaymentFactor(Guid code, Guid policyCode, CompanyPolicyFactorInputViewModel inputViewModel, CancellationToken cancellationToken);

        Task<CompanyFactorDetailResultViewModel> CreatePaymentFactorDetail(Guid code, Guid policyCode, long factorId, CompanyFactorDetailInputViewModel inputViewModel, CancellationToken cancellationToken);

        Task<CompanyFactorDetailResultViewModel> UpdatePaymentFactorDetail(Guid code, Guid policyCode, long factorId, long detailId, CompanyFactorDetailInputViewModel inputViewModel, CancellationToken cancellationToken);
        Task<CompanyFactorDetailResultViewModel> UpdatePaymentFactorDetailMine(long userId, Guid policyCode, long factorId, long detailId, CompanyFactorDetailInputViewModel inputViewModel, CancellationToken cancellationToken);

        Task<CompanyPolicyRequestFactorResultViewModel> CreatePaymentFactorMine(long userId, Guid policyCode, CompanyPolicyFactorInputViewModel inputViewModel, CancellationToken cancellationToken);

        Task<CompanyFactorDetailResultViewModel> CreatePaymentFactorDetailMine(long userId, Guid policyCode, long factorId, CompanyFactorDetailInputViewModel inputViewModel, CancellationToken cancellationToken);
        Task<PagedResult<CompanyFactorDetailResultViewModel>> GetAllPolicyFactorDetials(Guid code, Guid policyCode, long factorId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<PagedResult<CompanyFactorDetailResultViewModel>> GetAllPolicyFactorDetialsMine(long userId, Guid policyCode, long factorId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);
        Task<CompanyFactorDetailResultViewModel> GetCompanyFactorDetail(Guid code, Guid policyCode, long factorId, long detailId, CancellationToken cancellationToken);
        Task<CompanyFactorDetailResultViewModel> GetCompanyFactorDetailMine(long userId, Guid policyCode, long factorId, long detailId, CancellationToken cancellationToken);
        Task<PagedResult<CompanyFactorViewModel>> GetAllPaymentsMine(long userId, long? statusId, PageAbleResult pageAbleResult, CancellationToken cancellationToken);

    }
}
