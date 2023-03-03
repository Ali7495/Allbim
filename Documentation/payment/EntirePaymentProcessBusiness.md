<div dir="rtl" align="right">

# پیاده سازی فرایند پرداخت

فرایند پرداخت براساس 3 نوع دسترسی کاربر عادی، شرکت و مدیر سیستم پیاده سازی شده. هر یک از این نقش ها با آدرس خاصی از سمت کلاینت می توان متد های بک آن ها را اجرا نمود که شرح آن ها بصورت زیر است:

#### آدرس های مرتبط با کاربر

**Get** `policy-request/mine/{code}/PaymentInfo`

**Get** `payment/mine`

> *برای مطالعه روند اجرای سرویس ها [سرویس های کاربری](./UserServices.md) را مشاهده فرمایید*



#### آدرس های مرتبط با شرکت

**Get** `company/mine/policy-request/{policyCode}/PaymentInfo`

**Post**`company/mine/policy-request/{policyCode}/factor`

**Get**`company/mine/factor`

**Get** `company/mine/policy-request/{policyCode}/factor`

**Get** `company/mine/policy-request/{policyCode}/factor/{factorId}`

**Delete** `company/mine/policy-request/{policyCode}/factor/{factorId}`

**Post** `company/mine/policy-request/{policyCode}/factor/{factorId}/detail`

**Put** `company/mine/policy-request/{policyCode}/factor/{factorId}/detail/{detailId}`

**Get** `company/mine/policy-request/{policyCode}/factor/{factorId}/detail`

**Get** `company/mine/policy-request/{policyCode}/factor/{factorId}/detail/{detailId}`

**Delete** `company/mine/policy-request/{policyCode}/factor/{factorId}/detail`

**Get** `company/mine/payment`

> *برای مطالعه روند اجرای سرویس ها [سرویس های شرکت](./companyServices.md) را مشاهده فرمایید*



#### آدرس های مرتبط با مدیر سیستم

**Get** `company/{code}/policy-request/{policyCode}/PaymentInfo`

**Get** `company/{code}/policy-request/{policyCode}/PaymentInfo`

**Post **`company/{code}/policy-request/{policyCode}/factor`

**Get**`company/{code}/factor`

**Get** `company/{code}/policy-request/{policyCode}/factor`

**Get** `company/{code}/policy-request/{policyCode}/factor/{factorId}`

**Delete** `company/{code}/policy-request/{policyCode}/factor/{factorId}`

**Post** `company/{code}/policy-request/{policyCode}/factor/{factorId}/detail`

**Put** `company/{code}/policy-request/{policyCode}/factor/{factorId}/detail/{detailId}`

**Get** `company/{code}/policy-request/{policyCode}/factor/{factorId}/detail`

**Get** `company/{code}/policy-request/{policyCode}/factor/{factorId}/detail/{detailId}`

**Delete** `company/{code}/policy-request/{policyCode}/factor/{factorId}/detail`

**Get** `company/{code}/payment`



**Get** `payment`

**Post** `payment/paymentGateway`

**Get** `payment/paymentGateway`

**Get** `payment/paymentGateway/list`

**Get** `payment/paymentGateway/{gatewayId}`

**Put** `payment/paymentGateway/{gatewayId}`

**Delete** `payment/paymentGateway/{gatewayId}`



**Get** `payment`

**Post** `payment/paymentStatus`

**Get** `payment/paymentStatus/list`

**Get** `payment/paymentStatus/{statusId}`

**Put** `payment/paymentStatus/{statusId}`

**Delete** `payment/paymentStatus/{statusId}`



> *برای مطالعه روند اجرای سرویس ها [سرویس های مدیر سیستم](./AdminServices.md) را مشاهده فرمایید*
</div>