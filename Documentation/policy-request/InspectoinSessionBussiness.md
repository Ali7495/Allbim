<div align="right" dir="rtl">

عملیات CRUD جدول PolicyRequestInspectionSession بصورت زیر پیاده سازی شده. این عملیات ها در کنترلر های Company و PolicyRequest قرار دارند چراکه هم کاربران می توانند محل بازبینی انتخاب کنند هم شرکت باید این دسترسی را داشته باشد. 



>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*


سرویس های کنترلر PolicyRequest:
</div>

```C#

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


```

<div align="right" dir="rtl">

سرویس های کنترلر Company:

</div>


```C#

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


```






<div align="right" dir="rtl">

**درج یا ویرایش (سرویس Put)** : این سرویس متد `CreateOrUpdatePolicyRequestHolderInspectionAsync(code, viewModel, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

 public async Task<PolicyRequestInspectionResultViewModel> CreateOrUpdatePolicyRequestHolderInspectionAsync(
            string code, PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Guid policyCode = Guid.Parse(code);
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCode(policyCode, cancellationToken);

            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }


            PolicyRequestInspection inspection = await AddOrUpdatePolicyRequestInspectionCommon(policyRequest, viewModel, cancellationToken);


            return _mapper.Map<PolicyRequestInspectionResultViewModel>(inspection);
        }




 public async Task<PolicyRequestInspection> AddOrUpdatePolicyRequestInspectionCommon(DAL.Models.PolicyRequest policyRequest, PolicyRequestInspectionInputViewModel viewModel, CancellationToken cancellationToken)
        {
            PolicyRequestInspection inspection;
            inspection = await _policyRequestInspectionRepository.GetByPolicyRequestCode(policyRequest.Code,
                cancellationToken);
            if (inspection == null)
            {
                inspection = new PolicyRequestInspection();
                inspection.PolicyRequestId = policyRequest.Id;
                inspection.InspectionTypeId = viewModel.InspectionTypeId;


                if (viewModel.InspectionTypeId == 1)
                {
                    Address address = new Address();

                    address = await _addressRepository.GetAddressByCode(viewModel.LocationInspection.AddressCode.Value,
                        cancellationToken);
                    if (address == null)
                    {
                        throw new BadRequestException("کد آدرس وجود ندارد");
                    }

                    inspection.InspectionSessionDate =
                        DateTime.Parse(viewModel.LocationInspection.InspectionSessionDate);
                    inspection.InspectionAddressId = address.Id;
                    inspection.InspectionSessionId = viewModel.LocationInspection.InspectionSessionId;
                }
                else if (viewModel.InspectionTypeId == 2)
                {
                    inspection.InspectionSessionDate = DateTime.Parse(viewModel.CenterInspection.InspectionSessionDate);
                    inspection.CompanyCenterScheduleId = viewModel.CenterInspection.CompanyCenterScheduleId;
                }

                await _policyRequestInspectionRepository.AddAsync(inspection, cancellationToken);
            }
            else
            {
                inspection.PolicyRequestId = policyRequest.Id;
                inspection.InspectionTypeId = viewModel.InspectionTypeId;

                if (viewModel.InspectionTypeId == 1)
                {
                    Address address = new Address();

                    address = await _addressRepository.GetAddressByCode(viewModel.LocationInspection.AddressCode.Value,
                        cancellationToken);
                    if (address == null)
                    {
                        throw new BadRequestException("کد آدرس وجود ندارد");
                    }

                    inspection.InspectionAddressId = address.Id;
                    inspection.InspectionSessionId = viewModel.LocationInspection.InspectionSessionId;
                    inspection.InspectionSessionDate =
                        DateTime.Parse(viewModel.LocationInspection.InspectionSessionDate);
                    inspection.CompanyCenterScheduleId = null;
                }
                else if (viewModel.InspectionTypeId == 2)
                {
                    inspection.CompanyCenterScheduleId = viewModel.CenterInspection.CompanyCenterScheduleId;
                    inspection.InspectionSessionDate = DateTime.Parse(viewModel.CenterInspection.InspectionSessionDate);
                    inspection.InspectionAddressId = null;
                    inspection.InspectionSessionId = null;
                }

                await _policyRequestInspectionRepository.UpdateAsync(inspection, cancellationToken);
            }

            return inspection;

        }

```

<div align="right" dir="rtl">

<br>

**دریافت تکی (سرویس Get)** : این سرویس متد `GetPolicyRequestHolderInspectionAsync(code, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

        public async Task<PolicyRequestInspectionResultViewModel> GetPolicyRequestHolderInspectionAsync(string code,
            CancellationToken cancellationToken)
        {
            Guid policyCode = Guid.Parse(code);
            DAL.Models.PolicyRequest policyRequest =
                await _policyRequestRepository.GetByCode(policyCode, cancellationToken);

            if (policyRequest == null)
            {
                throw new BadRequestException("کد درخواست بیمه وجود ندارد");
            }


            PolicyRequestInspection inspection;
            inspection = await _policyRequestInspectionRepository.GetByPolicyRequestCodeNoTracking(policyCode,
                cancellationToken);


            return _mapper.Map<PolicyRequestInspectionResultViewModel>(inspection);
        }



```


<div align="right" dir="rtl">

این ها نمونه های کنترلر policy بودند. سایر سرویس ها دقیقا با همین مکانیزم پیاده شده اند و تنها تفاوت اعتبار سنجی ها می باشد.

</div>


