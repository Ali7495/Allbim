using Models.Insurance;
using Models.Person;
using System;
using System.Collections.Generic;
using Models.Agent;
using Models.Company;
using Models.Vehicle;
using Newtonsoft.Json;

namespace Models.PolicyRequest
{
    public class PolicyRequestViewModel
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
        public long RequestPersonId { get; set; }
        public long? PolicyRequestStatusId { get; set; }

        public InsurerViewModel Insurer { get; set; }
        public PersonViewModel RequestPerson { get; set; }
        public virtual ICollection<InsuredRequestViewModel> InsuredRequests { get; set; }
        public virtual ICollection<PolicyRequestAttachmentViewModel> PolicyRequestAttachments { get; set; }
        public virtual ICollection<PolicyRequestDetailViewModel> PolicyRequestDetails { get; set; }
        public virtual ICollection<PolicyRequestFactorViewModel> PolicyRequestFactors { get; set; }
        public virtual ICollection<PolicyRequestHolderViewModel> PolicyRequestHolders { get; set; }

    }
    public class PolicyRequestMineViewModel
    {
        public Guid Code { get; set; }
        public string Title { get; set; }
        public string PolicyNumber { get; set; }
        public string Description { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long InsurerId { get; set; }
        public long RequestPersonId { get; set; }
        public long? PolicyRequestStatusId { get; set; }

        public InsurerMineViewModel Insurer { get; set; }
        public PersonViewModel RequestPerson { get; set; }
        public AgentViewModel AgentSelected { get; set; }
        // public virtual ICollection<InsuredRequestViewModel> InsuredRequests { get; set; }
        public virtual ICollection<PolicyRequestAttachmentViewModel> PolicyRequestAttachments { get; set; }
        // public virtual ICollection<PolicyRequestDetailViewModel> PolicyRequestDetails { get; set; }
        // public virtual ICollection<PolicyRequestFactorViewModel> PolicyRequestFactors { get; set; }
        // public virtual ICollection<PolicyRequestHolderViewModel> PolicyRequestHolders { get; set; }

    }
    public class MyPolicyRequestViewModel
    {
        public Guid Code { get; set; }
        public virtual InsuranceViewModel insurance { get; set; }
        public virtual CompanyDetailViewModel Company { get; set; }
        public virtual VehicleResultViewModel Vehicle { get; set; }
        public string PolicyNumber { get; set; }
        public virtual PolicyRequestStatusViewModel PolicyRequestStatus { get; set; }
        
        public string Caption { get; set; }
        


    }

    public class PolicyReqiestStatusInputViewModel
    {
        [JsonProperty("policy_request_status_id")]
        public long PolicyRequestStatusId { get; set; }
    }

    public class PolicyRequestSummaryOutputViewModel
    {
        public long Id { get; set; } 
        public Guid Code { get; set; }
        public string Title { get; set; }
        public string PolicyNumber { get; set; }
        public string Description { get; set; }
        public long InsurerId { get; set; }
        public long PolicyRequestStatusId { get; set; }
    }

    public class PolicyRequestAgetSelectGetViewModel
    {
        public long? AgentSelectedId { get; set; }
        public byte? AgentSelectionTypeId { get; set; }
        public Guid? CompanyCode { get; set; }
    }
    public class PolicyRequestAgetSelectUpdateInputViewModel
    {
        public long? AgentSelectedId { get; set; }
        public byte AgentSelectionTypeId { get; set; }
    }
    public class PolicyRequestAgetSelectUpdateOutputViewModel
    {
        public Guid Code { get; set; }
        public long RequestPersonId { get; set; }
        public string Title { get; set; }
        public long InsurerId { get; set; }
        public string PolicyNumber { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? PolicyRequestStatusId { get; set; }
        public byte AgentSelectionTypeId { get; set; }
        public long? AgentSelectedId { get; set; }
    }
}
