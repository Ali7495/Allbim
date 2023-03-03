using Models.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Attachment;

namespace Models.PolicyRequestCommet
{
    public class PolicyRequestCommentOutputViewModel
    {
        public long Id { get; set; }
        public long PolicyRequestId { get; set; }
        public string Description { get; set; }
        //public long AuthorId { get; set; }
        public PersonInfoViewModel Author { get; set; }
        public byte? AuthorTypeId { get; set; }
        public DateTime? createdDateTime { get; set; }
    }
    public class PolicyRequestCommentGetAllOutputViewModel
    {
        public long Id { get; set; }
        public Guid PolicyRequestCode { get; set; }
        public string Description { get; set; }
        public PersonInfoViewModel Author { get; set; }
        public byte? AuthorTypeId { get; set; }
        public DateTime? createdDateTime { get; set; }
        public string JalaliDate { get; set; }
        public virtual List<PolicyRequestCommentAttachmentViewModel> Attachments { get; set; }
    }
    public class PolicyRequestCommentAttachmentViewModel
    {
        public Guid Code { get; set; }
        public string AttachmentCode { get; set; }
    }
    public class PolicyRequestCommentInputViewModel
    {
        public string Description { get; set; }
        //public long Status { get; set; }
        public List<Guid> AttachmentCodes { get; set; }

    }

}
