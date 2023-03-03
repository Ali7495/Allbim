using Models.RolePermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Role
{
    public class RoleResultViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }

        public virtual ICollection<RolePermissionResultViewModel> RolePermissions { get; set; }
    }
    public class RoleInputViewModel
    {
    
        public string Name { get; set; }
        public string Caption { get; set; }

        public virtual ICollection<RolePermissionResultViewModel> RolePermissions { get; set; }
    }
}
