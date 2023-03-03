<div dir="rtl" align="right">

# ساختار api

ساختار api های یک موجودیت بصورت زیر قرارداد شده است:
- POST: جهت درج موجودیت
- PUT {id}: جهت بروزرسانی کلی موجودیت(تمام فیلدهای آن)
- PATCH {id}: جهت بروزرسانی جزئی یک یا چند فیلد از آن موجودیت
- DELETE {id}: حذف یک موجودیت
- GET: جهت دریافت لیست صفحه بندی شده یک موجودیت
- GET '/list': جهت دریافت لیست موجودیت ها بدون صفحه بندی
- GET {id}: جهت دریافت یک موجودیت

## قوانین:
-بعضی موجودیت ها بعلت رعایت محرمانگی و عدم حدس رکورد قبلی یا بعدی از روی id ، بعضی از موجودیت ها با استفاده از {code} قابل دریافت است. مانند موجودیت های Company, PolicyRequest, User, Person, Attachment, Address. همچنین در ویومدل های مربوط به این موجودیت ها، شناسه id برگردانده نمی شود
- برای رابطه های 1 به n، مسیردهی موجودیت فرزند در ادامه والد می آید مانند Person/{code}/Address
- برای رابطه های m به n مسیریابی بصورت ترکیبی از موجودیت های درگیر بهمراه شناسه آنها می باشد مانند: Company/{code}/Person/{code}

## نمونه کد
<div dir="ltr" align="left">

```c#
[HttpPost("")]  
public async Task<ApiResult<PublicFaqResultViewModel>> Create([FromBody] PublicFaqInputViewModel CreateViewModel, CancellationToken cancellationToken)  
{  
  return await _publicFaqService.Create(CreateViewModel, cancellationToken);  
}  

[HttpDelete("{id}")]  
public async Task<ApiResult<string>> Delete(long id, CancellationToken cancellationToken)  
{  
  bool res = await _publicFaqService.Delete(id, cancellationToken);  
  return res.ToString();  
}  

[HttpPut("{id}")]  
public async Task<ApiResult<PublicFaqResultViewModel>> Update(long id, [FromBody] PublicFaqInputViewModel UpdateViewModel, CancellationToken cancellationToken)  
{  
  return await _publicFaqService.Update(id, UpdateViewModel, cancellationToken);  
}  

[HttpGet("{id}")]  
[AllowAnonymous]  
public async Task<ApiResult<PublicFaqResultViewModel>> GetDetail(long id, CancellationToken cancellationToken)  
{  
  return await _publicFaqService.Detail(id, cancellationToken);  
}  
  
[HttpGet("list")]  
[AllowAnonymous]  
public async Task<ApiResult<List<PublicFaqResultViewModel>>> GetAllWithoutPaging(CancellationToken cancellationToken)  
{  
  return await _publicFaqService.GetAllWithoutPaging(cancellationToken);  
}  
  
[HttpGet("")]  
[AllowAnonymous]  
public async Task<ApiResult<PagedResult<PublicFaqResultViewModel>>> GetAllPaging([FromQuery] PageAbleResult pageAbleResult,CancellationToken cancellationToken)  
{  
  return await _publicFaqService.GetAllPaging(pageAbleResult,cancellationToken);  
} 
```