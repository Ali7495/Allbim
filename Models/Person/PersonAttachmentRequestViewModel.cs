using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Models.Person
{
    public class PersonAttachmentRequestViewModel
    {
        public IFormFile File { get; set; }
        public int TypeId { get; set; }
    }
}