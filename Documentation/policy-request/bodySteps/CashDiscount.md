```C#

   public class CashDiscount : Step<BodyProductInputViewModel, OutputViewModel>
    {
        public override Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer, BodyProductInputViewModel bodyProductInputViewModel)
        {
            return Task.Run<OutputViewModel>(() =>
            {
                if (bodyProductInputViewModel.CashDiscountId != null)
                {
                    //ProductInsurerTermViewModel term = insurer.InsurerTerms.FirstOrDefault(i => i.Field == "CashDiscount" && i.Value == bodyProductInputViewModel.CashDiscountId.ToString());
                    ProductInsurerTermViewModel term = insurer.InsurerTerms.FirstOrDefault(i => i.Value == bodyProductInputViewModel.CashDiscountId.ToString() && i.InsuranceTermType.Field == "CashDiscount"  );

                    if (term != null)
                    {

                        if (term.IsCumulative)
                        {
                            if (term.PricingTypeId == 1)
                            {
                                output.Product.Price -= decimal.Parse(term.Discount, CultureInfo.InvariantCulture);
                            }
                            else if (term.PricingTypeId == 2)
                            {
                                output.Product.Price -= output.Product.Price * decimal.Parse(term.Discount, CultureInfo.InvariantCulture);
                            }
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

                }
               
                return output;
            });
        }
    }
```

<div align="right" dir="rtl">

نوع قیمت گذاری در محاسبات لحاظ شده

>*برای مشاهده پیاده سازی کلاس استپ [Pipe Line](./PipeLine.md) را مطالعه فرمایید*

</div>