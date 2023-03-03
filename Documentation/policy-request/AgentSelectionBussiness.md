<div align="right" dir="rtl">

عملیات ویرایش جدول PolicyRequest بصورت زیر پیاده سازی شده. این عملیات ها در کنترلر های Company و PolicyRequest قرار دارند چراکه هم کاربران می توانند نماینده انتخاب کنند هم شرکت باید این دسترسی را داشته باشد. 



>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*


سرویس های کنترلر PolicyRequest:
</div>

```C#

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

```

<div align="right" dir="rtl">

سرویس های کنترلر Company:

</div>


```C#

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


```






<div align="right" dir="rtl">

**درج یا ویرایش (سرویس Put)** : این سرویس متد `PolicyRequestAgentSelectUpdate(code, PolicyRequestAgetSelectUpdate, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

 public async Task<PolicyRequestAgetSelectUpdateOutputViewModel> PolicyRequestAgentSelectUpdate(Guid code,
            PolicyRequestAgetSelectUpdateInputViewModel PolicyRequestAgetSelectUpdate,
            CancellationToken cancellationToken)
        {
            DAL.Models.PolicyRequest PolicyRequest = await _policyRequestRepository.GetByCode(code, cancellationToken);
            if (PolicyRequest == null)
                throw new BadRequestException("درخواست بیمه یافت نشد");

            CompanyAgent companyAgent = await _agentRepository.GetByIdAsync(cancellationToken, PolicyRequestAgetSelectUpdate.AgentSelectedId);

            PolicyRequest.AgentSelectionTypeId = PolicyRequestAgetSelectUpdate.AgentSelectionTypeId;
            if (PolicyRequestAgetSelectUpdate.AgentSelectedId.HasValue)
            {
                PolicyRequest.AgentSelectedId = PolicyRequestAgetSelectUpdate.AgentSelectedId;
                PolicyRequest.ReviewerId = companyAgent.PersonId;
            }
            else
            {
                PolicyRequest.AgentSelectedId = null;
            }

            await _policyRequestRepository.UpdateAsync(PolicyRequest, cancellationToken);
            return _mapper.Map<PolicyRequestAgetSelectUpdateOutputViewModel>(PolicyRequest);
        }



 

```

<div align="right" dir="rtl">

<br>

**دریافت تکی (سرویس Get)** : این سرویس متد `etPolicyRequestAgentSelect(code, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

        public async Task<PolicyRequestAgetSelectGetViewModel> GetPolicyRequestAgentSelect(Guid code,
            CancellationToken cancellationToken)
        {
            var ResultData = await _policyRequestRepository.GetPolicyRequestAndCompanyByCode(code, cancellationToken);
            if (ResultData == null)
                throw new BadRequestException("درخواست بیمه یافت نشد");
            return _mapper.Map<PolicyRequestAgetSelectGetViewModel>(ResultData);
        }



```


<div align="right" dir="rtl">

این ها نمونه های کنترلر policy بودند. سایر سرویس ها دقیقا با همین مکانیزم پیاده شده اند و تنها تفاوت اعتبار سنجی ها می باشد.

</div>


