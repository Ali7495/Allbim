using DAL.Models;
using Models.Product;
using Models.QueryParams;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;

namespace Services.PipeLine
{
    public class MaxFinancialCover : Step<ThirdProductInputViewModel, OutputViewModel>
    {
        public async override Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer,
            ThirdProductInputViewModel productRequestViewModel)
        {
            return await Task.Run<OutputViewModel>(() =>
            {
                ThirdFactorViewModel thirdMaxFinancialCover;

                List<ProductInsurerTermViewModel> terms = insurer.InsurerTerms.Where(i => i.InsuranceTermType.Field == "MaxFinancialCover").ToList();

                for (int i = 0; i < terms.Count; i++)
                {
                    if (terms[i].Value.HasValue() && terms[i].Discount.HasValue())
                    {
                        thirdMaxFinancialCover = new ThirdFactorViewModel()
                        {
                            Id = int.Parse(terms[i].Value.ToString()),
                            Factor = Decimal.Parse(terms[i].Discount.ToString(),CultureInfo.InvariantCulture),
                            CalculationType = terms[i].CalculationTypeId.ToString()
                        };

                        output.Product.ThirdMaxFinancialCovers.Add(thirdMaxFinancialCover);
                    }
                }

                return output;
            });
        }
    }
}