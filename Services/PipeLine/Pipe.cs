using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Models.Product;
using Models.QueryParams;

namespace Services.PipeLine
{
    public class Pipe<T, O>
    {
        public O OutPut { get; set; }

        // تمام اطلاعات را از insurer می توان گرفت مثلا company , insurance, insurerTerm
        public ProductInsuranceViewModel insurer { get; set; }

        //public List<Step> Steps { get; set; }
        public List<string> StepNames { get; set; }

        // ورودی های کاربر
        public T productRequestViewModel { get; set; }


        public async Task Run()
        {
            for (int i = 0; i < StepNames.Count; i++)
            {
                string namespaceName = "PipeLine";
                Assembly objAssembly = Assembly.Load("Services");
                string currentClass = objAssembly.GetName().Name.Replace(" ", "_") + "." + namespaceName + "." +
                                      StepNames[i];
                dynamic obj = objAssembly.CreateInstance(currentClass);

                if (obj != null)
                {
                    this.OutPut = await obj.ExecuteAsync(OutPut, insurer, productRequestViewModel);
                }

                // باید مشخص شود که کدام استپ نال است
            }

            //for (int i = 0; i < Steps.Count; i++)
            //{
            //    this.OutPut = await Steps[i].ExecuteAsync(this.OutPut);
            //}
        }
    }
}