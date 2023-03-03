using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Models.Product;
using Models.QueryParams;

namespace Services.PipeLine
{
    public class CalculateDiscount : Step<ThirdProductInputViewModel, OutputViewModel>
    {
        public override async Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer,
            ThirdProductInputViewModel productRequestViewModel)
        {
            return await Task.Run<OutputViewModel>(() =>
            {
                double discount = (double) output.Product.InsuranceDiscount / 100;

                if (discount > 0)
                {
                    output.Product.Price = Convert.ToDecimal(Convert.ToDouble(output.Product.Price) * (1 - discount));
                }
                else if (discount < 0)
                {
                    output.Product.Price = Convert.ToDecimal(Convert.ToDouble(output.Product.Price) * (1 + Math.Abs(discount)));
                }

                return output;
            });
        }
    }
}