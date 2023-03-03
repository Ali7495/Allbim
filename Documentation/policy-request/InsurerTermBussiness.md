<div align="right" dir="rtl">

عملیات CRUD جدول InsurerTerm بصورت زیر پیاده سازی شده. این عملیات ها در کنترلر Company قرار دارند چراکه هر شرکت بیمه باید قوانین خود را تعریف کند. بنا بر این قبل از تمام آدرس ها باید /company بیاید.

>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*

</div>

```C#

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

```

<div align="right" dir="rtl">

**درج (سرویس Post)** : این سرویس متد CreateInsurerTerm(code, insuranceId, insurerTermViewModel, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>

```C#

        public async Task<InsurerTermDetailedResultViewModel> CreateInsurerTerm(Guid code, long insuranceId, InsurerTermInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد ");
            }

            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه وجود ندارد");
            }

            Insurer insurer = await _insurerRepository.GetWithInsuranceIdAndCompanyCodeNoTracking(code, insurance.Id, cancellationToken);
            if (insurer == null)
            {
                throw new BadRequestException("بیمه گر وجود ندارد");
            }


            InsurerTerm insurerTerm = _mapper.Map<InsurerTerm>(viewModel);
            insurerTerm.InsurerId = insurer.Id;


            await _insurerTermRepository.AddAsync(insurerTerm, cancellationToken);

            return _mapper.Map<InsurerTermDetailedResultViewModel>(insurerTerm);
        }


```


<div align="right" dir="rtl">

همان طور که واضح است ابتدا موجودیت برخی جداول بر اساس اطلاعات ارسالی چک می شود. و نهایتا ویومدل ورودی به مدل اصلی مپ شده و در جدول مربوطه درج می شود.

<br>

**ویرایش (سرویس Put)** : این سرویس متد UpdateInsurerTermAsync(code, insuranceId, id, insurerTermViewModel, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>

```C#

public async Task<InsurerTermDetailedResultViewModel> UpdateInsurerTermAsync(Guid code, long insuranceId, long id, InsurerTermInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه وجود ندارد");
            }

            Insurer insurer = await _insurerRepository.GetWithInsuranceIdAndCompanyCodeNoTracking(code, insurance.Id, cancellationToken);
            if (insurer == null)
            {
                throw new BadRequestException("بیمه گر وجود ندارد");
            }

            InsurerTerm insurerTerm = await _insurerTermRepository.GetByIdAsync(cancellationToken, id);

            insurerTerm.InsuranceTermTypeId = viewModel.InsuranceTermTypeId;
            insurerTerm.Value = viewModel.Value;
            insurerTerm.Discount = viewModel.Discount;
            insurerTerm.IsCumulative = viewModel.IsCumulative;
            insurerTerm.ConditionTypeId = viewModel.ConditionTypeId;


            await _insurerTermRepository.UpdateAsync(insurerTerm, cancellationToken);

            return _mapper.Map<InsurerTermDetailedResultViewModel>(insurerTerm);
        }


```

<div align="right" dir="rtl">

همان طور که واضح است ابتدا موجودیت برخی جداول بر اساس اطلاعات ارسالی چک می شود. و نهایتا مقادیر ویومدل داخل مدل اصلی گذاشته شده و آپدیت می شود

<br>

**دریافت تکی (سرویس Get)** : این سرویس متد GetInsurerTerm(id, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>

```C#

            InsurerTerm model = await _insurerTermRepository.GetWithDetailsById(id,cancellationToken);
            if (model == null)
                throw new BadRequestException("مقررات بیمه گر وجود ندارد");
            return _mapper.Map<InsurerTermDetailedResultViewModel>(model);

```


<div align="right" dir="rtl">

**دریافت کلی (سرویس Get)** : این سرویس متد GetAllInsurerTerms(code, insuranceId, pageAbleResult, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>


```C#

public async Task<PagedResult<InsurerTermResultViewModel>> GetAllInsurerTerms(Guid code, long insuranceId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            Insurance insurance = await _insuranceRepository.GetByIdAsync(cancellationToken, insuranceId);
            if (insurance == null)
            {
                throw new BadRequestException("بیمه وجود ندارد");
            }

            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<InsurerTerm> insurerTerms = await _insurerTermRepository.GetAllInsurerTerms(insuranceId, pageAbleModel, cancellationToken);
            return _mapper.Map<PagedResult<InsurerTermResultViewModel>>(insurerTerms);
        }

```

<div align="right" dir="rtl">

پس از بررسی موجودیت ها اطلاعات دریافتی بر اساس صفحه بندی (Paging) مپ شده و برگردانده می شوند.

<br>

**حذف (سرویس Delete)** : این سرویس متد DeleteInsurerTerm(id, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>


```C#

public async Task<bool> DeleteInsurerTerm(long id, CancellationToken cancellationToken)
        {
            InsurerTerm insurerTerm = await _insurerTermRepository.GetByIdAsync(cancellationToken, id);
            if (insurerTerm == null)
                throw new BadRequestException("مقررات وجود ندارد");

            await _insurerTermRepository.DeleteAsync(insurerTerm, cancellationToken);
            return true;
        }


```

<div align="right" dir="rtl">

سرویس های mine فرایند های InsurerTerm نیز به همین منوال است که تفاوت های آن در مفاهیم مشترک و پایه ای آمده است
<br>