<div align="right" dir="rtl">

عملیات CRUD جدول Reminder در کنترلر Reminder بصورت زیر پیاده سازی شده. 

>*  توصیه می شود قبل از دیدن کد ها  [مفاهیم مشترک و پایه ای](../common/CommonStructure.md) را مطالعه فرمایید*

</div>

```C#

       [HttpPost()]
        public async Task<ApiResult<ReminderResultViewModel>> Create([FromBody] ReminderInputViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = await _reminderService.Create(viewModel, cancellationToken);

            return model;
        }

        [HttpGet()]
        [AllowAnonymous]
        public async Task<ApiResult<List<ReminderResultViewModel>>> GetAllReminder(CancellationToken cancellationToken)
        {
            var model = await _reminderService.GetAllReminder(cancellationToken);

            return model;
        }

        [HttpPut("{id}")]
        public async Task<ApiResult<ReminderResultViewModel>> Update(long id, [FromBody] ReminderInputViewModel viewModel, CancellationToken cancellationToken)
        {
            var model = await _reminderService.Update(id, viewModel, cancellationToken);
            return model;
        }

         [HttpGet("{id}")]
        public async Task<ApiResult<ReminderResultViewModel>> GetDetail(long id, CancellationToken cancellationToken)
        {
            var model = await _reminderService.detail(id, cancellationToken);
            return model;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResult<string>> Delete(long id, CancellationToken cancellationToken)
        {
            var res = await _reminderService.Delete(id, cancellationToken);
            return res.ToString();
        }

         [AllowAnonymous]
        [HttpGet("Period")]
        public async Task<ApiResult<List<ReminderPeriodResultViewModel>>> GetAllReminderPeriod(CancellationToken cancellationToken)
        {
            var model = await _reminderService.getAllPeriod( cancellationToken);

            return model;
        }


```

<div align="right" dir="rtl">

**درج (سرویس Post)** : این سرویس متد `Create(viewModel, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

        public async Task<ReminderResultViewModel> Create(ReminderInputViewModel viewModel, CancellationToken cancellationToken)
        {

            Reminder model = new Reminder()
            {
                DueDate = viewModel.DueDate,
                CityId = viewModel.CityId,
                Description = viewModel.Description,
                InsuranceId = viewModel.InsuranceId,
                ReminderPeriodId = viewModel.ReminderPeriodId,

            };
            await _reminderRepository.AddAsync(model, cancellationToken);
            return _mapper.Map<ReminderResultViewModel>(model);


        }


```


<div align="right" dir="rtl">

ویومدل ورودی این سرویس : 

</div>

```C#

  public class ReminderInputViewModel
    {
        [JsonPropertyName("insurance_id")]

        public long? InsuranceId { get; set; }
        [JsonPropertyName("reminderPeriod_id")]

        public long? ReminderPeriodId { get; set; }
        [JsonPropertyName("description")]

        public string Description { get; set; }
        [JsonPropertyName("dueDate")]

        public DateTime? DueDate { get; set; }
        [JsonPropertyName("city_id")]

        public long? CityId { get; set; }

    }

```



<div align="right" dir="rtl">

**ویرایش (سرویس Put)** : این سرویس متد `Update(id, viewModel, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

public async Task<ReminderResultViewModel> Update(long id, ReminderInputViewModel viewModel, CancellationToken cancellationToken)
        {
            using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var ReminderData = await _reminderRepository.GetReminderByID(id, cancellationToken);
                    if (ReminderData == null)
                        throw new BadRequestException("داده یافت نشد");
                    ReminderData.Description = viewModel.Description;
                    ReminderData.CityId = viewModel.CityId;
                    ReminderData.DueDate = viewModel.DueDate;
                    ReminderData.InsuranceId = viewModel.InsuranceId;
                    ReminderData.ReminderPeriodId = viewModel.ReminderPeriodId;
                    await _reminderRepository.UpdateAsync(ReminderData, cancellationToken);
                    transaction.Complete();
                    return _mapper.Map<ReminderResultViewModel>(ReminderData);
            }
        }

```

<div align="right" dir="rtl">
پس از اعتبار سنجی موجودیت ها، تمتم جداول درگیر در درج ویرایش شدند.

<br>

**دریافت تکی (سرویس Get)** : این سرویس متد `detail(id, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>

```C#

        public async Task<ReminderResultViewModel> detail(long id, CancellationToken cancellationToken)
        {
            try
            {
                var ReminderData = await _reminderRepository.GetReminderByID(id, cancellationToken);
                if (ReminderData == null)
                    throw new CustomException("موردی یافت نشد");
                return _mapper.Map<ReminderResultViewModel>(ReminderData);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }


```


<div align="right" dir="rtl">

**دریافت کلی (سرویس Get)** : این سرویس متد `GetAllReminder(cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

public async Task<List<ReminderResultViewModel>> GetAllReminder(CancellationToken cancellationToken)
        {
            var model = await _reminderRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ReminderResultViewModel>>(model);
        }


```


<div align="right" dir="rtl">

**دریافت تمام دوره های (سرویس Get)** : این سرویس متد `getAllPeriod( cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

ublic async Task<List<ReminderPeriodResultViewModel>> getAllPeriod(CancellationToken cancellationToken)
        {
            var model = await _reminderPeriodReposity.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ReminderPeriodResultViewModel>>(model);
        }

```




<div align="right" dir="rtl">

**حذف (سرویس Delete)** : این سرویس متد `Delete(id, cancellationToken)` را فراخوانی می کند که به شرح زیر است:

</div>


```C#

public async Task<bool> Delete(long id, CancellationToken cancellationToken)
        {
            var ReminderData = await _reminderRepository.GetByIdAsync(cancellationToken,id);
            if (ReminderData == null)
                throw new CustomException("داده یافت نشد");
            await _reminderRepository.DeleteAsync(ReminderData, cancellationToken);
            return true;
        }



```

