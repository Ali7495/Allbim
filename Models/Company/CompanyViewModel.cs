using Models.Article;
using Models.Insurance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Models.Articles;

namespace Models.Company
{
    public class CompanyViewModel
    {

        [JsonProperty("code")]
        public Guid Code { get; set; }

        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        // [JsonProperty("description")]
        // [Required]
        // public string Description { get; set; }
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("article_id")]
        public long? ArticleId { get; set; }
        [JsonProperty("established_year")]
        public string EstablishedYear { get; set; }

        // public virtual ArticlesViewModel Article { get; set; }
    } 
    public class CompanyDetailViewModel
    {

        [JsonProperty("code")]
        public Guid Code { get; set; }

        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        [JsonProperty("description")]
        [Required]
        public string Description { get; set; }
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("article_id")]
        public long? ArticleId { get; set; }
        [JsonProperty("established_year")]
        public string EstablishedYear { get; set; }

        public virtual ArticleDetailViewModel Article { get; set; }
    } 
    public class CompanyInputViewModel
    {
        

        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [Required]
        public string Description { get; set; }
        
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("article")]
        public ArticlesInputViewModel Article { get; set; }
        [JsonProperty("established_year")]
        public string EstablishedYear { get; set; }
    }
    
}
