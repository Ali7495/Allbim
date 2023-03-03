<div align="right" dir="rtl">

ابتدا کلاس Step از نوع abstract و بصورت جنریک تعریف شده است. تا کلاس های دیگر بتوانند از آن ارث بری کنند.

متد `Task<O> ExecuteAsync(O output, ProductInsuranceViewModel insurer,T productRequestViewModel)` هم که شامل 3 آرگومان می باشد بصورت abstract تعریف می شود تا کلاس هایی که از این کلاس ارث میبرند هر کدام متناسب هدفشان این متد را پیاده سازی کنند.

کلاس های استپ از این کلاس ارث بری می کنند.

**ProductInsuranceViewModel** : ویومدلی از Insurer به عنوان ورودی تابع

**productRequestViewModel** : ویومدلی از ورودی های کاربر به عنوان ورودی

**O** : هر نوع کلاسی که قرار است به عنوان خروجی استفاده شود


</div>

```C#

public abstract class Step<T,O>
    {
        public abstract Task<O> ExecuteAsync(O output, ProductInsuranceViewModel insurer,T productRequestViewModel);
    }


```


<div align="right" dir="rtl">

کلاس Pipe هم بصورت زیر می باشد :

</div>

```C#

 public class Pipe<T, O>
    {
        public O OutPut { get; set; }

        // تمام اطلاعات را از insurer می توان گرفت مثلا company , insurance, insurerTerm
        public ProductInsuranceViewModel insurer { get; set; }

        //public List<Step> Steps { get; set; }
        public List<string> StepNames { get; set; }

        // ورودی های کاربر
        public T productRequestViewModel { get; set; }


        public async Task Run()
        {
            for (int i = 0; i < StepNames.Count; i++)
            {
                string namespaceName = "PipeLine";
                Assembly objAssembly = Assembly.Load("Services");
                string currentClass = objAssembly.GetName().Name.Replace(" ", "_") + "." + namespaceName + "." +
                                      StepNames[i];
                dynamic obj = objAssembly.CreateInstance(currentClass);

                if (obj != null)
                {
                    this.OutPut = await obj.ExecuteAsync(OutPut, insurer, productRequestViewModel);
                }

                // باید مشخص شود که کدام استپ نال است
            }

            //for (int i = 0; i < Steps.Count; i++)
            //{
            //    this.OutPut = await Steps[i].ExecuteAsync(this.OutPut);
            //}
        }
    }


```

<div align="right" dir="rtl">

**StepNames** : لیست نام استپ ها

در تابع `Task Run()` به ازای هر نام استپ توسط مفاهیم reflection کلاس آن را که قبلا ساخته ایم بصورت dynamic دریافت کرده و متد `ExecuteAsync(OutPut, insurer, productRequestViewModel)` را در آن فراخوانی می کنیم تا هر استپ محاسبات خود را انجام دهد.

نتیجه محاسبات هر استپ درون OutPut ریخته می شود تا در استپ بعدی از آن استفاده شود.

</div>