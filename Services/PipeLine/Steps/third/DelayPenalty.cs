using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Exceptions;
using DAL.Models;
using Models.Product;
using Models.QueryParams;

namespace Services.PipeLine
{
    public class DelayPenalty : Step<ThirdProductInputViewModel, OutputViewModel>
    {
        

        public override async Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer, ThirdProductInputViewModel productRequestViewModel)
        {
            return await Task.Run<OutputViewModel>(() =>
            {


                int days = 0;

                ProductCentralRuleViewModel maxDay = insurer.InsuranceCentralRules.FirstOrDefault(x => x.CentralRuleType.Field == "MaxDelayPenaltyDay");


                if (maxDay == null)
                {
                    throw new NotFoundException("Max Delay Penalty Day Not Found");
                }


                if (maxDay.IsCumulative)
                {
                    string endDate = productRequestViewModel.OldInsurerExpireDate; // 2021-06-21


                    if (endDate == null)
                    {
                        return output;
                    }

                    //decimal maximum = Convert.ToDecimal(maxDay.Value);
                    days = (int)(DateTime.Now - DateTime.Parse(endDate)).TotalDays;
                    // چک کردن تعداد روز دیرکرد از maxDay بیشتر باشد، maxDay جایگزین شود

                    // اگر تاخیر منفی بود، یعنی تاریخ انقضای بیمه هنوز اعتبار دارد
                    if (days <= 0)
                    {
                        return output;
                    }

                    // اگر تاخیر بیشتر از ماکزیموم اعلامی بیمه مرکزی بود
                    if (days > int.Parse(maxDay.Discount))
                    {
                        days = int.Parse(maxDay.Discount);
                    }

                    //نیاز به تحلیل
                    ProductCentralRuleViewModel delayPrice = insurer.InsuranceCentralRules.FirstOrDefault(x =>  x.Value == 1.ToString() && x.CentralRuleType.Field == "DelayPenalty");

                    if (delayPrice != null)
                    {
                        output.Product.Price += days * Convert.ToDecimal(delayPrice.Discount);
                    }
                }
                else
                {
                    output.Cumulatives.Add(new CumulativeViewModel()
                    {
                        TermId = maxDay.Id,
                        Discount = maxDay.Discount
                    });
                }

                return output;
            });
        }
    }
}