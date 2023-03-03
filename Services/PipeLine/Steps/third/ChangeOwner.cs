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
    public class ChangeOwner : Step<ThirdProductInputViewModel, OutputViewModel>
    {
        public override async Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer,ThirdProductInputViewModel productRequestViewModel)
        {
            return await Task.Run<OutputViewModel>(() => 
            { 
                return output ; 
            });
        } 
    }
}
