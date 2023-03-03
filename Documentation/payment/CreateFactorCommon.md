<div dir="rtl" align="right">

# عملیات درج فاکتور


این عملیات در سرویس `CreatePaymentFactorCommon(code, policyCode, inputViewModel, cancellationToken)` انجام میگردد.

code : کد شرکت

policyCode : کد درخواست بیمه

inputViewModel : مدل ورودی



مدل ورودی به شرح زیر است:
</div>

```
public class CompanyPolicyFactorInputViewModel
    {
        [JsonProperty("payment")]
        public virtual CompanyPaymentInputViewModel Payment { get; set; }
        [JsonProperty("policy_request_factor_detail")]
        public virtual List<CompanyFactorDetailInputViewModel> PolicyRequestFactorDetails { get; set; }
    }
```
<div dir="rtl" align="right">

این مدل خود شامل دو مدل دیگر است:

CompanyPaymentInputViewModel : مدل ورودی تراکنش که میتواند null باشد
</div>


```public class CompanyPaymentInputViewModel
    {
        [JsonProperty("payment_gateway_id")]
        public long? PaymentGatewayId { get; set; }
        [JsonProperty("payment_code")]
        public string PaymentCode { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
```
<div dir="rtl" align="right">
PolicyRequestFactorDetails : مدل جزئیات فاکتور که در مدل ورودی بصورت لیست می باشد
</div>


```public class CompanyFactorDetailInputViewModel
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("calculation_type_id")]
        public byte? CalculationTypeId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
```


<div dir="rtl" align="right">
در این متد بررسی می شود اگر payment نال نبود درج شود ودر غیر این صورت اتفاقی نیفتد

![checkPayment](.\checkPayment.PNG)

سپس جدول فاکتور و جزئیات آن با توجه به مقادیر ورودی درج می شوند.

از آن جایی که در این سرویس چند جدول با هم درج می شوند بنابر این از transaction استفاده شده است.

بدنه این متد بصورت کامل:

![paymentInsert](.\paymentInsert.PNG)

  </div>