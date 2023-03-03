namespace Models.Policy
{
    public class PolicyHolderViewModel
    {
        public long Id { get; set; }
        public byte Type { get; set; }
        public string Field { get; set; }
        public string Criteria { get; set; }
        public string Value { get; set; }
        public string Discount { get; set; }
        public string CalculationType { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public long PolicyId { get; set; }
    }
}
