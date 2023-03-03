using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;

namespace albim.Builder
{
    public class DynamicModelBinder:IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var result = new Dictionary<string, dynamic> { };
            var query = bindingContext.HttpContext.Request.Query;
            if (query == null)
            {
                bindingContext.ModelState.AddModelError("QueryString", "The data is null");
                return Task.CompletedTask;
            }

            foreach (var k in query.Keys)
            {
                StringValues v = string.Empty;
                var flag = query.TryGetValue(k, out v);
                var key = k.ToPascalCase();
                if (flag)
                {
                    if (v.Count > 1)
                    {
                        result.Add(key, v);
                    }
                    else { 
                        result.Add(key, v[0]);

                    }
                }
            }

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}