```C#

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
    
```

<div align="right" dir="rtl">

ابتدا بررسی می شود از فیلد های ورودی ویومدل به عنوان Detail استفاده شده یا خیر. در صورتی که استفاده شده باشد لیستی از آن ها تشکیل می شود.

سپس لیستی از Parent های جزئیات بدست می آوریم.

سپس به ازای هر پرنت فرزندان را در دو لیست مقایسه می کنیم هم از نظر مقدار هم از نظر تعداد تا به لیست دقیق جزئیات آن قانون برسیم و تخفیف آن را اعمال کنیم.

>*برای مشاهده پیاده سازی کلاس استپ [Pipe Line](./PipeLine.md) را مطالعه فرمایید*

</div>