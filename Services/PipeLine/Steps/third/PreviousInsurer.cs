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
    public class PreviousInsurer : Step<ThirdProductInputViewModel,OutputViewModel>
    {
        public override async Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer,ThirdProductInputViewModel thirdProductInputViewModel)
        {
            return await Task.Run<OutputViewModel>(() =>
            {
                if (insurer.Insurer.Id == thirdProductInputViewModel.OldInsurerId)
                {
                    // اقدامات لازم
                }
                return output ;
            });
        }
    }
}
