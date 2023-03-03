<div align="right" dir="rtl">

عملیات CRUD جدول CompanyCenter بصورت زیر پیاده سازی شده. این عملیات ها در کنترلر Company قرار دارند چراکه هر شرکت بیمه باید مرکز خود را تعریف کند. بنا بر این قبل از تمام آدرس ها باید /company بیاید.

>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*

</div>

```C#

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


```

<div align="right" dir="rtl">

**درج (سرویس Post)** : این سرویس متد `CreateCenter(code, centerInput, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

        public async Task<CompanyCenterResultViewModel> CreateCenter(Guid code, CompanyCenterInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            DAL.Models.CompanyCenter companyCenter = _mapper.Map<DAL.Models.CompanyCenter>(viewModel);
            companyCenter.CompanyId = company.Id;
            //await _companyCenterRepository.AddAsync(companyCenter, cancellationToken);
            await CreateCenterCommon(companyCenter, cancellationToken);
            return _mapper.Map<CompanyCenterResultViewModel>(companyCenter);
        }




        public async Task CreateCenterCommon(DAL.Models.CompanyCenter companyCenter , CancellationToken cancellationToken)
        {
            await _companyCenterRepository.AddAsync(companyCenter, cancellationToken);
        }

```


<div align="right" dir="rtl">

پس از بررسی موجودیت شرکت، ویومدل ورودی به مدل اصلی مپ شده و درج انجام میگیرد.

<br>

ویومدل ورودی این سرویس : 

</div>

```C#

 public class CompanyCenterInputViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("city_id")]
        public long CityId { get; set; }
    }

```



<div align="right" dir="rtl">

**ویرایش (سرویس Put)** : این سرویس متد `UpdateCenter(code, id, centerInput, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

 public async Task<CompanyCenterResultViewModel> UpdateCenter(Guid code, long id, CompanyCenterInputViewModel viewModel, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            DAL.Models.CompanyCenter companyCenter = await _companyCenterRepository.GetByIdAsync(cancellationToken, id);
            //companyCenter.Name = viewModel.Name;
            //companyCenter.Description = viewModel.Description;
            //companyCenter.CityId = viewModel.CityId;

            //await _companyCenterRepository.UpdateAsync(companyCenter, cancellationToken);
            await UpdateCenterCommon(companyCenter, viewModel, cancellationToken);
            return _mapper.Map<CompanyCenterResultViewModel>(companyCenter);
        }




 public async Task UpdateCenterCommon(DAL.Models.CompanyCenter companyCenter, CompanyCenterInputViewModel viewModel, CancellationToken cancellationToken)
        {
            companyCenter.Name = viewModel.Name;
            companyCenter.Description = viewModel.Description;
            companyCenter.CityId = viewModel.CityId;

            await _companyCenterRepository.UpdateAsync(companyCenter, cancellationToken);
        }

```

<div align="right" dir="rtl">

<br>

**دریافت تکی (سرویس Get)** : این سرویس متد `GetCenter(code, id, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

        public async Task<CompanyCenterResultViewModel> GetCenter(Guid code, long id, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            //DAL.Models.CompanyCenter companyCenter = await _companyCenterRepository.GetCentersWithAllData(id, cancellationToken);
            DAL.Models.CompanyCenter companyCenter = await GetCenterCommon(id, cancellationToken);
            return _mapper.Map<CompanyCenterResultViewModel>(companyCenter);
        }



        public async Task<DAL.Models.CompanyCenter> GetCenterCommon(long id, CancellationToken cancellationToken)
        {
            return await _companyCenterRepository.GetCentersWithAllData(id, cancellationToken);
        }

```


<div align="right" dir="rtl">

**دریافت کلی (سرویس Get)** : این سرویس متد `GetAllCenters(code, pageAbleResult, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

public async Task<PagedResult<CompanyCenterResultViewModel>> GetAllCenters(Guid code,PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);
            //PagedResult<DAL.Models.CompanyCenter> companyCenters = await _companyCenterRepository.GetAllCentersByCompanyCode(code,pageAbleModel, cancellationToken);
            PagedResult<DAL.Models.CompanyCenter> companyCenters = await GetAllCentersCommon(company.Code, pageAbleModel, cancellationToken);
            return _mapper.Map<PagedResult<CompanyCenterResultViewModel>>(companyCenters);
        }


        public async Task<PagedResult<DAL.Models.CompanyCenter>> GetAllCentersCommon(Guid code , PageAbleModel pageAbleModel , CancellationToken cancellationToken)
        {
            return await _companyCenterRepository.GetAllCentersByCompanyCode(code, pageAbleModel, cancellationToken);
        }

```


