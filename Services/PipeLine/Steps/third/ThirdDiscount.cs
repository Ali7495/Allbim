using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Models.Enums;
using Models.Product;
using Models.QueryParams;
using Common.Extensions;
namespace Services.PipeLine
{
    public class ThirdDiscount : Step<ThirdProductInputViewModel,OutputViewModel>
    {
        public override async Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer,ThirdProductInputViewModel productRequestViewModel)
        {
            return await Task.Run<OutputViewModel>(() =>
            {
                if (!productRequestViewModel.IsChangedOwner  && productRequestViewModel.ThirdDiscountId != null)
                {
                    int discount = productRequestViewModel.ThirdDiscountId.Value;

                    output.Product.InsuranceDiscount += discount;                    
                }
                return output ;
            });
        }
    }
}
