```C#

  public class CumulativeDiscounts : Step<BodyProductInputViewModel, OutputViewModel>
    {
        public override Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer, BodyProductInputViewModel productRequestViewModel)
        {
            return Task.Run<OutputViewModel>(() =>
            {
                if (output.Cumulatives.Count != 0)
                {
                    decimal maxDiscount = decimal.Parse(output.Cumulatives.Max(m => m.Discount), CultureInfo.InvariantCulture);

                    output.Product.Price -= output.Product.Price * maxDiscount;
                }

                return output;
            });
        }
    }
```

<div align="right" dir="rtl">

لیست تمام پوشش های غیر قابل تجمیع در اینجا بررسی می شود، هر عضوی که بیشترین مقدار را داشت اعمال می شود.

>*برای مشاهده پیاده سازی کلاس استپ [Pipe Line](./PipeLine.md) را مطالعه فرمایید*

</div>