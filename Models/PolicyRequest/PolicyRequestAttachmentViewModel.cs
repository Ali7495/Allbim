using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Attachment;

namespace Models.PolicyRequest
{
    public class PolicyRequestAttachmentViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public Guid Code { get; set; }
        public Guid AttachmentCode { get; set; }

        // public AttachmentViewModel Attachment { get; set; }
        //public virtual PolicyRequestViewModel PolicyRequest { get; set; }

        //public Guid? PolicyRequestCode { get; set; }
        //public Guid? AttachmentCode { get; set; }


    } 
    public class PolicyRequestAttachmentDownloadViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public Guid Code { get; set; }
        public string AttachmentCode { get; set; }
    }

    public class PolicyRequestInputAttachmentViewModel
    {
        [JsonPropertyName("file")]
        [Required]
        public IFormFile File { get; set; }
        [BindProperty(Name = "type_id")]
        [JsonPropertyName("type_id")]
        [Required]
        public int TypeId { get; set; }
    }
}
