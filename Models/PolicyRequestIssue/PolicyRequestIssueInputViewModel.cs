using Models.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.PolicyRequestIssue
{
    public class PolicyRequestIssueInputViewModel
    {
        [JsonPropertyName("code")]
        public Guid? Code { get; set; }

        [JsonPropertyName("email_address")]
        public string EmailAddress { get; set; }

        [JsonPropertyName("need_print")]
        public bool NeedPrint { get; set; }

        [JsonPropertyName("receiver_address")]
        public virtual AddressViewModel ReceiverAddress { get; set; }

        [JsonPropertyName("receiver_address_code")]
        public Guid? ReceiverAddressCode { get; set; }

        [JsonPropertyName("receive_date")]
        public string ReceiveDate { get; set; }

        [JsonPropertyName("issue_session_id")]
        public long? IssueSessionId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("wallet_id")]
        public byte? WalletId { get; set; }

    }
}
