using Models.InsuranceStep;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models.Insurance
{
    public class InsuranceViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        // [JsonProperty("description")]
        // [Required]
        // public string Description { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("insurers")]
        public virtual List<InsurerViewModel> Insurers { get; set; }
        [JsonProperty("insurance_steps")]
        public virtual List<InsuranceStepViewModel> InsuranceSteps { get; set; }
        [JsonProperty("insurance_central_rules")]
        public virtual List<InsuranceCenteralRuleViewModel> InsuranceCenteralRules { get; set; }
    }
    public class InsuranceMineViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        // [JsonProperty("description")]
        // [Required]
        // public string Description { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("insurers")]
        public virtual List<InsurerViewModel> Insurers { get; set; }
        [JsonProperty("insurance_steps")]
        public virtual List<InsuranceStepViewModel> InsuranceSteps { get; set; }
        [JsonProperty("insurance_central_rules")]
        public virtual List<InsuranceCenteralRuleViewModel> InsuranceCenteralRules { get; set; }
        [JsonProperty("insurance_front_tabs")]
        public virtual List<InsuranceFrontTabViewModel> InsuranceFrontTabs { get; set; }
    }
    public class InsuranceInputViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        [JsonProperty("description")]
        [Required]
        public string Description { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }


    }
}
