using Models.Person;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Customer
{
    public class CustomerViewModel
    {
        [JsonProperty("request_person")]
        public virtual CustomerPersonViewModel RequestPerson { get; set; }
    }
}
