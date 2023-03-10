```C#

  public class FranchiseRemoval : Step<BodyProductInputViewModel, OutputViewModel>
    {
        public override Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer, BodyProductInputViewModel bodyProductInputViewModel)
        {
            return Task.Run<OutputViewModel>(() =>
            {
                //ProductInsurerTermViewModel term = insurer.InsurerTerms.FirstOrDefault(i => i.Field == "FranchiseRemoval" && i.Value == bodyProductInputViewModel.FranchiseRemovalId.ToString());
                ProductInsurerTermViewModel term = insurer.InsurerTerms.FirstOrDefault(i => i.Value == bodyProductInputViewModel.FranchiseRemovalId.ToString() && i.InsuranceTermType.Field == "FranchiseRemoval"  );

                if (term != null)
                {
                    if (term.IsCumulative)
                    {
                        if (term.PricingTypeId == 1)
                        {
                            switch (term.CalculationTypeId)
                            {
                                case 1:
                                    output.Product.Price -= decimal.Parse(term.Discount, CultureInfo.InvariantCulture);
                                    break;
                                default:
                                    output.Product.Price += decimal.Parse(term.Discount, CultureInfo.InvariantCulture);
                                    break;
                            }
                        }
                        else if (term.PricingTypeId == 2)
                        {
                            switch (term.CalculationTypeId)
                            {
                                case 1:
                                    output.Product.Price -= output.Product.BacePrice * decimal.Parse(term.Discount, CultureInfo.InvariantCulture);
                                    break;
                                default:
                                    output.Product.Price += output.Product.BacePrice * decimal.Parse(term.Discount, CultureInfo.InvariantCulture);
                                    break;
                            }
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

                return output;
            });
        }
    }
```

<div align="right" dir="rtl">

?????? ???????? ?????????? ?? ???????????? ???? ?????????????? ???????? ??????

>*???????? ???????????? ?????????? ???????? ???????? ???????? [Pipe Line](./PipeLine.md) ???? ???????????? ??????????????*

</div>