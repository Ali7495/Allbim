using System.Collections.Generic;
using albim.Result;
using Common.Utilities;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.Company;
using Models.Insurance;
using Models.PageAble;
using Services;
using System.Threading;
using System.Threading.Tasks;
using Models.Agent;
using System;
using Services.Agent;
using Models.InsurerTerm;
using Models.CompanyCenter;
using Services.CompanyCenter;
using Models.Insurer;
using Services.InsurerServices;
using Models.Center;
using Models.CompanyCenterSchedule;
using Common.Extensions;
using System.Linq;
using Common.Exceptions;
using System.Security.Claims;
using Models.Customer;
using Models.PersonCompany;
using Models.CompanyPolicyRequest;
using Models.InsurerTernDetail;
using Services.InsurerTermDetailServices;
using Models.PolicyRequestSupplement;
using Models.BodySupplementInfo;
using Models.PolicyRequestIssue;
using Models.PolicyRequest;
using Models.PolicyRequestInspection;
using Models.Person;
using Models.CompanyUser;
using Models.User;
using Models.CompanyComment;
using Models.CompanyAgent;
using Models.CompanyComission;
using Models.PolicyRequestCommet;
using Models.CompanyPolicySuplement;
using Models.CompanyFactor;

namespace albim.Controllers.v1
{

    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]

    public class CompanyController : BaseController
    {
        #region Property
        private readonly ICompanyService _companyService;
        private readonly IInsuranceService _insuranceService;
        private readonly IInsurerTermService _insurerTermService;
        private readonly IInsurerServices _insurerServices;
        private readonly IAgentService _agentService;
        private readonly ICompanyCenterServices _companyCenterServices;
        private readonly IInsurerTermDetailServices _insurerTermDetailServices;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public CompanyController(ICompanyService companyService, IInsuranceService insuranceService, IInsurerTermService insurerTermService, IAgentService agentService, ICompanyCenterServices companyCenterServices, IInsurerServices insurerServices, IInsurerTermDetailServices insurerTermDetailServices)
        {
            _companyService = companyService;
            _insuranceService = insuranceService;
            _insurerTermService = insurerTermService;
            _agentService = agentService;
            _companyCenterServices = companyCenterServices;
            _insurerServices = insurerServices;
            _insurerTermDetailServices = insurerTermDetailServices;
        }
        #endregion

        #region Company Actions
        /// <summary>
        /// To Create a User
        /// </summary>c
        /// <param name="companyViewModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost()]
        [AllowAnonymous]
        public async Task<ApiResult<CompanyDetailViewModel>> Create(CompanyInputViewModel companyViewModel, CancellationToken cancellationToken)
        {
            var company = await _companyService.create(companyViewModel, cancellationToken);
            return company;
        }


        [HttpGet("{code}")]
        [AllowAnonymous]
        public async Task<ApiResult<CompanyDetailViewModel>> GetDetail(string code, CancellationToken cancellationToken)
        {
            var company = await _companyService.detail(code, cancellationToken);
            return company;
        }

        [HttpGet()]
        [AllowAnonymous]
        public async Task<ApiResult<PagedResult<CompanyViewModel>>> GetAll([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var people = await _companyService.all(page, pageSize, cancellationToken);
            return people;
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<ApiResult<List<CompanyViewModel>>> GetAllWithoutPaging(CancellationToken cancellationToken)
        {
            var people = await _companyService.allWithoutPaging(cancellationToken);
            return people;
        }

        [HttpDelete("{code}")]
        public async Task<ApiResult<string>> Delete(string code, CancellationToken cancellationToken)
        {
            var res = await _companyService.delete(code, cancellationToken);
            return res.ToString();
        }


        [HttpPut("{code}")]
        public async Task<ApiResult<CompanyDetailViewModel>> UpdateCompany(string code, CompanyInputViewModel companyInputViewModel, CancellationToken cancellationToken)
        {
            var res = await _companyService.update(code, companyInputViewModel, cancellationToken);
            return res;
        }
        [HttpPut("{companyCode}/Person/{personCode}")]
        public async Task<ApiResult<PersonCompanyDTOViewModel>> UpdatePersonCompany(Guid companyCode, Guid personCode, [FromBody] PersonCompanyInputViewModel personCompanyInputViewModel, CancellationToken cancellationToken)
        {
            var res = await _companyService.UpdatePersonCompany(companyCode, personCode, personCompanyInputViewModel, cancellationToken);
            return res;
        }
        [HttpDelete("{companyCode}/Person/{personCode}")]
        public async Task<ApiResult<string>> DeletePersonCompany(Guid companyCode, Guid personCode, CancellationToken cancellationToken)
        {
            var res = await _companyService.DeletePersonCompany(companyCode, personCode, cancellationToken);
            return res.ToString();
        }

        #endregion
        #region Insurance Actions
        //
        // [HttpPost("{code}/Insurance")]
        // public async Task<ApiResult<InsuranceViewModel>> Create(InsuranceViewModel insuranceViewModel, CancellationToken cancellationToken)
        // {
        //     var insurance = await _insuranceService.CreateInsurance(insuranceViewModel, cancellationToken);
        //     return insurance;
        // }
        //
        // [HttpGet("{code}/Insurance/{id}")]
        // public async Task<ApiResult<InsuranceViewModel>> GetDetail(Guid code,long id, CancellationToken cancellationToken)
        // {
        //     var model = await _insuranceService.GetInsurance(id, cancellationToken);
        //     return model;
        // }

        //todo: باید براساس شرکت، بیمه هایی که دارد فیلتر شود

        // [HttpGet("{code}/Insurance")]
        // public async Task<ApiResult<List<InsuranceViewModel>>> GetAllInsurance(Guid code,[FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        // {
        //     var model = await _insuranceService.GetAllInsurances( cancellationToken);
        //     return model;
        // }
        //
        // [HttpDelete("{code}/Insurance/{id}")]
        // public async Task<ApiResult<string>> Delete(int id, CancellationToken cancellationToken)
        // {
        //     var res = await _insuranceService.DeleteInsurance(id, cancellationToken);
        //     return res.ToString();
        // }

        #endregion

        #region Insurer Actions

        [HttpPost("{companyCode}/insurance/{insuranceId}")]
        public async Task<ApiResult<InsurerResultViewModel>> CreateInsurer(Guid companyCode, long insuranceId, CancellationToken cancellationToken)
        {
            return await _insurerServices.CreateInsurer(companyCode, insuranceId, cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("{companyCode}/insurance/{insuranceId}")]
        public async Task<ApiResult<InsurerResultViewModel>> GetInsurer(Guid companyCode, long insuranceId, CancellationToken cancellationToken)
        {
            return await _insurerServices.GetInsurer(companyCode, insuranceId, cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("{companyCode}/insurance")]
        public async Task<ApiResult<PagedResult<InsurerResultViewModel>>> GetAllInsurers(Guid companyCode, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _insurerServices.GetInsurersByCompany(companyCode, pageAbleResult, cancellationToken);
        }

        [HttpDelete("{companyCode}/insurance/{insuranceId}")]
        public async Task<string> DeleteInsurer(Guid companyCode, long insuranceId, CancellationToken cancellationToken)
        {
            return await _insurerServices.DeleteInsurer(companyCode, insuranceId, cancellationToken);
        }


        #endregion


        #region Mine Insurer

        [HttpPost("mine/insurance/{insuranceId}")]
        public async Task<ApiResult<InsurerResultViewModel>> CreateInsurerMine(long insuranceId, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _insurerServices.CreateInsurerMine(userId, insuranceId, cancellationToken);
        }


        [HttpGet("mine/insurance/{insuranceId}")]
        public async Task<ApiResult<InsurerResultViewModel>> GetInsurerMine(long insuranceId, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _insurerServices.GetInsurerMine(userId, insuranceId, cancellationToken);
        }

        [HttpGet("mine/insurance")]
        public async Task<ApiResult<PagedResult<InsurerResultViewModel>>> GetAllInsurersMine([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _insurerServices.GetInsurersByCompanyMine(userId, pageAbleResult, cancellationToken);
        }

        [HttpDelete("mine/insurance/{insuranceId}")]
        public async Task<string> DeleteInsurer(long insuranceId, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _insurerServices.DeleteInsurerMine(userId, insuranceId, cancellationToken);
        }
        #endregion




        #region InsurerTerm Actions
        [HttpPost("{code}/insurance/{insuranceId}/term")]
        public async Task<ApiResult<InsurerTermDetailedResultViewModel>> CreateInsurerTerm(Guid code, long insuranceId, InsurerTermInputViewModel insurerTermViewModel, CancellationToken cancellationToken)
        {
            InsurerTermDetailedResultViewModel insurerTerm = await _insurerTermService.CreateInsurerTerm(code, insuranceId, insurerTermViewModel, cancellationToken);
            return insurerTerm;
        }
        [AllowAnonymous]
        [HttpGet("{code}/insurance/{insuranceId}/term/{id}")]
        public async Task<ApiResult<InsurerTermDetailedResultViewModel>> GetInsurerTerm(long id, CancellationToken cancellationToken)
        {
            InsurerTermDetailedResultViewModel insurerTerm = await _insurerTermService.GetInsurerTerm(id, cancellationToken);
            return insurerTerm;
        }
        [AllowAnonymous]
        [HttpGet("{code}/insurance/{insuranceId}/term")]
        public async Task<ApiResult<PagedResult<InsurerTermResultViewModel>>> GetInsurerTerms(Guid code, long insuranceId, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _insurerTermService.GetAllInsurerTerms(code, insuranceId, pageAbleResult, cancellationToken);
        }

        [HttpDelete("{code}/insurance/{insuranceId}/term/{id}")]
        public async Task<ApiResult<string>> DeleteInsurerTerm(int id, CancellationToken cancellationToken)
        {
            bool res = await _insurerTermService.DeleteInsurerTerm(id, cancellationToken);
            return res.ToString();
        }

        [HttpPut("{code}/insurance/{insuranceId}/term/{id}")]
        public async Task<ApiResult<InsurerTermDetailedResultViewModel>> UpdateInsurerTerm(Guid code, long insuranceId, long id, InsurerTermInputViewModel insurerTermViewModel, CancellationToken cancellationToken)
        {
            InsurerTermDetailedResultViewModel result = await _insurerTermService.UpdateInsurerTermAsync(code, insuranceId, id, insurerTermViewModel, cancellationToken);
            return result;
        }
        #endregion

        #region InsurerTermDetial

        [HttpPost("{code}/insurance/{insuranceId}/term/{insurerTermId}/detail")]
        public async Task<ApiResult<TermDetailResultViewModel>> CreateInsurerTerm(Guid code, long insuranceId, long insurerTermId, TermDetailInputViewModel termDetailInputView, CancellationToken cancellationToken)
        {
            TermDetailResultViewModel termDetail = await _insurerTermDetailServices.CreateInsurerTermDetail(code, insuranceId, insurerTermId, termDetailInputView, cancellationToken);
            return termDetail;
        }
        [AllowAnonymous]
        [HttpGet("{code}/insurance/{insuranceId}/term/{insurerTermId}/detail")]
        public async Task<ApiResult<PagedResult<TermDetailResultViewModel>>> GetInsurerTermDetails(Guid code, long insuranceId, long insurerTermId, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _insurerTermDetailServices.GetAllInsurerTermDetails(code, insuranceId, insurerTermId, pageAbleResult, cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("{code}/insurance/{insuranceId}/term/{insurerTermId}/detail/list")]
        public async Task<ApiResult<List<TermDetailResultViewModel>>> GetInsurerTermDetailList(Guid code, long insuranceId, long insurerTermId, CancellationToken cancellationToken)
        {
            return await _insurerTermDetailServices.GetAllInsurerTermDetailList(code, insuranceId, insurerTermId, cancellationToken);


        }
        [AllowAnonymous]
        [HttpGet("{code}/insurance/{insuranceId}/term/{insurerTermId}/detail/{detailId}")]
        public async Task<ApiResult<TermDetailResultViewModel>> GetInsurerTermDetail(Guid code, long insuranceId, long insurerTermId, long detailId, CancellationToken cancellationToken)
        {
            return await _insurerTermDetailServices.GetInsurerTermDetail(code, insuranceId, insurerTermId, detailId, cancellationToken);
        }

        [HttpPut("{code}/insurance/{insuranceId}/term/{insurerTermId}/detail/{detailId}")]
        public async Task<ApiResult<TermDetailResultViewModel>> UpdateInsurerTermDetail(Guid code, long insuranceId, long insurerTermId, long detailId, TermDetailInputViewModel termDetailInputView, CancellationToken cancellationToken)
        {
            TermDetailResultViewModel result = await _insurerTermDetailServices.UpdateInsurerTermDetailAsync(code, insuranceId, insurerTermId, detailId, termDetailInputView, cancellationToken);
            return result;
        }

        [HttpDelete("{code}/insurance/{insuranceId}/term/{insurerTermId}/detail/{detailId}")]
        public async Task<bool> DeleteInsurerTermDetail(Guid code, long insuranceId, long insurerTermId, long detailId, CancellationToken cancellationToken)
        {
            bool result = await _insurerTermDetailServices.DeleteInsurerTermDetailAsync(code, insuranceId, insurerTermId, detailId, cancellationToken);
            return result;
        }
        #endregion


        #region Mine InsurerTerm 

        [HttpPost("mine/insurance/{insuranceId}/term")]
        public async Task<ApiResult<InsurerTermDetailedResultViewModel>> CreateInsurerTermMine(long insuranceId, InsurerTermInputViewModel insurerTermViewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            InsurerTermDetailedResultViewModel insurerTerm = await _insurerTermService.CreateInsurerTermMine(userId, insuranceId, insurerTermViewModel, cancellationToken);
            return insurerTerm;
        }

        [HttpGet("mine/insurance/{insuranceId}/term/{id}")]
        public async Task<ApiResult<InsurerTermDetailedResultViewModel>> GetInsurerTermDetailsMine(long insuranceId, long id, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            InsurerTermDetailedResultViewModel insurerTerm = await _insurerTermService.GetInsurerTermMine(userId, insuranceId, id, cancellationToken);
            return insurerTerm;
        }

        [HttpGet("mine/insurance/{insuranceId}/term")]
        public async Task<ApiResult<PagedResult<InsurerTermResultViewModel>>> GetInsurerTermsMine(long insuranceId, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _insurerTermService.GetAllInsurerTermsMine(userId, insuranceId, pageAbleResult, cancellationToken);
        }

        [HttpDelete("mine/insurance/{insuranceId}/term/{id}")]
        public async Task<ApiResult<string>> DeleteInsurerTermMine(long insuranceId, int id, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            bool res = await _insurerTermService.DeleteInsurerTermMine(userId, insuranceId, id, cancellationToken);
            return res.ToString();
        }

        [HttpPut("mine/insurance/{insuranceId}/term/{id}")]
        public async Task<ApiResult<InsurerTermDetailedResultViewModel>> UpdateInsurerTermMine(long insuranceId, long id, InsurerTermInputViewModel insurerTermViewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            InsurerTermDetailedResultViewModel result = await _insurerTermService.UpdateInsurerTermAsyncMine(userId, insuranceId, id, insurerTermViewModel, cancellationToken);
            return result;
        }
        #endregion


        #region Mine InsurerTermDetail

        [HttpPost("mine/insurance/{insuranceId}/term/{insurerTermId}/detail")]
        public async Task<ApiResult<TermDetailResultViewModel>> CreateInsurerTermMine(long insuranceId, long insurerTermId, TermDetailInputViewModel termDetailInputView, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            TermDetailResultViewModel termDetail = await _insurerTermDetailServices.CreateInsurerTermDetailMine(userId, insuranceId, insurerTermId, termDetailInputView, cancellationToken);
            return termDetail;
        }

        [HttpGet("mine/insurance/{insuranceId}/term/{insurerTermId}/detail")]
        public async Task<ApiResult<PagedResult<TermDetailResultViewModel>>> GetInsurerTermDetailsMine(long insuranceId, long insurerTermId, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _insurerTermDetailServices.GetAllInsurerTermDetailsMine(userId, insuranceId, insurerTermId, pageAbleResult, cancellationToken);
        }

        [HttpGet("mine/insurance/{insuranceId}/term/{insurerTermId}/detail/list")]
        public async Task<ApiResult<List<TermDetailResultViewModel>>> GetInsurerTermDetailListMine(long insuranceId, long insurerTermId, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _insurerTermDetailServices.GetAllInsurerTermDetailListMine(userId, insuranceId, insurerTermId, cancellationToken);
        }

        [HttpGet("mine/insurance/{insuranceId}/term/{insurerTermId}/detail/{detailId}")]
        public async Task<ApiResult<TermDetailResultViewModel>> GetInsurerTermDetail(long insuranceId, long insurerTermId, long detailId, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _insurerTermDetailServices.GetInsurerTermDetailMine(userId, insuranceId, insurerTermId, detailId, cancellationToken);
        }

        [HttpPut("mine/insurance/{insuranceId}/term/{insurerTermId}/detail/{detailId}")]
        public async Task<ApiResult<TermDetailResultViewModel>> UpdateInsurerTermDetailMine(long insuranceId, long insurerTermId, long detailId, TermDetailInputViewModel termDetailInputView, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            TermDetailResultViewModel result = await _insurerTermDetailServices.UpdateInsurerTermDetailAsyncMine(userId, insuranceId, insurerTermId, detailId, termDetailInputView, cancellationToken);
            return result;
        }

        [HttpDelete("mine/insurance/{insuranceId}/term/{insurerTermId}/detail/{detailId}")]
        public async Task<bool> DeleteInsurerTermDetailMine(long insuranceId, long insurerTermId, long detailId, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            bool result = await _insurerTermDetailServices.DeleteInsurerTermDetailAsyncMine(userId, insuranceId, insurerTermId, detailId, cancellationToken);
            return result;
        }

        #endregion


        #region Agent
        [AllowAnonymous]
        [HttpGet("{code}/agent")]
        public async Task<ApiResult<PagedResult<AgentViewModel>>> GetCompanyAgents(Guid code, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _agentService.GetAgents(code, pageAbleResult, cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("{code}/agent/list")]
        public async Task<ApiResult<List<AgentViewModel>>> GetAgentsList(Guid code, CancellationToken cancellationToken)
        {
            return await _agentService.GetAgentsList(code, cancellationToken);
        }

        [HttpPost("{code}/agent")]
        public async Task<ApiResult<AgentPersonViewModel>> CreateCompanyAgent(Guid code, CopmanyAgentAndPersonViewModel viewModel, CancellationToken cancellationToken)
        {
            return await _agentService.CreateAgent(code, viewModel, cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("{code}/agent/{personCode}")]
        public async Task<ApiResult<AgentViewModel>> GetCompanyAgent(Guid code, Guid personCode, CancellationToken cancellationToken)
        {
            return await _agentService.GetAgent(code, personCode, cancellationToken);
        }

        [HttpPut("{code}/agent/{personCode}")]
        public async Task<ApiResult<AgentPersonViewModel>> UpdateCompanyAgent(Guid code, Guid personCode, CopmanyAgentAndPersonViewModel viewModel, CancellationToken cancellationToken)
        {
            return await _agentService.UpdateAgent(code, personCode, viewModel, cancellationToken);
        }

        [HttpDelete("{code}/agent/{personCode}")]
        public async Task<ApiResult<string>> DeleteCompanyAgent(Guid code, Guid personCode, CancellationToken cancellationToken)
        {
            string result = (await _agentService.DeleteAgent(code, personCode, cancellationToken)).ToString();
            return result;
        }
        #endregion


        #region MyAgent


        [HttpGet("mine/person")]
        public async Task<ApiResult<PagedResult<PersonResultWithAgentCompanyViewModel>>> GetCompanyAgentPersonsMine([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            return await _agentService.GetAgentsMine(userId, pageAbleResult, cancellationToken);
        }


        [HttpGet("mine/agent/list")]
        public async Task<ApiResult<List<AgentViewModel>>> GetAgentsListMine(CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _agentService.GetAgentsListMine(userId, cancellationToken);
        }

        [HttpGet("mine/agent")]
        public async Task<ApiResult<PagedResult<CompanyAgentViewModel>>> GetCompanyAgentsMine([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            return await _agentService.GetCompanyAgentsMine(userId, pageAbleResult, cancellationToken);
        }

        [HttpPost("mine/person")]
        public async Task<ApiResult<PersonResultViewModel>> CreateCompanyAgentMine(PersonViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _agentService.CreateAgentMine(userId, viewModel, cancellationToken);
        }

        [HttpGet("mine/person/{personCode}")]
        public async Task<ApiResult<PersonResultViewModel>> GetCompanyAgentMine(Guid personCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _agentService.GetAgentMine(userId, personCode, cancellationToken);
        }

        [HttpPut("mine/person/{personCode}")]
        public async Task<ApiResult<PersonResultViewModel>> UpdateCompanyAgentMine(Guid personCode, UpdatePersonInputViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _agentService.UpdateAgentMine(userId, personCode, viewModel, cancellationToken);
        }

        [HttpDelete("mine/person/{personCode}")]
        public async Task<ApiResult<string>> DeleteCompanyAgentMine(Guid personCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            string result = (await _agentService.DeleteAgentMine(userId, personCode, cancellationToken)).ToString();
            return result;
        }


        [HttpGet("mine/person/without_user")]
        public async Task<ApiResult<PagedResult<PersonResultViewModel>>> GetPersonsOfCompanyWithoutUser([FromQuery] string search_text, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PagedResult<PersonResultViewModel> persons = await _companyService.GetAllPersonsWithoutUser(userId, search_text, pageAbleResult, cancellationToken);
            return persons;
        }

        #endregion


        #region CompanyCenterSchedule
        [AllowAnonymous]
        [HttpGet("{code}/center/{centerId}/schedule")]
        public async Task<ApiResult<PagedResult<CenterScheduleResultViewModel>>> GetCompanyCenterSchedules(Guid code, long centerId, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _companyCenterServices.GetAllCenterSchedules(code, centerId, pageAbleResult, cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("{code}/center/{centerId}/schedule/{id}")]
        public async Task<ApiResult<CenterScheduleResultViewModel>> GetCompanyCenterSchedule(Guid code, long centerId, long id, CancellationToken cancellationToken)
        {
            return await _companyCenterServices.GetCenterSchedule(code, centerId, id, cancellationToken);
        }

        [HttpPost("{code}/center/{centerId}/schedule")]
        public async Task<ApiResult<CenterScheduleResultViewModel>> CreateCompanyCenterSchedule(Guid code, long centerId, CenterScheduleInputViewModel ScheduleInputViewModel, CancellationToken cancellationToken)
        {
            return await _companyCenterServices.CreateCenterSchedule(code, centerId, ScheduleInputViewModel, cancellationToken);
        }

        [HttpPut("{code}/center/{centerId}/schedule/{id}")]
        public async Task<ApiResult<CenterScheduleResultViewModel>> UpdateCompanyCenterSchedule(Guid code, long centerId, long id, CenterScheduleInputViewModel ScheduleInputViewModel, CancellationToken cancellationToken)
        {
            return await _companyCenterServices.UpdateCenterSchedule(code, centerId, id, ScheduleInputViewModel, cancellationToken);
        }

        [HttpDelete("{code}/center/{centerId}/schedule/{id}")]
        public async Task<ApiResult<string>> DeleteCompanyCenterSchedule(Guid code, long centerId, long id, CancellationToken cancellationToken)
        {
            return await _companyCenterServices.DeleteCenterSchedule(code, centerId, id, cancellationToken);
        }

        #endregion


        #region CompanyCenter
        [AllowAnonymous]
        [HttpGet("{code}/center")]
        public async Task<ApiResult<PagedResult<CompanyCenterResultViewModel>>> GetCompanyCenters(Guid code, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _companyCenterServices.GetAllCenters(code, pageAbleResult, cancellationToken);
        }

        [AllowAnonymous]
        [HttpGet("{code}/center/{id}")]
        public async Task<ApiResult<CompanyCenterResultViewModel>> GetCompanyCenter(Guid code, long id, CancellationToken cancellationToken)
        {
            return await _companyCenterServices.GetCenter(code, id, cancellationToken);
        }

        [HttpPost("{code}/center")]
        public async Task<ApiResult<CompanyCenterResultViewModel>> CreateCompanyCenter(Guid code, CompanyCenterInputViewModel centerInput, CancellationToken cancellationToken)
        {
            return await _companyCenterServices.CreateCenter(code, centerInput, cancellationToken);
        }

        [HttpPut("{code}/center/{id}")]
        public async Task<ApiResult<CompanyCenterResultViewModel>> UpdateCompanyCenter(Guid code, long id, CompanyCenterInputViewModel centerInput, CancellationToken cancellationToken)
        {
            return await _companyCenterServices.UpdateCenter(code, id, centerInput, cancellationToken);
        }

        [HttpDelete("{code}/center/{id}")]
        public async Task<ApiResult<string>> DeleteCompanyCenter(Guid code, long id, CancellationToken cancellationToken)
        {
            return await _companyCenterServices.DeleteCenter(code, id, cancellationToken);
        }
        [AllowAnonymous]
        [HttpGet("{company_code}/city/{city_id}/center")]
        public async Task<ApiResult<List<CompanyCenterViewModel>>> CompanyCentersByCityAndCompaycode(Guid company_code, long city_id, CancellationToken cancellationToken)
        {
            return await _companyCenterServices.GetCentersByCityAndCompanyCode(company_code, city_id, cancellationToken);
        }

        #endregion
        #region CompanyCenterMine
        [HttpGet("mine/center")]
        public async Task<ApiResult<PagedResult<CompanyCenterResultViewModel>>> GetCompanyCentersMine([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            return await _companyCenterServices.GetAllCentersMine(UserID, pageAbleResult, cancellationToken);
        }
        [HttpGet("mine/center/{id}")]
        public async Task<ApiResult<CompanyCenterResultViewModel>> GetCompanyCenterMine(long id, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            return await _companyCenterServices.GetCenterMine(UserID, id, cancellationToken);
        }
        [HttpPost("mine/center")]
        public async Task<ApiResult<CompanyCenterResultViewModel>> CreateCompanyCenterMine(CompanyCenterInputViewModel centerInput, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            return await _companyCenterServices.CreateCenterMine(UserID, centerInput, cancellationToken);
        }
        [HttpPut("mine/center/{id}")]
        public async Task<ApiResult<CompanyCenterResultViewModel>> UpdateCompanyCenterMine(long id, CompanyCenterInputViewModel centerInput, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            return await _companyCenterServices.UpdateCenterMine(UserID, id, centerInput, cancellationToken);
        }
        [HttpGet("mine/city/{city_id}/center")]
        public async Task<ApiResult<List<CompanyCenterViewModel>>> CompanyCentersByCityAndCompaycodeMine(long city_id, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            return await _companyCenterServices.GetCentersByCityAndCompanyCodeMine(UserID, city_id, cancellationToken);
        }
        [HttpDelete("mine/center/{id}")]
        public async Task<ApiResult<string>> DeleteCompanyCenterMine(long id, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            return await _companyCenterServices.DeleteCenterMine(UserID, id, cancellationToken);
        }
        #endregion

        #region CompanyCenterScheduleMine

        [HttpGet("mine/center/{centerId}/schedule")]
        public async Task<ApiResult<PagedResult<CenterScheduleResultViewModel>>> GetCompanyCenterSchedulesMine(Guid code, long centerId, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            return await _companyCenterServices.GetAllCenterSchedulesMine(UserID, centerId, pageAbleResult, cancellationToken);
        }

        [HttpGet("mine/center/{centerId}/schedule/{id}")]
        public async Task<ApiResult<CenterScheduleResultViewModel>> GetCompanyCenterScheduleMine(Guid code, long centerId, long id, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            return await _companyCenterServices.GetCenterScheduleMine(UserID, centerId, id, cancellationToken);
        }

        [HttpPost("mine/center/{centerId}/schedule")]
        public async Task<ApiResult<CenterScheduleResultViewModel>> CreateCompanyCenterScheduleMine(Guid code, long centerId, CenterScheduleInputViewModel ScheduleInputViewModel, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            return await _companyCenterServices.CreateCenterScheduleMine(UserID, centerId, ScheduleInputViewModel, cancellationToken);
        }

        [HttpPut("mine/center/{centerId}/schedule/{id}")]
        public async Task<ApiResult<CenterScheduleResultViewModel>> UpdateCompanyCenterScheduleMine(Guid code, long centerId, long id, CenterScheduleInputViewModel ScheduleInputViewModel, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            return await _companyCenterServices.UpdateCenterScheduleMine(UserID, centerId, id, ScheduleInputViewModel, cancellationToken);
        }
        [HttpDelete("mine/center/{centerId}/schedule/{id}")]
        public async Task<ApiResult<string>> DeleteCompanyCenterScheduleMine(long centerId, long id, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            return await _companyCenterServices.DeleteCenterScheduleMine(UserID, centerId, id, cancellationToken);
        }
        #endregion

        #region Customers

        [HttpGet("mine/customer")]
        public async Task<ApiResult<PagedResult<CustomerViewModel>>> GetCompanyCustomersMine([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User?.GetId());

            return await _companyService.GetAllCustomersMine(userId, pageAbleResult, cancellationToken);
        }


        [HttpGet("mine/{code}/customer")]
        public async Task<ApiResult<PagedResult<CustomerViewModel>>> GetAllCustomersOfCompany(Guid code, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            //long userId = long.Parse(HttpContext.User?.GetId());

            return await _companyService.GetAllCustomersOfCompany(code, pageAbleResult, cancellationToken);
        }
        #endregion

        #region Company Actions mine
        [HttpGet("mine")]
        public async Task<ApiResult<CompanyDetailViewModel>> GetDetailMine(CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            var company = await _companyService.detailMine(UserID, cancellationToken);
            return company;
        }
        [HttpPut("mine")]
        public async Task<ApiResult<CompanyDetailViewModel>> UpdateCompanyMine(CompanyInputViewModel companyInputViewModel, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            var res = await _companyService.updateMine(UserID, companyInputViewModel, cancellationToken);
            return res;
        }

        [HttpPut("{companyCode}/Person/mine")]
        public async Task<ApiResult<PersonCompanyDTOViewModel>> UpdatePersonCompanyMine(Guid companyCode, [FromBody] PersonCompanyInputViewModel personCompanyInputViewModel, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            var res = await _companyService.UpdatePersonCompanyMine(companyCode, UserID, personCompanyInputViewModel, cancellationToken);
            return res;
        }
        [HttpDelete("{companyCode}/Person/mine")]
        public async Task<ApiResult<string>> DeletePersonCompanyMine(Guid companyCode, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User?.GetId());
            var res = await _companyService.DeletePersonCompanyMine(companyCode, UserID, cancellationToken);
            return res.ToString();
        }

        #endregion

        #region PolicyRequest

        [HttpGet("{code}/policy-requst")]
        public async Task<ApiResult<PagedResult<CompanyPolicyRequestViewModel>>> GetCompanyPolicyRequests(Guid code, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _companyService.GetCompanyPolicyReqeusts(code, pageAbleResult, cancellationToken);
        }


        [HttpGet("{code}/policy-requst/{policyCode}")]
        public async Task<ApiResult<CompanyPolicyRequestViewModel>> GetCompanyPolicyRequest(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {
            return await _companyService.GetCompanyPolicyReqeust(code, policyCode, cancellationToken);
        }


        [HttpGet("mine/policy-requst")]
        public async Task<ApiResult<PagedResult<CompanyPolicyRequestViewModel>>> GetCompanyPolicyRequestsMine([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _companyService.GetCompanyPolicyReqeustsMine(userId, pageAbleResult, cancellationToken);
        }

        [HttpGet("mine/policy-requst/{policyCode}")]
        public async Task<ApiResult<CompanyPolicyRequestViewModel>> GetCompanyPolicyRequestMine(Guid policyCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _companyService.GetCompanyPolicyReqeustMine(userId, policyCode, cancellationToken);
        }


        [HttpGet("mine/policy-request/filter/{status}")]
        public async Task<ApiResult<PagedResult<CompanyPolicyRequestViewModel>>> GetCompanyPolicyRequestsByStatusMine(string status, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            long roleId = long.Parse(userRole);

            return await _companyService.GetCompanyPolicyReqeustsByStatusMine(userId, roleId, status, pageAbleResult, cancellationToken);
        }

        [HttpGet("mine/policy-request/filter")]
        public async Task<ApiResult<PagedResult<CompanyPolicyRequestViewModel>>> GetCompanyPolicyRequestsByAllStatusMine([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            long roleId = long.Parse(userRole);

            return await _companyService.GetCompanyPolicyReqeustsByStatusMine(userId, roleId, "all", pageAbleResult, cancellationToken);
        }




        [HttpGet("{code}/policy-request/filter/{status}")]
        public async Task<ApiResult<PagedResult<CompanyPolicyRequestViewModel>>> GetCompanyPolicyRequestsByStatus(Guid code, string status, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            long roleId = long.Parse(userRole);

            return await _companyService.GetCompanyPolicyReqeustsByStatus(code, status, pageAbleResult, cancellationToken);
        }

        [HttpGet("{code}/policy-request/filter")]
        public async Task<ApiResult<PagedResult<CompanyPolicyRequestViewModel>>> GetCompanyPolicyRequestsByAllStatus(Guid code, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }

            long roleId = long.Parse(userRole);

            return await _companyService.GetCompanyPolicyReqeustsByStatus(code, "all", pageAbleResult, cancellationToken);
        }
        #endregion

        #region Policy Request Supplement

        [HttpPost("mine/policy-request/{policyCode}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> CreateCompanyHolderSupplementInfoMine(Guid policyCode, PolicySupplementViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PolicySupplementViewModel result = await _companyService.CreateCompanyPolicyRequestHolderSupplementInfoAsyncMine(userId, policyCode, viewModel, cancellationToken);
            return result;
        }

        [HttpGet("mine/policy-request/{policyCode}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> GetHolderSupplementInfoMine(Guid policyCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PolicySupplementViewModel result = await _companyService.GetCompanyPolicyRequestHolderSupplementInfoMine(userId, policyCode, cancellationToken);

            return result;

        }

        [HttpPut("mine/policy-request/{policyCode}/BodySupplement")]
        public async Task<ApiResult<CompanyBodySupplementInfoViewModel>> AddOrUpdateCompanyBodySupplementIssueMine(Guid policyCode, CompanyBodySupplementInfoViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());


            CompanyBodySupplementInfoViewModel result = await _companyService.AddOrUpdateCompanyBodySupplementMine(policyCode, userId, viewModel, cancellationToken);
            // return result;
            return result;
        }

        [HttpGet("mine/policy-request/{policyCode}/BodySupplement")]
        public async Task<ApiResult<BodySupplementInfoViewModel>> GetBodyCompanySupplementIssueMine(Guid policyCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());


            BodySupplementInfoViewModel result = await _companyService.GetCompanyBodySupplementMine(userId, policyCode, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }







        [HttpPost("{Code}/policy-request/{policyCode}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> CreateCompanyHolderSupplementInfo(Guid Code, Guid policyCode, PolicySupplementViewModel viewModel, CancellationToken cancellationToken)
        {

            PolicySupplementViewModel result = await _companyService.CreateCompanyPolicyRequestHolderSupplementInfoAsync(Code, policyCode, viewModel, cancellationToken);
            return result;
        }
        [HttpGet("{Code}/policy-request/{policyCode}/SupplementInfo")]
        public async Task<ApiResult<PolicySupplementViewModel>> GetHolderSupplementInfo(Guid Code, Guid policyCode, CancellationToken cancellationToken)
        {

            PolicySupplementViewModel result = await _companyService.GetCompanyPolicyRequestHolderSupplementInfo(Code, policyCode, cancellationToken);

            return result;

        }


        [HttpPut("{Code}/policy-request/{policyCode}/BodySupplement")]
        public async Task<ApiResult<CompanyBodySupplementInfoViewModel>> AddOrUpdateCompanyBodySupplementIssue(Guid Code, Guid policyCode, CompanyBodySupplementInfoViewModel viewModel, CancellationToken cancellationToken)
        {

            CompanyBodySupplementInfoViewModel result = await _companyService.AddOrUpdateCompanyBodySupplement(policyCode, Code, viewModel, cancellationToken);
            // return result;
            return result;
        }

        [HttpGet("{code}/policy-request/{policyCode}/BodySupplement")]
        public async Task<ApiResult<BodySupplementInfoViewModel>> GetBodyCompanySupplementIssue(Guid Code, Guid policyCode, CancellationToken cancellationToken)
        {


            BodySupplementInfoViewModel result = await _companyService.GetCompanyBodySupplement(Code, policyCode, cancellationToken);
            return result;
            // return new PolicySupplementViewModel();
        }


        #endregion

        #region Policy Request Issue

        [HttpPut("mine/policy-request/{policyCode}/Issue")]
        public async Task<ApiResult<PolicyRequestIssueViewModel>> AddOrUpdatePolicyRequestIssueMine(Guid policyCode, PolicyRequestIssueInputViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PolicyRequestIssueViewModel result = await _companyService.CreateOrUpdateCompanyPolicyRequestHolderIssueAsyncMine(userId, policyCode, viewModel, cancellationToken);
            // return result;
            return result;
        }

        [HttpGet("mine/policy-request/{policyCode}/Issue")]
        public async Task<ApiResult<PolicyRequestIssueViewModel>> GetCompanyPolicyRequestIssueMine(Guid policyCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PolicyRequestIssueViewModel result = await _companyService.GetCompanyPolicyRequestHolderIssueAsyncMine(userId, policyCode, cancellationToken);
            return result;
            // return new PolicyRequestIssueViewModel();
        }



        [HttpPut("{code}/policy-request/{policyCode}/Issue")]
        public async Task<ApiResult<PolicyRequestIssueViewModel>> AddOrUpdatePolicyRequestIssue(Guid Code, Guid policyCode, PolicyRequestIssueInputViewModel viewModel, CancellationToken cancellationToken)
        {


            PolicyRequestIssueViewModel result = await _companyService.CreateOrUpdateCompanyPolicyRequestHolderIssueAsync(Code, policyCode, viewModel, cancellationToken);
            // return result;
            return result;
        }

        [HttpGet("{code}/policy-request/{policyCode}/Issue")]
        public async Task<ApiResult<PolicyRequestIssueViewModel>> GetCompanyPolicyRequestIssue(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {


            PolicyRequestIssueViewModel result = await _companyService.GetCompanyPolicyRequestHolderIssueAsync(code, policyCode, cancellationToken);
            return result;
            // return new PolicyRequestIssueViewModel();
        }
        #endregion

        #region Policy Request PaymentInfo

        [HttpGet("mine/policy-request/{policyCode}/PaymentInfo")]

        public async Task<ApiResult<PolicyRequestPaymentViewModel>> GetCompanyPolicyRequestPaymentInfoMine(Guid policyCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            var result = await _companyService.GetCompanyPolicyRequestPaymentDetailsMine(userId, policyCode, cancellationToken);
            return result;
        }




        [HttpGet("{code}/policy-request/{policyCode}/PaymentInfo")]

        public async Task<ApiResult<PolicyRequestPaymentViewModel>> GetCompanyPolicyRequestPaymentInfo(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {

            var result = await _companyService.GetCompanyPolicyRequestPaymentDetails(code, policyCode, cancellationToken);
            return result;
        }
        #endregion

        #region Policy Request Inspection

        [HttpPut("mine/policy-request/{policyCode}/Inspection")]
        public async Task<ApiResult<PolicyRequestInspectionResultViewModel>> AddOrUpdateCompanyPolicyRequestInspectionMine(Guid policyCode, PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());


            PolicyRequestInspectionResultViewModel result = await _companyService.CreateOrUpdateCompanyPolicyRequestHolderInspectionAsyncMine(userId, policyCode, viewModel, cancellationToken);

            return result;
        }

        [HttpGet("mine/policy-request/{policyCode}/Inspection")]
        public async Task<ApiResult<PolicyRequestInspectionResultViewModel>> GetCompanyPolicyRequestInspectionMine(Guid policyCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());


            PolicyRequestInspectionResultViewModel result = await _companyService.GetCompanyPolicyRequestHolderInspectionAsyncMine(userId, policyCode, cancellationToken);
            return result;
        }



        [HttpPut("{code}/policy-request/{policyCode}/Inspection")]
        public async Task<ApiResult<PolicyRequestInspectionResultViewModel>> AddOrUpdateCompanyPolicyRequestInspection(Guid code, Guid policyCode, PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken)
        {

            PolicyRequestInspectionResultViewModel result = await _companyService.CreateOrUpdateCompanyPolicyRequestHolderInspectionAsync(code, policyCode, viewModel, cancellationToken);

            return result;
        }

        [HttpGet("{code}/policy-request/{policyCode}/Inspection")]
        public async Task<ApiResult<PolicyRequestInspectionResultViewModel>> GetCompanyPolicyRequestInspection(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {
            PolicyRequestInspectionResultViewModel result = await _companyService.GetCompanyPolicyRequestHolderInspectionAsync(code, policyCode, cancellationToken);
            return result;
        }
        #endregion



        #region BodyPaymentInfo

        [HttpGet("mine/policy-request/{policyCode}/BodyPaymentInfo")]
        public async Task<ApiResult<PolicyRequestPaymentViewModel>> GetCompanyBodyRequestPaymentInfoMine(Guid policyCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());


            var result = await _companyService.GetCompanyPolicyRequestBodyPaymentDetailsMine(userId, policyCode, cancellationToken);
            return result;
        }



        [HttpGet("{code}/policy-request/{policyCode}/BodyPaymentInfo")]
        public async Task<ApiResult<PolicyRequestPaymentViewModel>> GetCompanyBodyRequestPaymentInfo(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {


            PolicyRequestPaymentViewModel result = await _companyService.GetCompanyPolicyRequestBodyPaymentDetails(code, policyCode, cancellationToken);
            return result;
        }

        #endregion

        #region Policy Request Detail

        [HttpGet("mine/policy-request/{policyCode}/detail")]
        public async Task<ApiResult<PolicyRequestMineViewModel>> GetCompanyPolicyRequestDetailsMine(Guid policyCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());


            return await _companyService.GetCompanyPolicyDetailsMine(userId, policyCode, cancellationToken);
        }

        [HttpGet("{code}/policy-request/{policyCode}/detail")]
        public async Task<ApiResult<PolicyRequestMineViewModel>> GetCompanyPolicyRequestDetails(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {

            return await _companyService.GetCompanyPolicyDetails(code, policyCode, cancellationToken);
        }
        #endregion


        #region Agent Select

        [HttpGet("mine/policy-request/{policyCode}/AgentSelect")]
        public async Task<PolicyRequestAgetSelectGetViewModel> CompanyPolicyRequestAgentSelectMine(Guid policyCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PolicyRequestAgetSelectGetViewModel result = await _companyService.GetCompanyPolicyRequestAgentSelectMine(userId, policyCode, cancellationToken);
            return result;
        }

        [HttpPut("mine/policy-request/{policyCode}/AgentSelect")]
        public async Task<PolicyRequestAgetSelectUpdateOutputViewModel> CompanyPolicyRequestAgentSelectUpdateMine(Guid policyCode, [FromBody] PolicyRequestAgetSelectUpdateInputViewModel PolicyRequestAgetSelectUpdate, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            PolicyRequestAgetSelectUpdateOutputViewModel result = await _companyService.CompanyPolicyRequestAgentSelectUpdateMine(userId, policyCode, PolicyRequestAgetSelectUpdate, cancellationToken);
            return result;
        }



        [HttpGet("{code}/policy-request/{policyCode}/AgentSelect")]
        public async Task<PolicyRequestAgetSelectGetViewModel> CompanyPolicyRequestAgentSelect(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {

            PolicyRequestAgetSelectGetViewModel result = await _companyService.GetCompanyPolicyRequestAgentSelect(code, policyCode, cancellationToken);
            return result;
        }


        [HttpPut("{code}/policy-request/{policyCode}/AgentSelect")]
        public async Task<PolicyRequestAgetSelectUpdateOutputViewModel> CompanyPolicyRequestAgentSelectUpdate(Guid code, Guid policyCode, [FromBody] PolicyRequestAgetSelectUpdateInputViewModel PolicyRequestAgetSelectUpdate, CancellationToken cancellationToken)
        {

            PolicyRequestAgetSelectUpdateOutputViewModel result = await _companyService.CompanyPolicyRequestAgentSelectUpdate(code, policyCode, PolicyRequestAgetSelectUpdate, cancellationToken);
            return result;
        }
        #endregion

        #region Attachment

        [HttpGet("mine/policy-request/{policyCode}/Attachment")]
        public async Task<ApiResult<List<PolicyRequestAttachmentDownloadViewModel>>> GetCompanyPolicyRequestAttachmentsMine(Guid policyCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());


            List<PolicyRequestAttachmentDownloadViewModel> result = await _companyService.GetCompanyPolicyRequestAttachmentsMine(userId, policyCode, cancellationToken);
            return result;
        }


        [HttpGet("{code}/policy-request/{policyCode}/Attachment")]
        public async Task<ApiResult<List<PolicyRequestAttachmentDownloadViewModel>>> GetCompanyPolicyRequestAttachments(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {

            List<PolicyRequestAttachmentDownloadViewModel> result = await _companyService.GetCompanyPolicyRequestAttachments(code, policyCode, cancellationToken);
            return result;
        }

        #endregion


        #region MyUser

        [HttpPost("mine/user")]
        public async Task<ApiResult<UserResultViewModel>> UpdateCompanyUsers(UserInputViewModel userInputViewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _companyService.CreateCompanyUser(userId, userInputViewModel, cancellationToken);
        }

        [HttpGet("mine/user")]
        public async Task<ApiResult<PagedResult<CompanyUserResultViewModel>>> GetCompanyUsers([FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _companyService.GetCompanyUsers(userId, pageAbleResult, cancellationToken);
        }

        [HttpPut("mine/user/{userCode}")]
        public async Task<ApiResult<UpdatedUserResultViewModel>> UpdateCompanyUsers(Guid userCode, CompanyUserUpdateInputViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _companyService.UpdateCompanyUser(userId, userCode, viewModel, cancellationToken);
        }

        [HttpDelete("mine/user/{userCode}")]
        public async Task<bool> DeleteCompanyUsers(Guid userCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _companyService.DeleteCompanyUser(userId, userCode, cancellationToken);
        }

        [HttpGet("mine/user/{userCode}")]
        public async Task<ApiResult<CompanySingleUserResultViewModel>> GetCompanyUser(Guid userCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _companyService.GetCompanyUser(userId, userCode, cancellationToken);
        }

        [HttpPatch("mine/user/{userCode}/changePassword")]
        public async Task<ApiResult<string>> ChangeCompanyUserPassword(Guid userCode, UserChangePasswordViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _companyService.ChangeUserPassword(userId, userCode, viewModel, cancellationToken);
        }
        #endregion


        #region Comment

        [HttpPost("{code}/policy-request/{policyCode}/comment")]
        public async Task<ApiResult<PolicyRequestCommentGetAllOutputViewModel>> CreateCompanyPolicyComment(Guid code, Guid policyCode, CompanyCommentInputViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _companyService.CreateCompanyComment(userId, code, policyCode, viewModel, cancellationToken);
        }

        [HttpGet("{code}/policy-request/{policyCode}/comment")]
        public async Task<ApiResult<List<PolicyRequestCommentGetAllOutputViewModel>>> GetCompanyPolicyComment(Guid code, Guid policyCode, CancellationToken cancellationToken)
        {

            return await _companyService.GetAllPolicyComments(code, policyCode, cancellationToken);
        }



        [HttpPost("mine/policy-request/{policyCode}/comment")]
        public async Task<ApiResult<PolicyRequestCommentGetAllOutputViewModel>> CreateCompanyPolicyCommentMine(Guid policyCode, CompanyCommentInputMineViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _companyService.CreateCompanyCommentMine(userId, policyCode, viewModel, cancellationToken);
        }

        [HttpGet("mine/policy-request/{policyCode}/comment")]
        public async Task<ApiResult<List<PolicyRequestCommentGetAllOutputViewModel>>> GetCompanyPolicyCommentMine(Guid policyCode, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _companyService.GetAllPolicyCommentsMine(userId, policyCode, cancellationToken);
        }

        #endregion

        #region Comission

        [HttpPatch("mine/policy-request/{policyCode}/comission")]
        public async Task<ApiResult<CompanyComissionResultViewModel>> CompanyAgentComission(Guid policyCode, CompanyComissionInputViewModel viewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.GetId());

            return await _companyService.UpdateCompanyPolicyComission(userId, policyCode, viewModel, cancellationToken);
        }

        #endregion

        #region Company Policy Status

        [HttpPatch("{code}/policy-request/{policyCode}/status")]
        public async Task<ApiResult<PolicyRequestSummaryOutputViewModel>> PlicyRequestStatusChange(Guid code, Guid policyCode, PolicyReqiestStatusInputViewModel _policyReqiestStatusInputViewModel, CancellationToken cancellationToken)
        {
            long UserID = long.Parse(HttpContext.User.Identity.GetUserId());
            var claims = HttpContext.User.Claims.ToList();
            var userRole = claims.Where(z => z.Type == ClaimTypes.Role).Select(z => z.Value).FirstOrDefault();
            if (userRole == null)
            {
                throw new BadRequestException("شما نقشی در این سیستم ندارید");
            }
            long roleId = long.Parse(userRole);
            return await _companyService.CompanyPlicyRequestStatusChange(code, policyCode, roleId, UserID, _policyReqiestStatusInputViewModel, cancellationToken);
        }




        [HttpPatch("mine/policy-request/{policyCode}/status")]
        public async Task<ApiResult<PolicyRequestSummaryOutputViewModel>> PlicyRequestStatusChangeMine(Guid policyCode, PolicyReqiestStatusInputViewModel _policyReqiestStatusInputViewModel, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User.Identity.GetUserId());

            return await _companyService.CompanyPlicyRequestStatusChangeMine(policyCode, userId, _policyReqiestStatusInputViewModel, cancellationToken);
        }

        #endregion

        #region Factor
        [HttpPost("{code}/policy-request/{policyCode}/factor")]
        public async Task<CompanyPolicyRequestFactorResultViewModel> CreateFactor(Guid code, Guid policyCode, CompanyPolicyFactorInputViewModel ViewModel,
            CancellationToken cancellationToken)
        {
            var result = await _companyService.CreatePaymentFactor(code, policyCode, ViewModel, cancellationToken);
            return result;
        }


        [HttpGet("{code}/factor")]
        public async Task<ApiResult<PagedResult<CompanyPolicyRequestFactorResultViewModel>>> GetAllFactors(Guid code, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<CompanyPolicyRequestFactorResultViewModel> result = await _companyService.GetAllFactors(code, pageAbleResult, cancellationToken);
            return result;
        }

        [HttpGet("{code}/policy-request/{policyCode}/factor")]
        public async Task<ApiResult<PagedResult<CompanyPolicyRequestFactorResultViewModel>>> GetAllPolicyFactors(Guid code,Guid policyCode,[FromQuery] PageAbleResult pageAbleResult,
           CancellationToken cancellationToken)
        {
            PagedResult<CompanyPolicyRequestFactorResultViewModel> result = await _companyService.GetAllPolicyFactors(code,policyCode,pageAbleResult, cancellationToken);
            return result;
        }

        [HttpGet("{code}/policy-request/{policyCode}/factor/{factorId}")]
        public async Task<CompanyPolicyRequestFactorResultViewModel> GetCompanyPolicyFactor( Guid code, Guid policyCode, long factorId,
            CancellationToken cancellationToken)
        {
            CompanyPolicyRequestFactorResultViewModel result = await _companyService.GetCompanyPolicyFactor(code, policyCode, factorId, cancellationToken);
            return result;
        }

        [HttpDelete("{code}/policy-request/{policyCode}/factor/{factorId}")]
        public async Task<ApiResult<string>> DeleteFactor(Guid code, Guid policyCode, long factorId,
            CancellationToken cancellationToken)
        {
            bool result = await _companyService.DeleteFactor(code,policyCode, factorId, cancellationToken);
            return result.ToString();
        }

        [HttpPost("{code}/policy-request/{policyCode}/factor/{factorId}/detail")]
        public async Task<CompanyFactorDetailResultViewModel> CreateFactorDetail(Guid code, Guid policyCode,long factorId, CompanyFactorDetailInputViewModel ViewModel,
            CancellationToken cancellationToken)
        {
            var result = await _companyService.CreatePaymentFactorDetail(code, policyCode, factorId, ViewModel, cancellationToken);
            return result;
        }

        [HttpPut("{code}/policy-request/{policyCode}/factor/{factorId}/detail/{detailId}")]
        public async Task<CompanyFactorDetailResultViewModel> UpdateFactorDetail(Guid code, Guid policyCode, long factorId,long detailId, CompanyFactorDetailInputViewModel ViewModel,
            CancellationToken cancellationToken)
        {
            var result = await _companyService.UpdatePaymentFactorDetail(code, policyCode, factorId, detailId, ViewModel, cancellationToken);
            return result;
        }

        [HttpGet("{code}/policy-request/{policyCode}/factor/{factorId}/detail")]
        public async Task<ApiResult<PagedResult<CompanyFactorDetailResultViewModel>>> GetAllPolicyFactorDetails(Guid code, Guid policyCode, long factorId, [FromQuery] PageAbleResult pageAbleResult,
           CancellationToken cancellationToken)
        {
            PagedResult<CompanyFactorDetailResultViewModel> result = await _companyService.GetAllPolicyFactorDetials(code, policyCode, factorId, pageAbleResult, cancellationToken);
            return result;
        }

        [HttpGet("{code}/policy-request/{policyCode}/factor/{factorId}/detail/{detailId}")]
        public async Task<CompanyFactorDetailResultViewModel> GetCompanyPolicyFactor(Guid code, Guid policyCode, long factorId, long detailId, CancellationToken cancellationToken)
        {
            CompanyFactorDetailResultViewModel result = await _companyService.GetCompanyFactorDetail(code, policyCode, factorId, detailId, cancellationToken);
            return result;
        }

        [HttpDelete("{code}/policy-request/{policyCode}/factor/{factorId}/detail/{detailId}")]
        public async Task<ApiResult<string>> DeleteFactorDetail(Guid code, Guid policyCode, long factorId, long detailId, CancellationToken cancellationToken)
        {
            bool result = await _companyService.DeleteFactorDetail(code, policyCode, factorId, detailId, cancellationToken);
            return result.ToString();
        }

        //[HttpDelete("{code}/payment/testDelete")]
        //public async Task<ApiResult<string>> DeleteAllPaymentWithGateways(Guid code, Guid policyCode, long factorId, long detailId, CancellationToken cancellationToken)
        //{
        //    bool result = await _companyService.DeleteAllPaymentWithGateways(code, cancellationToken);
        //    return result.ToString();
        //}











        [HttpPost("mine/policy-request/{policyCode}/factor")]
        public async Task<CompanyPolicyRequestFactorResultViewModel> CreateFactorMine(Guid policyCode, CompanyPolicyFactorInputViewModel ViewModel,
            CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User?.GetId());
            var result = await _companyService.CreatePaymentFactorMine(userId, policyCode, ViewModel, cancellationToken);
            return result;
        }

        [HttpGet("mine/factor")]
        public async Task<ApiResult<PagedResult<CompanyPolicyRequestFactorResultViewModel>>> GetAllFactorsMine( [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User?.GetId());
            PagedResult<CompanyPolicyRequestFactorResultViewModel> result = await _companyService.GetAllFactorsMine(userId, pageAbleResult, cancellationToken);
            return result;
        }

        [HttpGet("mine/policy-request/{policyCode}/factor")]
        public async Task<ApiResult<PagedResult<CompanyPolicyRequestFactorResultViewModel>>> GetAllPolicyFactorsMine( Guid policyCode, [FromQuery] PageAbleResult pageAbleResult,
           CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User?.GetId());
            PagedResult<CompanyPolicyRequestFactorResultViewModel> result = await _companyService.GetAllPolicyFactorsMine(userId, policyCode, pageAbleResult, cancellationToken);
            return result;
        }

        [HttpGet("mine/policy-request/{policyCode}/factor/{factorId}")]
        public async Task<CompanyPolicyRequestFactorResultViewModel> GetCompanyPolicyFactorMine(Guid policyCode, long factorId,
            CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User?.GetId());
            CompanyPolicyRequestFactorResultViewModel result = await _companyService.GetCompanyPolicyFactorMine(userId, policyCode, factorId, cancellationToken);
            return result;
        }

        [HttpDelete("mine/policy-request/{policyCode}/factor/{factorId}")]
        public async Task<ApiResult<string>> DeleteFactorMine(Guid policyCode, long factorId,
            CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User?.GetId());
            bool result = await _companyService.DeleteFactorMine(userId, policyCode, factorId, cancellationToken);
            return result.ToString();
        }

        [HttpPost("mine/policy-request/{policyCode}/factor/{factorId}/detail")]
        public async Task<CompanyFactorDetailResultViewModel> CreateFactorDetailMine( Guid policyCode, long factorId, CompanyFactorDetailInputViewModel ViewModel,
            CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User?.GetId());
            var result = await _companyService.CreatePaymentFactorDetailMine(userId, policyCode, factorId, ViewModel, cancellationToken);
            return result;
        }

        [HttpPut("mine/policy-request/{policyCode}/factor/{factorId}/detail/{detailId}")]
        public async Task<CompanyFactorDetailResultViewModel> UpdateFactorDetailMine(Guid policyCode, long factorId, long detailId, CompanyFactorDetailInputViewModel ViewModel,
            CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User?.GetId());
            var result = await _companyService.UpdatePaymentFactorDetailMine(userId, policyCode, factorId, detailId, ViewModel, cancellationToken);
            return result;
        }


        [HttpGet("mine/policy-request/{policyCode}/factor/{factorId}/detail")]
        public async Task<ApiResult<PagedResult<CompanyFactorDetailResultViewModel>>> GetAllPolicyFactorDetailsMine(Guid policyCode, long factorId, [FromQuery] PageAbleResult pageAbleResult,
           CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User?.GetId());
            PagedResult<CompanyFactorDetailResultViewModel> result = await _companyService.GetAllPolicyFactorDetialsMine(userId, policyCode, factorId, pageAbleResult, cancellationToken);
            return result;
        }

        [HttpGet("mine/policy-request/{policyCode}/factor/{factorId}/detail/{detailId}")]
        public async Task<CompanyFactorDetailResultViewModel> GetCompanyPolicyFactorMien(Guid policyCode, long factorId, long detailId, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User?.GetId());
            CompanyFactorDetailResultViewModel result = await _companyService.GetCompanyFactorDetailMine(userId, policyCode, factorId, detailId, cancellationToken);
            return result;
        }

        [HttpDelete("mine/policy-request/{policyCode}/factor/{factorId}/detail/{detailId}")]
        public async Task<ApiResult<string>> DeleteFactorDetail(Guid policyCode, long factorId, long detailId, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User?.GetId());
            bool result = await _companyService.DeleteFactorDetailMine(userId, policyCode, factorId, detailId, cancellationToken);
            return result.ToString();
        }
        #endregion

        #region companyPayment

        [HttpGet("mine/payment")]
        public async Task<ApiResult<PagedResult<CompanyFactorViewModel>>> GetAllPaymentsMine(long? statusId, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            long userId = long.Parse(HttpContext.User?.GetId());
            PagedResult<CompanyFactorViewModel> factors = await _companyService.GetAllPaymentsMine(userId, statusId, pageAbleResult, cancellationToken);
            return factors;
        }

        #endregion
    }
}
