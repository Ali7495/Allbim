```C#

 public class FinancialDamage : Step<ThirdProductInputViewModel, OutputViewModel>
    {
        public override async Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer,ThirdProductInputViewModel productRequestViewModel)
        {
            return await Task.Run<OutputViewModel>(() => 
            {
                ProductCentralRuleViewModel rule = insurer.InsuranceCentralRules.FirstOrDefault(x => x.Value == productRequestViewModel.ThirdFinancialDamageId.ToString() && x.CentralRuleType.Field == "ThirdFinancialDamageId");


                if (rule != null)
                {
                    if (rule.IsCumulative)
                    {
                        int discount = Convert.ToInt32(rule.Discount);
                        output.Product.InsuranceDiscount -= discount;
                    }
                    else
                    {
                        output.Cumulatives.Add(new CumulativeViewModel()
                        {
                            TermId = rule.Id,
                            Discount = rule.Discount
                        });
                    }
                }
                return output; 
            });
        } 
    }

```


>*برای مشاهده پیاده سازی کلاس استپ [Pipe Line](./PipeLine.md) را مطالعه فرمایید*