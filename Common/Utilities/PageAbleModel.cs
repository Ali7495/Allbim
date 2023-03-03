using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Models.PageAble
{
    public class PageAbleModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string SortField { get; set; }
        public string SortOrder { get; set; } = "desc";
        public bool AllowPaginate { get; set; } = true;
        
        
        
    }
}
