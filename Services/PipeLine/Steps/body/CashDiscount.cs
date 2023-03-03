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
}
