```C#

  public class Tax : Step<BodyProductInputViewModel, OutputViewModel>
    {
        public override Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer,
            BodyProductInputViewModel bodyProductInputViewModel)
        {
            return Task.Run<OutputViewModel>(() =>
            {
                ProductInsurerTermViewModel term = insurer.InsurerTerms.FirstOrDefault(i => i.Value == bodyProductInputViewModel.TaxId.ToString() && i.InsuranceTermType.Field == "TaxId" );

                if (term != null)
                {
                    if (term.IsCumulative)
                    {
                        output.Product.Price += output.Product.BacePrice * decimal.Parse(term.Discount, CultureInfo.InvariantCulture);
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
                


                //if (term != null)
                //{
                //    output.Price += output.BacePrice * decimal.Parse(term.Value,CultureInfo.InvariantCulture);
                //}

                return output;
            });
        }
    }
```

<div align="right" dir="rtl">


>*برای مشاهده پیاده سازی کلاس استپ [Pipe Line](./PipeLine.md) را مطالعه فرمایید*

</div>