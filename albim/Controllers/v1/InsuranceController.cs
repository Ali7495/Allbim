using albim.Result;
using Common.Exceptions;
using Common.Utilities;
using DAL.Models;
using Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models.CentralRuleType;
using Models.FAQ;
using Models.Insurance;
using Models.InsuranceCentralRule;
using Models.InsuranceTermType;
using Models.InsurerTerm;
using Models.PageAble;
using Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace albim.Controllers.v1
{

    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [AllowAnonymous]
    public class InsuranceController : BaseController
    {
        #region Property
        private readonly IInsuranceService _insuranceService;
        private readonly IInsuranceCenteralRuleService _insuranceCenteralRuleService;
        private readonly IInsuranceFAQServices _fAQServices;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public InsuranceController(IInsuranceService insuranceService, IInsuranceCenteralRuleService insuranceCenteralRuleService, IInsuranceFAQServices fAQServices)
        {
            _insuranceCenteralRuleService = insuranceCenteralRuleService;
            _insuranceService = insuranceService;
            _fAQServices = fAQServices;
        }
        #endregion

        #region Insurance Actions



        [HttpGet("{slug}/insurer")]
        public async Task<ApiResult<List<InsurerViewModel>>> GetInsurers(string slug, CancellationToken cancellationToken)
        {
            var model = await _insuranceService.GetInsuranceInsurer(slug, cancellationToken);
            return model;
        }


        [HttpPost("")]
        public async Task<ApiResult<InsuranceViewModel>> Create(InsuranceInputViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            var model = await _insuranceService.CreateInsurance(insuranceViewModel, cancellationToken);
            return model;
        }

        [HttpGet("{id}")]
        public async Task<ApiResult<InsuranceViewModel>> GetDetail(long id, CancellationToken cancellationToken)
        {
            var model = await _insuranceService.GetInsurance(id, cancellationToken);
            return model;
        }

        // [HttpGet("")]
        // public async Task<PagedResult<InsuranceViewModel>> GetAll([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        // {
        //     var model = await _insuranceService.GetAllInsurances(page, pageSize, cancellationToken);
        //     return model;
        // }
        [HttpGet("")]
        public async Task<ApiResult<List<InsuranceViewModel>>> GetAll([FromQuery] int? page, [FromQuery] int? pageSize, CancellationToken cancellationToken)
        {
            var model = await _insuranceService.GetAllInsurances(cancellationToken);
            return model;
        }

        [HttpGet("list")]
        public async Task<ApiResult<List<InsuranceViewModel>>> GetAllWithoutPaginate(CancellationToken cancellationToken)
        {
            var model = await _insuranceService.GetAllWithoutPaginate(cancellationToken);
            return model;
        }


        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<ApiResult<string>> Delete(int id, CancellationToken cancellationToken)
        {
            var model = await _insuranceService.DeleteInsurance(id, cancellationToken);
            return model.ToString();
        }


        [AllowAnonymous]
        [HttpGet("{slug}/details")]
        public async Task<ApiResult<List<InsuranceDetailsViewModel>>> GetInsuranceDetails(string slug, CancellationToken cancellationToken)
        {
            return await _insuranceService.GetInsuranceDetails(slug, cancellationToken);
        }

        #endregion

        #region InsuranceCenteralRule Actions

        [HttpPost("{insuranceId}/rule")]
        public async Task<ApiResult<InsuranceCentralRuleResultViewModel>> CreateInsuranceCentralRule(long insuranceId, InsuranceCentralRuleInputViewModel insuranceCentralRule, CancellationToken cancellationToken)
        {
            InsuranceCentralRuleResultViewModel centralRule = await _insuranceCenteralRuleService.CreateInsuranceCenteralRule(insuranceId, insuranceCentralRule, cancellationToken);
            return centralRule;
        }

        [HttpGet("{insuranceId}/rule/{RuleId}")]
        public async Task<ApiResult<InsuranceCentralRuleResultViewModel>> GetDetailRule(long insuranceId, long RuleId, CancellationToken cancellationToken)
        {
            InsuranceCentralRuleResultViewModel model = await _insuranceCenteralRuleService.GetInsuranceCenteralRule(insuranceId, RuleId, cancellationToken);
            return model;
        }

        [HttpGet("{insuranceId}/rule")]
        public async Task<ApiResult<PagedResult<InsuranceCentralRuleResultViewModel>>> GetAllRules(long insuranceId, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PagedResult<InsuranceCentralRuleResultViewModel> model = await _insuranceCenteralRuleService.GetAllInsuranceCenteralRules(insuranceId, pageAbleResult, cancellationToken);
            return model;
        }

        [HttpPut("{insuranceId}/rule/{RuleId}")]
        public async Task<ApiResult<InsuranceCentralRuleResultViewModel>> UpdateCentralRule(long insuranceId, long RuleId, InsuranceCentralRuleInputViewModel insuranceCentralRule, CancellationToken cancellationToken)
        {
            InsuranceCentralRuleResultViewModel model = await _insuranceCenteralRuleService.CentralRule(insuranceId, RuleId, insuranceCentralRule, cancellationToken);
            return model;
        }

        [HttpDelete("{insuranceId}/rule/{RuleId}")]
        public async Task<ApiResult<string>> DeleteRule(int insuranceId, long RuleId, CancellationToken cancellationToken)
        {
            bool result = await _insuranceCenteralRuleService.DeleteInsuranceCenteralRule(insuranceId, RuleId, cancellationToken);
            return result.ToString();
        }
        
        #endregion

       

        #region FAQ

        [HttpPost("{id}/FAQ")]
        public async Task<ApiResult<FAQResultViewModel>> PostInsuranceFAQ(long id, FAQInputViewModel model, CancellationToken cancellationToken)
        {
            return await _fAQServices.PostNewInsuranceFAQ(id, model, cancellationToken);
        }

        [HttpGet("{id}/FAQ/{faqId}")]
        public async Task<ApiResult<FAQResultViewModel>> GetInsuranceFAQ(long id, long faqId, CancellationToken cancellationToken)
        {
            return await _fAQServices.GetInsuranceFAQ(id, faqId, cancellationToken);
        }

        [HttpGet("{id}/FAQ")]
        public async Task<ApiResult<PagedResult<FAQResultViewModel>>> GetAllInsuranceFAQs(long id, [FromQuery] PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            return await _fAQServices.GetAllInsuranceFAQs(id, pageAbleResult, cancellationToken);
        }

        [HttpPut("{id}/FAQ/{faqId}")]
        public async Task<ApiResult<FAQResultViewModel>> UpdateInsuranceFAQ(long id, long faqId, FAQInputViewModel model, CancellationToken cancellationToken)
        {
            return await _fAQServices.UpdateInsuranceFAQ(id, faqId, model, cancellationToken);
        }

        [HttpDelete("{id}/FAQ/{faqId}")]
        public async Task<bool> DeleteInsuranceFAQ(long id, long faqId, CancellationToken cancellationToken)
        {
            return await _fAQServices.DeleteInsuranceFAQ(id, faqId, cancellationToken);
        }
        #endregion

        #region Insurance Term Type

        [HttpGet("{insuranceId}/termType")]
        public async Task<ApiResult<List<InsuranceTermTypeViewModel>>> GetAllInsuranceTermTypes(long insuranceId, CancellationToken cancellationToken)
        {
            List<InsuranceTermTypeViewModel> result = await _insuranceService.GetAllInsuranceTermTypes(insuranceId, cancellationToken);
            return result;
        }

        #endregion

        #region CentralRuleType

        [HttpGet("{insuranceId}/ruleType")]
        public async Task<ApiResult<List<CentralRuleTypeViewModel>>> GetAllCentralRuleTypes(long insuranceId, CancellationToken cancellationToken)
        {
            List<CentralRuleTypeViewModel> result = await _insuranceService.GetAllCentralRuleTypes(insuranceId, cancellationToken);
            return result;
        }

        #endregion

    }
}
