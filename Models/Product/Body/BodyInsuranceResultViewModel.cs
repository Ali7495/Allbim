using Models.Insurance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Product
{
    public class BodyInsuranceResultViewModel
    {
        public long Id { get; set; }
        public string title { get; set; }
        public string Price { get; set; }
        public string number { get; set; }
        public string level { get; set; }
        public string num { get; set; }
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        public List<InsurerDetailTestViewModel> products { get; set; }

        //public List<ThirdFactorViewModel> ThirdMaxFinancialCovers { get; set; }
        //public List<ThirdFactorViewModel> ThirdInsuranceCreditDurations { get; set; }
    }
}
