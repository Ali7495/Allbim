using DAL.Contracts.EnumIRepositories;
using DAL.Models;
using Models.Product;
using Models.QueryParams;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.PipeLine
{
    public class MarketFluctuateCover : Step<BodyProductInputViewModel, OutputViewModel>
    {

        public override Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer, BodyProductInputViewModel bodyProductInputViewModel)
        {
            return Task.Run<OutputViewModel>(() =>
            {

                ProductInsurerTermViewModel term = insurer.InsurerTerms.FirstOrDefault(i => i.Value == bodyProductInputViewModel.MarketFluctuateCoverId.ToString() && i.InsuranceTermType.Field == "MarketFluctuateCover" );

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
}
