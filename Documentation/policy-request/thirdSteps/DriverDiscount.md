```C#

  public class DriverDiscount : Step<ThirdProductInputViewModel, OutputViewModel>
    {
        public override async Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer,
            ThirdProductInputViewModel productRequestViewModel)
        {
            return await Task.Run<OutputViewModel>(() =>
            {
                if (!productRequestViewModel.IsChangedOwner && productRequestViewModel.DriverDiscountId != null)
                {
                    int discount = productRequestViewModel.DriverDiscountId.Value;

                    output.Product.DriverDiscount += discount;
                }

                return output;
            });
        }
    }

```


>*برای مشاهده پیاده سازی کلاس استپ [Pipe Line](./PipeLine.md) را مطالعه فرمایید*