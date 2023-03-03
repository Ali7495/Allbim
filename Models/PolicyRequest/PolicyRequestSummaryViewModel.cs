using Models.Insurance;
using Models.Person;
using System;
using System.Collections.Generic;

namespace Models.PolicyRequest
{
    public class PolicyRequestSummaryViewModel
    {
        public long Id { get; set; }
        public Guid Code { get; set; }
        public string Title { get; set; }
        public string PolicyNumber { get; set; }
        public string Description { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long InsurerId { get; set; }
        // public long RequestPersonId { get; set; }
        
    }
}
