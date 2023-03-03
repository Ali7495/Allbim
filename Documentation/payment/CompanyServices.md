<div dir="rtl" align="right">

# تشریح سرویس های نقش های شرکت

به طور کل نقش های شرکت شامل: مدیر شرکت، نماینده شرکت، کارشناس شرکت و کارشناس نمایندگی می باشد


* آدرس  `company/mine/policy-request/{policyCode}/PaymentInfo` با متد Get جهت دریافت اطلاعات درخواست بیمه و فاکتور قابل پرداخت آن سرویس زیر را در کنترلر Company فراخوانی می کند:

`GetCompanyPolicyRequestPaymentDetailsMine(userId, policyCode, cancellationToken)`

ورودی های مهم آن به شرح زیر است:

userId : آیدی کابر استفاده کننده از سیستم که در برنه این سرویس دریافت می شود

policyCode: کد انحصاری درخواست بیمه مورد نظر که از url خوانده می شود



در این سرویس ابتدا جهت برخی بررسی ها متد زیر صدا زده می شود :

`IsUserValidCommon(userId, cancellationToken)` : جهت بررسی وجود کاربری با آیدی دریافت شده

سپس بررسی می شود که درخواست مورد نظر وجود دارد و اگر وجود دارد به این شرکت مرتبط است یا خیر

و نهایتا سرویس `GetCompanyPaymentInfoCommon(policyRequest, cancellationToken)`  اطلاعات درخواست بیمه به همراه جزئیات آن درخواست و فاکتور و قیمت آن را بر میگرداند.



* آدرس `company/mine/policy-request/{policyCode}/factor` با متد Post جهت درج یک فاکتور به همراه جزئیات و پرداخت احتمالی برای شرکت استفاده می شود.

  این آدرس سرویس `CreatePaymentFactorMine(userId, policyCode, ViewModel, cancellationToken)` را فراخوانی می کند که ورودی های مهم آن مانند سرویس بالا هستند به جز ViewModel که مدل ورودی جهت درج داده است .

  در این سرویس بررسی می گردد که کاربر استفاده کننده از طرف همین شرکت باشد

  

  عملکرد این سرویس مشابه عملکرد سرویس با آدرس `company/{code}/policy-request/{policyCode}/factor` است با این   تفاوت که بررسی روی تطابق کاربر استفاده کننده و شرکت صورت نمی گیرد. این آدرس سرویس `CreatePaymentFactor(policyCode, ViewModel, cancellationToken)` را صدا میزند با همان ورودی ها اما بدون userId.

  هردو این سرویس ها با فارخوانی سرویس `CreatePaymentFactorCommon(code, policyCode, inputViewModel, cancellationToken)` عملیات درج را انجام می دهند.

  

> *برای مشاهده جزئیات درج این سرویس [عملیات درج فاکتور](./CreateFactorCommon.md) را مطالعه فرمایید*



* آدرس های `company/mine/factor` و `company/{code}/factor` با متد Get جهت دریافت تمام فاکتورهای یک شرکت به همراه جزئیات و پرداخت احتمالی برای شرکت استفاده می شود.

   های این آدرس ها سرویس `GetAllFactors(code, pageAbleResult, cancellationToken)` و `GetAllFactorsMine(userId, code, pageAbleResult, cancellationToken)` را فراخوانی می کند که ورودی های مهم آن مانند سرویس بالا هستند .

   >هر زمانی که mine استفاده می شود عملیات برای کاربر استفاده کننده انجام می شود

  >اگر {code} بود سرویس ها کاملا مشابه هستند فقط موارد از طریق آن کد دریافت می شوند 

  > تمام آدرس هایی که به factor یا factorId ختم می شوند، خروجی آن ها یر اساس مدل CompanyPolicyRequestFactorResultViewModel می باشد
  </div>
```public class CompanyPolicyRequestFactorResultViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("policy_request")]
        public CompanyPolicyRequestResultViewModel PolicyRequest { get; set; }
        [JsonProperty("payment")]
        public CompanyPaymentResultViewModel Payment { get; set; }
        [JsonProperty("policy_request_factor_details")]
        public List<CompanyFactorDetailResultViewModel> PolicyRequestFactorDetails { get; set; }
    }
```

<div dir="rtl" align="right">

* آدرس های `company/mine/policy-request/{policyCode}/factor` و `company/{code}/policy-request/{policyCode}/factor` با متد Get جهت دریافت تمام فاکتورهای یک شرکت با کد درخواست بیمه مشخص به همراه جزئیات و پرداخت احتمالی برای شرکت استفاده می شود.

