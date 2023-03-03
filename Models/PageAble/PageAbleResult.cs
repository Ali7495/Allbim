using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Models.PageAble
{
    public class PageAbleResult
    {
        [FromQuery(Name = "pageNumber")]
        public int PageNumber { get; set; } = 1;
        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = 20;
        [FromQuery(Name = "sortField")]

        public string SortField { get; set; }
        [FromQuery(Name = "sortOrder")]

        public string SortOrder { get; set; }

        
        
        
        [FromQuery(Name = "page")]
        public int Page { get; set; }
        [FromQuery]

        public string OrderBy { get; set; }

        public InsurerQ Insurer { get; set; }
    }

    public class InsurerQ
    {
    
        public CompanyQ Company { get; set; }
    }
    public class CompanyQ
    {
      
        public string name { get; set; }
    }
}
