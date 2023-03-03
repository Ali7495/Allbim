using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Schema
{
    public class ShemaVersionViewModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("script_name")]
        public string ScriptName { get; set; }

        [JsonProperty("applied")]
        public string Applied { get; set; }
    }
}