<div align="right" dir="rtl">

**دریافت مراکز بر اساس شهر (سرویس Get)** : این سرویس متد `GetCentersByCityAndCompanyCode(company_code, city_id, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

 public async Task<List<CompanyCenterViewModel>> GetCentersByCityAndCompanyCode(Guid code, long cityId, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            DAL.Models.City city = await _cityRepository.GetByIdAsync(cancellationToken, cityId);
            if (city == null)
            {
                throw new BadRequestException("شهر وجود ندارد");
            }

            List<DAL.Models.CompanyCenter> companyCenters = await _companyCenterRepository.GetCentersByCityAndCompayId(company.Id, cityId, cancellationToken);
            if (companyCenters == null)
            {
                throw new BadRequestException("مرکزی وجود ندارد");
            }

            List<CompanyCenterViewModel> companyCenterViewModels = _mapper.Map<List<CompanyCenterViewModel>>(companyCenters);

            
            companyCenterViewModels = GetCentersByCityAndCompanyCodeCommon(companyCenterViewModels, companyCenters);
            return companyCenterViewModels;
        }


 public List<CompanyCenterViewModel> GetCentersByCityAndCompanyCodeCommon(List<CompanyCenterViewModel> companyCenterViewModels , List<DAL.Models.CompanyCenter> companyCenters)
        {
            for (int i = 0; i < companyCenterViewModels.Count; i++)
            {
                List<CenterSessionDataViewModel> centerSessionData = new List<CenterSessionDataViewModel>();
                for (int j = 1; j <= 7; j++)
                {
                    centerSessionData.Add(new CenterSessionDataViewModel()
                    {
                        Date = DateTime.Today.AddDays(j).ToString("yyyy/MM/dd"),
                        JalaliDate = PersianDateTime.Now.AddDays(j).ToString("yyyy/MM/dd"),
                        DayName = PersianDateTime.Now.AddDays(j).ToString("dddd")
                    });
                }
                companyCenterViewModels[i].CenterSessionData = centerSessionData;
                companyCenterViewModels[i].CenterSessionData.ForEach(
                    c => c.CenterSchedules = _mapper.Map<List<CenterScheduleViewModel>>(companyCenters.Single(c => c.Id == companyCenterViewModels[i].Id).CompanyCenterSchedules));
            }
            return companyCenterViewModels;
        }



```




<div align="right" dir="rtl">

این سرویس پس از اعتبار سنجی و دریافت مراکز بر اساس شهر، حال برای آن ها تا هفته بعد از روز جاری زمان بندی روزانه و ساعتی میسازد.


**حذف (سرویس Delete)** : این سرویس متد `DeleteCenter(code, id, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

public async Task<string> DeleteCenter(Guid code, long id, CancellationToken cancellationToken)
        {
            Company company = await _companyRepository.GetByCodeNoTracking(code, cancellationToken);
            if (company == null)
            {
                throw new BadRequestException("شرکت وجود ندارد");
            }

            DAL.Models.CompanyCenter companyCenter = await _companyCenterRepository.GetCenterWithSchedules(id,cancellationToken);
            if (companyCenter == null )
            {
                throw new BadRequestException("مرکز وجود ندارد");
            }

            if (companyCenter.CompanyCenterSchedules.Count > 0)
            {
                throw new BadRequestException("مرکز دارای زمانبندی می باشد و قابل حذف نیست.");
            }

            //await _companyCenterRepository.DeleteAsync(companyCenter, cancellationToken);
            await DeleteCenterCommon(companyCenter, cancellationToken);
            return true.ToString();
        }


        public async Task DeleteCenterCommon(DAL.Models.CompanyCenter companyCenter , CancellationToken cancellationToken)
        {
            await _companyCenterRepository.DeleteAsync(companyCenter, cancellationToken);
        }

```

