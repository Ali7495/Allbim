using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ContactUs
{
    public class ContactUsInputViewModel
    {
        public string Email { get; set; }
        public string Title { get; set; }
        public string Answer { get; set; }
        public string Description { get; set; }
    }
    public class ContactUsInputPostViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
    public class ContactUsInputPutViewModel
    {
        [Required]
        public string Answer { get; set; }
    }
    
    public class ContactUsDashboardResultViewModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Answer { get; set; }
        public long TrackingNumber { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
    public class ContactUsFrontResultViewModel
    {
        public string Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long TrackingNumber { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
