using System;

namespace Models.PolicyRequest
{
    public class PolicyRequestHolderViewModel
    {
        public long Id { get; set; }
        public long PolicyRequestId { get; set; }
        public long? PersonId { get; set; }
        public long? CompanyId { get; set; }
        public Guid Code { get; set; }
    }
}
