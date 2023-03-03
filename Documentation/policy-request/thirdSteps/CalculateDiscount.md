```C#

  public class CalculateDiscount : Step<ThirdProductInputViewModel, OutputViewModel>
    {
        public override async Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer,
            ThirdProductInputViewModel productRequestViewModel)
        {
            return await Task.Run<OutputViewModel>(() =>
            {
                double discount = (double) output.Product.InsuranceDiscount / 100;

                if (discount > 0)
                {
                    output.Product.Price = Convert.ToDecimal(Convert.ToDouble(output.Product.Price) * (1 - discount));
                }
                else if (discount < 0)
                {
                    output.Product.Price = Convert.ToDecimal(Convert.ToDouble(output.Product.Price) * (1 + Math.Abs(discount)));
                }

                return output;
            });
        }
    }

```


>*برای مشاهده پیاده سازی کلاس استپ [Pipe Line](./PipeLine.md) را مطالعه فرمایید*