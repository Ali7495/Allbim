```C#

 public class BaseCost : Step<ThirdProductInputViewModel, OutputViewModel>
    {
        public  override async Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer,
            ThirdProductInputViewModel productRequestViewModel)
        {
            return await Task.Run<OutputViewModel>(() =>
            {
                ProductCentralRuleViewModel rule = insurer.InsuranceCentralRules.FirstOrDefault(x => x.Value == productRequestViewModel.VehicleRuleCategoryId.ToString() && x.CentralRuleType.Field == "VehicleRuleCategoryId");


                if (rule != null)
                {
                    if (rule.IsCumulative)
                    {
                        output.Product.Price = Convert.ToDecimal(rule.Discount);
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


```


>*برای مشاهده پیاده سازی کلاس استپ [Pipe Line](./PipeLine.md) را مطالعه فرمایید*