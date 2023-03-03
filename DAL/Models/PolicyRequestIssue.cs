using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PolicyRequestIssue
    {
        public long Id { get; set; }
        public long PolicyRequestId { get; set; }
        public string EmailAddress { get; set; }
        public bool? NeedPrint { get; set; }
        public long? ReceiverAddressId { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public long? IssueSessionId { get; set; }
        public long? WalletId { get; set; }
        public string Description { get; set; }

        public virtual IssueSession IssueSession { get; set; }
        public virtual PolicyRequest PolicyRequest { get; set; }
        public virtual Address ReceiverAddress { get; set; }
    }
}
