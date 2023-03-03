using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyComission
{
    public class CompanyComissionInputViewModel
    {
        [JsonProperty("agent_selected_id")]
        public long AgentSelectedId { get; set; }
    }
}
