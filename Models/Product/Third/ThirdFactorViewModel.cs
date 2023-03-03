using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Product
{
    public class ThirdFactorViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("factor")]
        public decimal Factor { get; set; }

        [JsonProperty("calculation_type")]
        public string CalculationType { get; set; }
    }
}
