<div align="right" dir="rtl">

عملیات CRUD جدول InsuranceCentralRule بصورت زیر پیاده سازی شده. این عملیات ها در کنترلر Insurance قرار دارند چراکه هر شرکت بیمه باید قوانین خود را تعریف کند. بنا بر این قبل از تمام آدرس ها باید /insurance بیاید.

به این سرویس ها فقط مدیر سیستم دسترسی دارد به این دلیل که این ها قوانین بیمه مرکزی هستند و یکبار برای همه تعیین می شوند.

>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*

</div>

```C#

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

```

<div align="right" dir="rtl">

**درج (سرویس Post)** : این سرویس متد CreateInsuranceCenteralRule(insuranceId, insuranceCentralRule, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>

```C#

        public async Task<InsuranceCentralRuleResultViewModel> CreateInsuranceCenteralRule(long insuranceId, InsuranceCentralRuleInputViewModel insuranceViewModel, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه مورد نظر وجود ندارد");
            }

            CentralRuleType centralRuleType = await _centralRuleTypeRepository.GetByIdAsync(cancellationToken, insuranceViewModel.CentralRuleTypeId);
            if (centralRuleType == null)
            {
                throw new BadRequestException("نوع قانون مورد نظر وجود ندارد");
            }

            InsuranceCentralRule insuranceCentralRule = new InsuranceCentralRule()
            {
                CentralRuleTypeId = insuranceViewModel.CentralRuleTypeId,
                CalculationTypeId = insuranceViewModel.CalculationTypeId,
                ConditionTypeId = insuranceViewModel.ConditionTypeId,
                Discount = insuranceViewModel.Discount,
                //FieldId = "Field",
                FieldType = " ",
                IsCumulative = insuranceViewModel.IsCumulative,
                PricingTypeId = insuranceViewModel.PricingTypeId,
                //Type = 0,
                JalaliYear = insuranceViewModel.JalaliYear,
                GregorianYear = insuranceViewModel.GregorianYear,
                Value = insuranceViewModel.Value
            };

            await _insuranceCenteralRuleRepository.AddAsync(insuranceCentralRule, cancellationToken);

            return _mapper.Map<InsuranceCentralRuleResultViewModel>(insuranceCentralRule);
        }


```


<div align="right" dir="rtl">

همان طور که واضح است ابتدا موجودیت برخی جداول بر اساس اطلاعات ارسالی چک می شود. و نهایتا ویومدل ورودی به مدل اصلی مپ شده و در جدول مربوطه درج می شود.

<br>

**ویرایش (سرویس Put)** : این سرویس متد CentralRule(insuranceId, RuleId, insuranceCentralRule, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>

```C#

public async Task<InsuranceCentralRuleResultViewModel> CentralRule(long insuranceId, long RuleId, InsuranceCentralRuleInputViewModel insuranceCentralRule, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه مورد نظر وجود ندارد");
            }

            CentralRuleType centralRuleType = await _centralRuleTypeRepository.GetByIdAsync(cancellationToken, insuranceCentralRule.CentralRuleTypeId);
            if (centralRuleType == null)
            {
                throw new BadRequestException("نوع قانون مورد نظر وجود ندارد");
            }

            InsuranceCentralRule model = await _insuranceCenteralRuleRepository.GetByIdAsync(cancellationToken, RuleId);
            if (model == null)
                throw new BadRequestException("این قانون وجود ندارد");

            model.CentralRuleTypeId = insuranceCentralRule.CentralRuleTypeId;
            model.Value = insuranceCentralRule.Value;
            model.JalaliYear = insuranceCentralRule.JalaliYear;
            model.GregorianYear = insuranceCentralRule.GregorianYear;
            model.CalculationTypeId = insuranceCentralRule.CalculationTypeId;
            model.ConditionTypeId = insuranceCentralRule.ConditionTypeId;
            model.Discount = insuranceCentralRule.Discount;
            //model.FieldId = "Field";
            model.IsCumulative = insuranceCentralRule.IsCumulative;
            model.PricingTypeId = insuranceCentralRule.PricingTypeId;


            await _insuranceCenteralRuleRepository.UpdateAsync(model, cancellationToken);
            return _mapper.Map<InsuranceCentralRuleResultViewModel>(model);
        }


```

<div align="right" dir="rtl">

همان طور که واضح است ابتدا موجودیت برخی جداول بر اساس اطلاعات ارسالی چک می شود. و نهایتا مقادیر ویومدل داخل مدل اصلی گذاشته شده و آپدیت می شود

<br>

**دریافت تکی (سرویس Get)** : این سرویس متد GetInsuranceCenteralRule(insuranceId, RuleId, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>

```C#
public async Task<InsuranceCentralRuleResultViewModel> GetInsuranceCenteralRule(long insuranceId, long roleId, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه مورد نظر وجود ندارد");
            }

            InsuranceCentralRule model = await _insuranceCenteralRuleRepository.GetRuleByInsuranceIdAndId(insuranceId,roleId,cancellationToken);

            return _mapper.Map<InsuranceCentralRuleResultViewModel>(model);
        }

```


<div align="right" dir="rtl">

**دریافت کلی (سرویس Get)** : این سرویس متد GetAllInsuranceCenteralRules(insuranceId, pageAbleResult, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>


```C#

public async Task<PagedResult<InsuranceCentralRuleResultViewModel>> GetAllInsuranceCenteralRules(long insuranceId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه مورد نظر وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<InsuranceCentralRule> centralRules = await _insuranceCenteralRuleRepository.GetAllCentalRules(insuranceId, pageAbleModel, cancellationToken);


            return _mapper.Map<PagedResult<InsuranceCentralRuleResultViewModel>>(centralRules);
        }

```

<div align="right" dir="rtl">

پس از بررسی موجودیت ها اطلاعات دریافتی بر اساس صفحه بندی (Paging) مپ شده و برگردانده می شوند.

<br>

**حذف (سرویس Delete)** : این سرویس متد DeleteInsuranceCenteralRule(insuranceId, RuleId, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>


```C#

public async Task<bool> DeleteInsuranceCenteralRule(long insuranceId, long RuleId, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه مورد نظر وجود ندارد");
            }

            InsuranceCentralRule model = await _insuranceCenteralRuleRepository.GetByIdAsync(cancellationToken, RuleId);
            if (model == null)
                throw new BadRequestException("این قانون وجود ندارد");


            await _insuranceCenteralRuleRepository.DeleteAsync(model, cancellationToken);
            return true;
        }

```
