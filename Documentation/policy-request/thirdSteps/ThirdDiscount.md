```C#

 public class ThirdDiscount : Step<ThirdProductInputViewModel,OutputViewModel>
    {
        public override async Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer,ThirdProductInputViewModel productRequestViewModel)
        {
            return await Task.Run<OutputViewModel>(() =>
            {
                if (!productRequestViewModel.IsChangedOwner  && productRequestViewModel.ThirdDiscountId != null)
                {
                    int discount = productRequestViewModel.ThirdDiscountId.Value;

                    output.Product.InsuranceDiscount += discount;                    
                }
                return output ;
            });
        }
    }

```


>*برای مشاهده پیاده سازی کلاس استپ [Pipe Line](./PipeLine.md) را مطالعه فرمایید*