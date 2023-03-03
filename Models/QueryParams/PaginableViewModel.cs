using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Models.QueryParams
{
    public class PaginableViewModel
    {
        [FromQuery] public int Page { get; set; } = 1;
        [FromQuery] public int PageSize { get; set; } = 20;
        [FromQuery] public string OrderBy { get; set; }
    }
}