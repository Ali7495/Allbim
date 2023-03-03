<div dir="rtl" align="right">

# تشریح سرویس های نقش مدیر سیستم

بسیاری از سرویس های این نقش که با `company/{code}` شروع می شدند در [سرویس های شرکت](./companyServices.md) توضیح داده شدند. بنابر این به سایر سرویس های مدیر سیستم می پردازیم.


* آدرس  `payment` با متد Get جهت دریافت اطلاعات تراکنش ها و فاکتور و جزئیات آن  ها سرویس زیر را در کنترلر Payment فراخوانی می کند:

`GetAllPayments(companyCode, statusId, pageAbleResult, cancellationToken)`

ورودی های مهم آن به شرح زیر است:

statuId : آیدی وضعیت پرداختی تراکنش که بصورت nullable میباشد

companyCode: کد انحصاری شرکت مورد نظر که از url خوانده می شود

<br>

بدنه این سرویس بصورت زیر می باشد و باتوجه به وضعیت ارسالی متد مناسب را از ریپازیتوری مدل فراخوانی میکند:

</div>

```C#

 public async Task<PagedResult<PaymentFactorViewModel>> GetAllPayments(Guid? companyCode, long? statusId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)
        {
            PageAbleModel pageAbleModel = _mapper.Map<PageAbleModel>(pageAbleResult);

            PagedResult<PolicyRequestFactor> factors = new PagedResult<PolicyRequestFactor>();

            if (statusId != null && companyCode != null)
            {
                factors = await _policyRequestFactorRepository.GetAllFactorsByAllParameters(companyCode.Value, statusId.Value, pageAbleModel, cancellationToken);
            }
            else if (companyCode != null)
            {
                factors = await _policyRequestFactorRepository.GetAllFactorsByCompany(companyCode.Value, pageAbleModel, cancellationToken);
            }
            else if (statusId != null)
            {
                factors = await _policyRequestFactorRepository.GetAllFactorsByStatus(statusId.Value, pageAbleModel, cancellationToken);
            }
            else
            {
                factors = await _policyRequestFactorRepository.GetAllFactors(pageAbleModel, cancellationToken);
            }

            return _mapper.Map<PagedResult<PaymentFactorViewModel>>(factors);
        }

```

<br>

<div dir="rtl" align="right">

* آدرس های زیر تماما برای انجام عملیات CRUD جداول PaymentGateway و PaymentStatus می باشد که جزئیات آن مانند سایر موارد است و قبلا ذکر شده:

`payment/paymentGateway` **Post** جهت درج داده

`payment/paymentGateway` **Get** جهت دریافت تمام داده ها

`payment/paymentGateway/list` **Get** جهت دریافت لیستی از داده ها

`payment/paymentGateway/{gatewayId}` **Get** جهت دریافت تک داده مورد نظر

`payment/paymentGateway/{gatewayId}` **Put** جهت ویرایش داده

`payment/paymentGateway/{gatewayId}` **Delete** جهت حذف داده

<br>

`payment/paymentStatus` **Post** جهت درج داده

`payment/paymentStatus` **Get** جهت دریافت تمام داده ها

`payment/paymentStatus/list` **Get** جهت دریافت لیستی از داده ها

`payment/paymentStatus/{statusId}` **Get** جهت دریافت تک داده مورد نظر

`payment/paymentStatus/{statusId}` **Put** جهت ویرایش داده

`payment/paymentStatus/{statusId}` **Delete** جهت حذف داده

</div>