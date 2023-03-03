```C#

 public class ThirdCumulativeDiscounts : Step<ThirdProductInputViewModel, OutputViewModel>
    {
        public override Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer, ThirdProductInputViewModel productRequestViewModel)
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


>*برای مشاهده پیاده سازی کلاس استپ [Pipe Line](./PipeLine.md) را مطالعه فرمایید*