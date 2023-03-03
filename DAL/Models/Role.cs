using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Role
    {
        public Role()
        {
            InverseParent = new HashSet<Role>();
            RolePermissions = new HashSet<RolePermission>();
            UserRoles = new HashSet<UserRole>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public string Caption { get; set; }
        public long? ParentId { get; set; }

        public virtual Role Parent { get; set; }
        public virtual ICollection<Role> InverseParent { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
