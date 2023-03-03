namespace Models.PolicyRequest
{
    public class PolicyRequestFactorViewModel
    {
        public long Id { get; set; }
        public long? PaymentId { get; set; }
        public long? PolicyRequestId { get; set; }
        public  PolicyRequestViewModel PolicyRequest { get; set; }

    }
}
