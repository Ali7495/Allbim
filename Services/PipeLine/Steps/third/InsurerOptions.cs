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
    public class InsurerOptions : Step<ThirdProductInputViewModel, OutputViewModel>
    {

        public async override Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer, ThirdProductInputViewModel productRequestViewModel)
        {
            return await Task.Run<OutputViewModel>(() =>
            {
                // نیاز به تحلیل
                return output;
            });
        }
    }
}
