```C#

  public class AcidicSpray : Step<BodyProductInputViewModel, OutputViewModel>
    {
        public override Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer, BodyProductInputViewModel bodyProductInputViewModel)
        {
            return Task.Run<OutputViewModel>(() =>
            {

                //ProductInsurerTermViewModel term = insurer.InsurerTerms.FirstOrDefault(i => i.Field == "AcidAndChemicalId" && i.Value == bodyProductInputViewModel.AcidAndChemicalId.ToString());
                ProductInsurerTermViewModel term = insurer.InsurerTerms.FirstOrDefault(i => i.Value == bodyProductInputViewModel.AcidAndChemicalId.ToString() && i.InsuranceTermType.Field == "AcidAndChemicalId" );

                if (term != null)
                {
                    if (term.IsCumulative)
                    {
                        if (term.PricingTypeId == 1)
                        {
                            switch (term.CalculationTypeId)
                            {
                                case 1:
                                    output.Product.Price -=  decimal.Parse(term.Discount, CultureInfo.InvariantCulture);
                                    break;
                                default:
                                    output.Product.Price +=  decimal.Parse(term.Discount, CultureInfo.InvariantCulture);
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