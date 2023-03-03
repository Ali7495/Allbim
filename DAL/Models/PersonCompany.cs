using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PersonCompany
    {
        public PersonCompany()
        {
            InverseParent = new HashSet<PersonCompany>();
        }

        public long Id { get; set; }
        public long? PersonId { get; set; }
        public long? CompanyId { get; set; }
        public string Position { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public long? ParentId { get; set; }

        public virtual Company Company { get; set; }
        public virtual PersonCompany Parent { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<PersonCompany> InverseParent { get; set; }
    }
}
