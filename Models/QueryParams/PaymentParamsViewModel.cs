using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Models.PageAble;

namespace Models.QueryParams
{
    public class PaymentParamsViewModel:PageAbleResult
    {
         public long? statusId;

    }
}