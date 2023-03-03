using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PolicyRequest
    {
        public PolicyRequest()
        {
            InsuredRequests = new HashSet<InsuredRequest>();
            PolicyRequestAttachments = new HashSet<PolicyRequestAttachment>();
            PolicyRequestComments = new HashSet<PolicyRequestComment>();
            PolicyRequestDetails = new HashSet<PolicyRequestDetail>();
            PolicyRequestFactors = new HashSet<PolicyRequestFactor>();
            PolicyRequestHolders = new HashSet<PolicyRequestHolder>();
            PolicyRequestInspections = new HashSet<PolicyRequestInspection>();
            PolicyRequestIssues = new HashSet<PolicyRequestIssue>();
        }

        public long Id { get; set; }
        public Guid Code { get; set; }
        public long RequestPersonId { get; set; }
        public string Title { get; set; }
        public long InsurerId { get; set; }
        public string PolicyNumber { get; set; }
        public string Description { get; set; }
        public byte IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? PolicyRequestStatusId { get; set; }
        public long? ReviewerId { get; set; }
        public byte AgentSelectionTypeId { get; set; }
        public long? AgentSelectedId { get; set; }

        public virtual CompanyAgent AgentSelected { get; set; }
        public virtual Insurer Insurer { get; set; }
        public virtual PolicyRequestStatus PolicyRequestStatus { get; set; }
        public virtual Person RequestPerson { get; set; }
        public virtual Person Reviewer { get; set; }
        public virtual ICollection<InsuredRequest> InsuredRequests { get; set; }
        public virtual ICollection<PolicyRequestAttachment> PolicyRequestAttachments { get; set; }
        public virtual ICollection<PolicyRequestComment> PolicyRequestComments { get; set; }
        public virtual ICollection<PolicyRequestDetail> PolicyRequestDetails { get; set; }
        public virtual ICollection<PolicyRequestFactor> PolicyRequestFactors { get; set; }
        public virtual ICollection<PolicyRequestHolder> PolicyRequestHolders { get; set; }
        public virtual ICollection<PolicyRequestInspection> PolicyRequestInspections { get; set; }
        public virtual ICollection<PolicyRequestIssue> PolicyRequestIssues { get; set; }
    }
}
