using System;

namespace Models.Policy
{
    public class PolicyViewModel
    {
        public long Id { get; set; }
        public Guid Code { get; set; }
        public string Title { get; set; }
        public string PolicyNumber { get; set; }
        public string Description { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public long InsurerId { get; set; }
    }
}
