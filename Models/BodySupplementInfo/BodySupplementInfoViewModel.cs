using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.BodySupplementInfo
{
    public class BodySupplementInfoViewModel
    {

        [JsonProperty("issued_person_type")]
        public byte? IssuedPersonType { get; set; }

        [JsonProperty("owner_person")]
        public virtual BodyOwnerSupplementViewModel OwnerPerson { get; set; }

        [JsonProperty("issued_person")]
        public virtual BodyIssueSupplementInfoViewModel IssuedPerson { get; set; }

        // [JsonProperty("address")]
        // public virtual BodySupplementAddressViewModel Address { get; set; }
    }
}
