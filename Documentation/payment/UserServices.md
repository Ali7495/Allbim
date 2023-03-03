<div dir="rtl" align="right">
# تشریح سرویس های نقش کاربر

* آدرس  `policy-request/mine/{code}/PaymentInfo` با متد Get جهت دریافت اطلاعات درخواست بیمه و فاکتور قابل پرداخت آن سرویس زیر را در کنترلر PolicyRequest فراخوانی می کند:

`GetPolicyRequestPaymentDetailsMine(userId,code,cancellationToken)`

ورودی های مهم آن به شرح زیر است:

userId : آیدی کابر استفاده کننده از سیستم که در برنه این سرویس دریافت می شود

code: کد انحصاری درخواست بیمه مورد نظر که از url خوانده می شود



در این سرویس ابتدا جهت برخی بررسی ها متد های زیر صدا زده می شوند :

`IsUserValidCommon(userId, cancellationToken)` : جهت بررسی وجود کاربری با آیدی دریافت شده

`IsMinePolicyRequestCommon(userId, code, cancellationToken)` : بررسی می کند که درخواست بیمه برای کاربر استفاده کننده می باشد

و نهایتا سرویس `GetPaymentInfoCommon(policyRequest, cancellationToken)`  اطلاعات درخواست بیمه به همراه جزئیات آن درخواست و فاکتور و قیمت آن را بر میگرداند.



* آدرس `payment/mine`  جهت دریافت تمامی فاکتور های کاربر استفاده کننده به همراه اطلاعات درخواست بیمه و جزئیات فاکتور و تراکنش سرویس زیر را در کنترلر Payment فراخوانی میکند:

  `GetAllPaymentsMine(long userId, long? statusId, PageAbleResult pageAbleResult, CancellationToken cancellationToken)`

  این سرویس پارامتر statusId را که قابلیت null بودن دارد دریافت میکند. اگر null بود که تمام تراکنش ها و در غیر این صورت تراکنش هایی با وضعیت مورد درخواست می آورد. برای مثال اگر statusId معادل 1 باشد تمام فاکتور های در انتظار پرداخت آورده می شود.

  </div>