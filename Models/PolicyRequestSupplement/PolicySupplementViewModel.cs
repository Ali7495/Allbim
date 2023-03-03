using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Models.Person;

namespace Models.PolicyRequestSupplement
{
    public class PolicySupplementViewModel
    {
        [JsonPropertyName("code")]
        public Guid? Code { get; set; }

        // [JsonPropertyName("holder")]
        // public virtual PolicyHolderSupplementViewModel PolicyHolder { get; set; }
        // [JsonPropertyName("person")]
        // public virtual InsurredRequestPersonViewModel InsurredRequestPerson { get; set; }
        [JsonPropertyName("company")]
        public virtual PolicySupplementCompanyViewModel Company { get; set; }
        [JsonPropertyName("issued_person_type")]
        public byte? IssuedPersonType { get; set; }
        [JsonPropertyName("issued_person_relation")]
        public string IssuedPersonRelation { get; set; }
        
        [JsonPropertyName("inssued_person")]
        public virtual PolicySupplementPersonViewModel IssuedPerson { get; set; }  
        [JsonPropertyName("owner_person")]
        public virtual PolicySupplementPersonViewModel OwnerPerson { get; set; }
        [JsonPropertyName("address")]
        public virtual PolicyRequestHolderPersonAddressViewModel Address { get; set; }

    }
}
