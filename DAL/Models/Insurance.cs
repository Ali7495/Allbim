using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Insurance
    {
        public Insurance()
        {
            Discounts = new HashSet<Discount>();
            InsuranceFaqs = new HashSet<InsuranceFaq>();
            InsuranceFields = new HashSet<InsuranceField>();
            InsuranceFrontTabs = new HashSet<InsuranceFrontTab>();
            InsuranceSteps = new HashSet<InsuranceStep>();
            Insurers = new HashSet<Insurer>();
            Reminders = new HashSet<Reminder>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Slug { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsDeleted { get; set; }
        public string Summary { get; set; }

        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<InsuranceFaq> InsuranceFaqs { get; set; }
        public virtual ICollection<InsuranceField> InsuranceFields { get; set; }
        public virtual ICollection<InsuranceFrontTab> InsuranceFrontTabs { get; set; }
        public virtual ICollection<InsuranceStep> InsuranceSteps { get; set; }
        public virtual ICollection<Insurer> Insurers { get; set; }
        public virtual ICollection<Reminder> Reminders { get; set; }
    }
}
