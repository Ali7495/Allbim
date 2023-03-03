```C#

 public class VehicleApplication : Step<ThirdProductInputViewModel,OutputViewModel>
    {
        public override async Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer,
            ThirdProductInputViewModel productRequestViewModel)
        {
            return await Task.Run<OutputViewModel>(() =>
            {
                ProductCentralRuleViewModel rule = insurer.InsuranceCentralRules.Where(x => x.Value == productRequestViewModel.VehicleApplicationId.ToString() && x.CentralRuleType.Field == "VehicleApplicationId").FirstOrDefault();


                if (rule != null)
                {
                    if (rule.IsCumulative)
                    {
                        output.Product.Price *= (1 + (Convert.ToInt32(rule.Discount) / 100));
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