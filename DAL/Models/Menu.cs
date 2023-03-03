using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Menu
    {
        public Menu()
        {
            InverseParent = new HashSet<Menu>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public long? Order { get; set; }
        public long? ParentId { get; set; }
        public long? PermissionId { get; set; }

        public virtual Menu Parent { get; set; }
        public virtual Permission Permission { get; set; }
        public virtual ICollection<Menu> InverseParent { get; set; }
    }
}
