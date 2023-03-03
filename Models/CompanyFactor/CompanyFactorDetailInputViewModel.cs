using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyFactor
{
    public class CompanyFactorDetailInputViewModel
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("calculation_type_id")]
        public byte? CalculationTypeId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
