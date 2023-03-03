using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Models.QueryParams
{
    public class DateableViewModel
    {
        [FromQuery] public string Date { get; set; }
    }
}