using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyCenter
{
    public class CenterScheduleInputViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("descriptioin")]
        public string Description { get; set; }
    }
}
