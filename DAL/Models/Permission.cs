using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Permission
    {
        public Permission()
        {
            Menus = new HashSet<Menu>();
            ResourceOperations = new HashSet<ResourceOperation>();
            RolePermissions = new HashSet<RolePermission>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<ResourceOperation> ResourceOperations { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