این آدرس ها سرویس های `GetAllPolicyFactors(Guid code, Guid policyCode, PageAbleResult pageAbleResult, CancellationToken cancellationToken)` و `GetAllPolicyFactorsMine(long userId, Guid policyCode, PageAbleResult pageAbleResult, CancellationToken cancellationToken)` را فراخوانی می کند که ورودی های مهم آن مانند سرویس بالا هستند .
 
 >همواره در سرویس های mine بررسی می گردد که کاربر استفاده کننده به شرکت مرتبط هست یا نه
 
 >در سرویس های {code} صرفا وجود شرکت مورد نظر بررسی می گردد.

 > در تمام سرویس های دارای policyCode وجود درخواست بیمه مورد نظر بررسی می گردد.

>*برای مشاهده نمونه ای از کد سرویس های Get و بررسی validation ها [نحوه Get ](./GetValidationProcess.md) را مطالعه فرمایید*

 
 * آدرس های `company/mine/policy-request/{policyCode}/factor/{factorId}` و `company/{code}/policy-request/{policyCode}/factor/{factorId}` اگر با متد Get فراخوانی شوند فاکتور مورد نظر را دریافت می کند به همراه جزئیات آن، اما اگر با متد Delete صدا زده شوند آنگاه آن فاکتور حذف خواهد شد.

این آدرس ها سرویس های `GetCompanyPolicyFactor(code, policyCode, factorId, cancellationToken)` و `GetCompanyPolicyFactorMine(userId, policyCode, factorId, cancellationToken)` و `DeleteFactor(code,policyCode, factorId, cancellationToken)` و
`DeleteFactorMine(userId,policyCode, factorId, cancellationToken)` را جهت عملیات مذکور استفاده می کنند
 

 >تمام مکانیزم های شرح شده در بالا برای سرویس های زیر هم که جهت انجام همان عملیات ها برای جزئیات فاکتور است یکسان است با مدل های متفاوت

 >اگر قبل از *company/mine* ، policy-request  بیاید برای شرکت استفاده کننده است

 >اگر قبل از *company/{code}* ، policy-request  بیاید برای مدیر سیستم است

 `policy-request/{policyCode}/factor/{factorId}/detail`

 `policy-request/{policyCode}/factor/{factorId}/detail/{detailId}`

 `policy-request/{policyCode}/factor/{factorId}/detail`

 `policy-request/{policyCode}/factor/{factorId}/detail/{detailId}`

 `policy-request/{policyCode}/factor/{factorId}/detail/{detailId}`

 تنها تفاوت عملیات مربوط به جزئیات و خود فاکتور در این است که زمانی که جزئیات ویرایش یادرج می شوند، درصورتی که تراکنشی ثبت شده باشد، مبلغ آن تراکنش نسبت به مجموع مقادیر موجود در جزئیات فاکتور تغییر میکند. نمونه زیر شرح ویرایش آن است :
</div>

```C#
using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                DAL.Models.PolicyRequest policyRequest = await CheckCompanyAndPolicyValidationCommon(companyCode, policyCode, cancellationToken);

                PolicyRequestFactor factor = await CheckIfFactorPaid(factorId, cancellationToken);

                PolicyRequestFactorDetail detail = await _policyRequestFactorDetailRepository.GetByIdNoTracking(detailId, cancellationToken);

                decimal price = await CalculateUpdatedPriceCommon(inputViewModel, factor.Payment.Price, detail.Amount);

                detail.Description = inputViewModel.Description;
                detail.Amount = inputViewModel.Amount;
                detail.CalculationTypeId = inputViewModel.CalculationTypeId;

                factor.PolicyRequestFactorDetails = null;



                Payment payment = factor.Payment;
                payment.Price = price;
                payment.UpdatedDateTime = DateTime.Now;

                await _policyRequestFactorDetailRepository.UpdateAsync(detail, cancellationToken);
                await _paymentRepository.UpdateAsync(payment, cancellationToken);

                transaction.Complete();

                return _mapper.Map<CompanyFactorDetailResultViewModel>(detail);

            }
```

<div dir="rtl" align="right">

که این عملیات در قالب متد `UpdateFactorDetailCommon(code, policyCode, factorId, detailId, inputViewModel, cancellationToken)` انجام می شود.

فرایند مشابه برای درج جزئیات فاکتور نیز برقرار است در متدی دیگر با نام `AddFactorDetailCommon(code, policyCode, factorId, inputViewModel, cancellationToken)`


و در انتها سرویس `company/mine/payment` که تمام تراکنش ها را با توجه به وضعیت انتخابی از سوی کاربران دریافت می کند.
سرویس آن به این صورت است `GetAllPaymentsMine(userId, statusId, pageAbleResult, cancellationToken)` که پارامتر **statusId** بصورت nullable است و شناسه وضعیت پرداخت می باشد.

در صورتی که خالی باشد تمام تراکنش ها به همراه جزئیات دریافت می شود. در غیر این صورت تراکنش ها با وضعیت انتخابی نمایش داده می شوند.
</div>