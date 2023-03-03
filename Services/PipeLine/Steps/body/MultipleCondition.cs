using DAL.Models;
using Models.InsurerTernDetail;
using Models.MultipleDiscount;
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
    public class MultipleCondition : Step<BodyProductInputViewModel, OutputViewModel>
    {
        public override Task<OutputViewModel> ExecuteAsync(OutputViewModel output, ProductInsuranceViewModel insurer, BodyProductInputViewModel bodyProductInputViewModel)
        {
            return Task.Run<OutputViewModel>(() =>
            {

                //این هارد کد فقط برای رفع خطای سینتکس
                // قرار شد برای این استپ تحلیل کنیم
                ProductInsurerTermViewModel term = insurer.InsurerTerms.FirstOrDefault(i=> i.InsuranceTermType.Field == "MultipleCondition");

                if (term != null)
                {
                    if (term.IsCumulative)
                    {
                        List<MultipleDiscountViewModel> multipleDiscounts = new List<MultipleDiscountViewModel>();

                        List<string> fields = term.InsurerTermDetails.Distinct().Select(d => d.Field).ToList();

                        for (int i = 0; i < bodyProductInputViewModel.GetType().GetProperties().Length; i++)
                        {
                            if (fields.Contains(bodyProductInputViewModel.GetType().GetProperties()[i].Name))
                            {
                                multipleDiscounts.Add(new MultipleDiscountViewModel() { Field = bodyProductInputViewModel.GetType().GetProperties()[i].Name, Value = bodyProductInputViewModel.GetType().GetProperties()[i].GetValue(bodyProductInputViewModel).ToString() });
                            }
                        }

                        List<ProductInsurerDetailViewModel> parentDetails = term.InsurerTermDetails.Where(d => multipleDiscounts.Select(m => m.Field).ToList().Contains(d.Field) && d.ParentId == null).ToList();

                        //for (int i = 0; i < parentDetails.Count; i++)
                        //{
                        //    if (parentDetails[i].Value == multipleDiscounts.FirstOrDefault(m => m.Field == parentDetails[i].Field).Value)
                        //    {
                        //        var childeren = term.InsurerTermDetails.Where(d => d.ParentId == parentDetails[i].Id).ToList();

                        //        for (int j = 0; j < childeren.Count; j++)
                        //        {
                        //            if (multipleDiscounts.Exists(m => m.Field == childeren[j].Field) && childeren[j].Value == multipleDiscounts.FirstOrDefault(m => m.Field == childeren[j].Field).Value)
                        //            {
                        //                output.Price -= output.Price * decimal.Parse(childeren[j].Discount, CultureInfo.InvariantCulture);
                        //            }
                        //        }
                        //    }
                        //}




                        // ----------------------- راه حل دوم -----------------------

                        int correctsCount = 1;

                        for (int i = 0; i < parentDetails.Count; i++)
                        {
                            if (parentDetails[i].Value == multipleDiscounts.FirstOrDefault(m => m.Field == parentDetails[i].Field).Value)
                            {
                                var childeren = term.InsurerTermDetails.Where(d => d.ParentId == parentDetails[i].Id).ToList();

                                if ((childeren.Count + 1) == multipleDiscounts.Count)
                                {
                                    for (int j = 0; j < childeren.Count; j++)
                                    {
                                        if (multipleDiscounts.Exists(m => m.Field == childeren[j].Field) && childeren[j].Value == multipleDiscounts.FirstOrDefault(m => m.Field == childeren[j].Field).Value)
                                        {
                                            correctsCount++;
                                            //output.Price -= output.Price * decimal.Parse(childeren[j].Discount, CultureInfo.InvariantCulture);
                                        }
                                    }

                                    if (correctsCount == multipleDiscounts.Count)
                                    {
                                        output.Product.Price -= output.Product.Price * decimal.Parse(parentDetails[i].Discount, CultureInfo.InvariantCulture);
                                        return output;
                                    }
                                }
                            }
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

                return output;
            });
        }
    }
}
