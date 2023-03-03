using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Models.Attachment
{
    public class AttachmentViewModel
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
    }
    public class AttachmentInputViewModel
    {
        public Guid Code { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
    }
    public class AttachmentResultViewModel
    {
        public Guid Code { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string AttachmentCode { get; set; }
        
    }
}
