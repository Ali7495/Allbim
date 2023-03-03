```C#

 public class BodyBaseCost : Step<BodyProductInputViewModel, OutputViewModel>
    {
        public override Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer, BodyProductInputViewModel bodyProductInputViewModel)
        {
            return Task.Run<OutputViewModel>(() =>
            {
                

                ProductInsurerTermViewModel term = insurer.InsurerTerms.FirstOrDefault(i => i.InsuranceTermType.Field == "BodyBaseCost");

                if (term != null)
                {
                    if (term.IsCumulative)
                    {
                        output.Product.Price = decimal.Parse(bodyProductInputViewModel.CarValue, CultureInfo.InvariantCulture) * decimal.Parse(term.Discount, CultureInfo.InvariantCulture);
                        output.Product.BacePrice = output.Product.Price;
                    }
                    else
                    {
                        output.Cumulatives.Add(new CumulativeViewModel()
                        {
                            TermId = term.Id,
                            Discount = term.Discount
                        });
                    } 
                }

                return output;
            });
        }
    }

```

<div align="right" dir="rtl">

نرخ پایه را در ارزش خودرو ضرب میکند

>*برای مشاهده پیاده سازی کلاس استپ [Pipe Line](./PipeLine.md) را مطالعه فرمایید*

</div>