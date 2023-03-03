using Models.BodySupplementInfo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.CompanyPolicySuplement
{
    public class CompanyBodySupplementInfoViewModel
    {
        [JsonProperty("issued_person_type")]
        public byte? IssuedPersonType { get; set; }

        [JsonProperty("owner_person")]
        public virtual CompanyBodyOwnerSupplementViewModel OwnerPerson { get; set; }

        [JsonProperty("issued_person")]
        public virtual BodyIssueSupplementInfoViewModel IssuedPerson { get; set; }
    }
}
