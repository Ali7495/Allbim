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
    public abstract class Step<T,O>
    {
        public abstract Task<O> ExecuteAsync(O output, ProductInsuranceViewModel insurer,T productRequestViewModel);
    }
}
