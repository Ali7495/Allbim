using Models.Article;
using Models.Company;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Insurance
{
    public class InsurerViewModel
    {
        public long Id { get; set; }
        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }
        [JsonProperty("description")]
        [Required]
        public string Description { get; set; }
        [Required]
        public long InsuranceId { get; set; }
        [Required]
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        

        [JsonProperty("company_code")]
        public Guid CompanyCode { get; set; }

        [JsonProperty("article_id")]
        public long? ArticleId { get; set; }

        [JsonProperty("company")]
        public virtual CompanyViewModel Company { get; set; }
        [JsonProperty("article")]
        public virtual ArticlesViewModel Article { get; set; }
        
        // public virtual ICollection<InsurerTermViewModel> InsurerTerms { get; set; }
    }
    public class InsurerMineViewModel
    {
        public long Id { get; set; }
        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }
        [JsonProperty("description")]
        [Required]
        public string Description { get; set; }
        [Required]
        public long InsuranceId { get; set; }
        [Required]
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        

        [JsonProperty("company_code")]
        public Guid CompanyCode { get; set; }

        [JsonProperty("article_id")]
        public long? ArticleId { get; set; }

        [JsonProperty("company")]
        public virtual CompanyViewModel Company { get; set; }
        [JsonProperty("article")]
        public virtual ArticlesViewModel Article { get; set; }  
        [JsonProperty("insurance")]
        public virtual InsuranceMineViewModel Insurance { get; set; }
        
        // public virtual ICollection<InsurerTermViewModel> InsurerTerms { get; set; }
    }
}
