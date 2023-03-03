<div dir="rtl" align="right">

# سرویس های تماس با ما
بطور کلی سرویس های تماس با ما به دو قسمت سمت مخاطب و سمت مدیریت تقسیم می شود

## سمت مخاطب

- سرویس درج تماس با ما
<div dir="ltr" align="left">

```c#

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ApiResult<ContactUsFrontResultViewModel>> Create([FromBody] ContactUsInputPostViewModel ContactUsInputPostViewModel, CancellationToken cancellationToken)
        {
            return await _contactUsService.create(ContactUsInputPostViewModel, cancellationToken);
        }

```
</div>
خروجی این سرویس، ویومدل ContactUsFrontResultViewModel می باشد که فقط فیلد id را ندارد.

## سمت مدیریت
- ساختار سرویس های CRUD که در مستندات مشترک توضیح داده شده است([ساختارهای مشترک طراحی api](../common/CommonStructure.md))

- سرویس جواب به مخاطب که از نوع update می باشد.
<div dir="ltr" align="left">

```c#

 [HttpPut("{id}/answer")]
        public async Task<ApiResult<ContactUsDashboardResultViewModel>> Answer(long id, [FromBody] ContactUsInputPutViewModel ContactUsEditeViewModel, CancellationToken cancellationToken)
        {
            return await _contactUsService.Answer(id, ContactUsEditeViewModel, cancellationToken);
        }

```
</div>
در اینجا علاوه بر ویومدل های استاندارد، یک ویومدل مجزا با نام ContactUsInputPutViewModel داریم که فقط فیلد answer را از ورودی دریافت می کند.
