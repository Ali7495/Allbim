<div align="right" dir="rtl">

عملیات CRUD جدول InsurerTermDetail بصورت زیر پیاده سازی شده. این عملیات ها در کنترلر Company قرار دارند چراکه هر شرکت بیمه باید جزئیات قوانین خود را تعریف کند. بنا بر این قبل از تمام آدرس ها باید /company بیاید.

>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*

</div>

```C#

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

```

<div align="right" dir="rtl">

**درج (سرویس Post)** : این سرویس متد CreateInsurerTermDetail(code, insuranceId, insurerTermId, termDetailInputView, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>

```C#

        public async Task<TermDetailResultViewModel> CreateInsurerTermDetail(Guid code, long insuranceId, long termId, TermDetailInputViewModel viewModel, CancellationToken cancellationToken)
        {
            await IsDataValidCommon(code, insuranceId, termId, cancellationToken);

            InsurerTermDetail termDetail = _mapper.Map<InsurerTermDetail>(viewModel);

            termDetail.InsurerTermId = termId;

            await _insurerTermDetailRepository.AddAsync(termDetail, cancellationToken);

            return _mapper.Map<TermDetailResultViewModel>(termDetail);
        }




```


<div align="right" dir="rtl">

همان طور که واضح است ابتدا موجودیت برخی جداول بر اساس اطلاعات ارسالی چک می شود. و نهایتا ویومدل ورودی به مدل اصلی مپ شده و در جدول مربوطه درج می شود.

متدی که این بررسی را انجام می دهد:

</div>


```C#

public async Task<bool> IsDataValidCommon(Guid code, long insuranceId, long termId, CancellationToken cancellationToken)
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

            InsurerTerm insurerTerm = await _insurerTermRepository.GetByIdAsync(cancellationToken, termId);
            if (insurerTerm == null)
            {
                throw new BadRequestException("قانوین بیمه گر وجود ندارد");
            }

            return true;
        }

```


<br>

**ویرایش (سرویس Put)** : این سرویس متد UpdateInsurerTermDetailAsync(code, insuranceId, insurerTermId, detailId, termDetailInputView, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>

```C#

public async Task<TermDetailResultViewModel> UpdateInsurerTermDetailAsync(Guid code, long insuranceId, long termId, long detailId, TermDetailInputViewModel viewModel, CancellationToken cancellationToken)
        {
            await IsDataValidCommon(code, insuranceId, termId, cancellationToken);

            InsurerTermDetail insurerTermDetail = await GetDetialCommon(detailId, cancellationToken);

            insurerTermDetail = _mapper.Map<InsurerTermDetail>(viewModel);
            insurerTermDetail.Id = detailId;
            insurerTermDetail.InsurerTermId = termId;

            await _insurerTermDetailRepository.UpdateAsync(insurerTermDetail, cancellationToken);

            return _mapper.Map<TermDetailResultViewModel>(insurerTermDetail);
        }


```

<div align="right" dir="rtl">

همان طور که واضح است ابتدا موجودیت برخی جداول بر اساس اطلاعات ارسالی چک می شود. و نهایتا مقادیر ویومدل داخل مدل اصلی گذاشته شده و آپدیت می شود

<br>

**دریافت تکی (سرویس Get)** : این سرویس متد GetInsurerTermDetail(code, insuranceId, insurerTermId, detailId, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>

```C#

            public async Task<TermDetailResultViewModel> GetInsurerTermDetail(Guid code, long insuranceId, long insurerTermId, long detailId, CancellationToken cancellationToken)
        {
            await IsDataValidCommon(code, insuranceId, insurerTermId, cancellationToken);

            InsurerTermDetail insurerTermDetail = await GetDetialCommon(detailId, cancellationToken);

            return _mapper.Map<TermDetailResultViewModel>(insurerTermDetail);
        }






        public async Task<InsurerTermDetail> GetDetialCommon(long id, CancellationToken cancellationToken)
        {
            InsurerTermDetail insurerTermDetail = await _insurerTermDetailRepository.GetTermDetailNoTracking(id, cancellationToken);
            if (insurerTermDetail == null)
            {
                throw new BadRequestException("جزئیات قانون بیمه گر وجود ندارد");
            }

            return insurerTermDetail;
        }

```


<div align="right" dir="rtl">

**دریافت کلی (سرویس Get)** : این سرویس متد GetAllInsurerTermDetails(code, insuranceId, insurerTermId, pageAbleResult, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>


```C#

public async Task<PagedResult<TermDetailResultViewModel>> GetAllInsurerTermDetails(Guid code, long insuranceId, long insurerTermId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            await IsDataValidCommon(code, insuranceId, insurerTermId, cancellationToken);

            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<InsurerTermDetail> insurerTermDetails = await _insurerTermDetailRepository.GetAllTermDetails(insurerTermId, pageAbleModel, cancellationToken);

            return _mapper.Map<PagedResult<TermDetailResultViewModel>>(insurerTermDetails);
        }

```

<div align="right" dir="rtl">

پس از بررسی موجودیت ها اطلاعات دریافتی بر اساس صفحه بندی (Paging) مپ شده و برگردانده می شوند.

<br>


<div align="right" dir="rtl">

**دریافت لیست (سرویس Get)** : این سرویس متد GetAllInsurerTermDetailList(code, insuranceId, insurerTermId, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>


```C#
public async Task<List<TermDetailResultViewModel>> GetAllInsurerTermDetailList(Guid code, long insuranceId, long insurerTermId, CancellationToken cancellationToken)
        {
            await IsDataValidCommon(code, insuranceId, insurerTermId, cancellationToken);

            List<InsurerTermDetail> details = await _insurerTermDetailRepository.GetAllTermDetailList(insurerTermId, cancellationToken);

            return _mapper.Map<List<TermDetailResultViewModel>>(details);
        }

```



**حذف (سرویس Delete)** : این سرویس متد DeleteInsurerTermDetailAsync(code, insuranceId, insurerTermId, detailId, cancellationToken) را فراخوانی می کند که به شرح زیر است:

</div>


```C#

 public async Task<bool> DeleteInsurerTermDetailAsync(Guid code, long insuranceId, long termId, long detailId, CancellationToken cancellationToken)
        {
            await IsDataValidCommon(code, insuranceId, termId, cancellationToken);

            InsurerTermDetail insurerTermDetail = await GetDetialCommon(detailId, cancellationToken);

            await _insurerTermDetailRepository.DeleteAsync(insurerTermDetail, cancellationToken);

            return true;
        }

```

<div align="right" dir="rtl">

سرویس های mine فرایند های InsurerTermDetail نیز به همین منوال است که تفاوت های آن در مفاهیم مشترک و پایه ای آمده است
<br>