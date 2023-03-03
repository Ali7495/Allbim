```C#

 public class MaxFinancialCover : Step<ThirdProductInputViewModel, OutputViewModel>
    {
        public async override Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer,
            ThirdProductInputViewModel productRequestViewModel)
        {
            return await Task.Run<OutputViewModel>(() =>
            {
                ThirdFactorViewModel thirdMaxFinancialCover;

                List<ProductInsurerTermViewModel> terms = insurer.InsurerTerms.Where(i => i.InsuranceTermType.Field == "MaxFinancialCover").ToList();

                for (int i = 0; i < terms.Count; i++)
                {
                    if (terms[i].Value.HasValue() && terms[i].Discount.HasValue())
                    {
                        thirdMaxFinancialCover = new ThirdFactorViewModel()
                        {
                            Id = int.Parse(terms[i].Value.ToString()),
                            Factor = Decimal.Parse(terms[i].Discount.ToString(),CultureInfo.InvariantCulture),
                            CalculationType = terms[i].CalculationTypeId.ToString()
                        };

                        output.Product.ThirdMaxFinancialCovers.Add(thirdMaxFinancialCover);
                    }
                }

                return output;
            });
        }
    }

```


>*برای مشاهده پیاده سازی کلاس استپ [Pipe Line](./PipeLine.md) را مطالعه فرمایید*