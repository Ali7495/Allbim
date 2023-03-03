```C#

 public class CreditDuration : Step<ThirdProductInputViewModel, OutputViewModel>
    {

        public override Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer, ThirdProductInputViewModel productRequestViewModel)
        {
            return Task.Run<OutputViewModel>(() =>
            {
                ThirdFactorViewModel thirdInsuranceCreditDuration;

                List<ProductInsurerTermViewModel> terms = insurer.InsurerTerms.Where(i => i.InsuranceTermType.Field == "CreditDuration").ToList();

                if (terms != null)
                {
                    for (int i = 0; i < terms.Count(); i++)
                    {
                        thirdInsuranceCreditDuration = new ThirdFactorViewModel()
                        {
                            Id = int.Parse(terms[i].Value),
                            Factor = decimal.Parse(terms[i].Discount, CultureInfo.InvariantCulture),
                            CalculationType = terms[i].CalculationTypeId.ToString()
                        };

                        output.Product.ThirdInsuranceCreditDurations.Add(thirdInsuranceCreditDuration);

                    }
                }

                return output;
            });
        }
    }

```


>*برای مشاهده پیاده سازی کلاس استپ [Pipe Line](./PipeLine.md) را مطالعه فرمایید*