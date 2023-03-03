using Models.Article;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Insurer
{
    public class InsurerResultViewModel
    {
        [JsonProperty("insurance_id")]
        public long InsuranceId { get; set; }
        [JsonProperty("code")]
        public Guid CompanyCode { get; set; }
        [JsonProperty("article_id")]
        public long? ArticleId { get; set; }

        [JsonProperty("company")]
        public virtual InsurerCompanyResultViewModel Company { get; set; }
        [JsonProperty("insurance")]
        public virtual InsurerInsuranceResultViewModel Insurance { get; set; }
        [JsonProperty("article")]
        public virtual ArticlesViewModel Article { get; set; }
    }
}
