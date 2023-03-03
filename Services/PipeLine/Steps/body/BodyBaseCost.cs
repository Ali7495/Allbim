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
    public class BodyBaseCost : Step<BodyProductInputViewModel, OutputViewModel>
    {
        public override Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer, BodyProductInputViewModel bodyProductInputViewModel)
        {
            return Task.Run<OutputViewModel>(() =>
            {
                

                ProductInsurerTermViewModel term = insurer.InsurerTerms.FirstOrDefault(i => i.InsuranceTermType.Field == "BodyBaseCost");

                if (term != null)
                {
                    if (term.IsCumulative)
                    {
                        output.Product.Price = decimal.Parse(bodyProductInputViewModel.CarValue, CultureInfo.InvariantCulture) * decimal.Parse(term.Discount, CultureInfo.InvariantCulture);
                        output.Product.BacePrice = output.Product.Price;
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
