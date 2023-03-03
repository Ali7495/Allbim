using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Person
{
    public class PersonAddressViewModel
    {

 
        [JsonProperty("address")]
        public virtual AddressViewModel Address { get; set; }
        [JsonProperty("address_type_id")]
        public long AddressTypeId { get; set; }
       


    }
}
