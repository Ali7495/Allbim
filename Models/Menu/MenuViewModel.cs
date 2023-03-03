using System.Collections.Generic;
using Models.Permission;
using System;
using System.Linq;

namespace Services.ViewModels.Menu
{
    public class MenuResultViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long PermissionId { get; set; }

        public virtual PermissionResultViewModel Permission { get; set; }
    }   
    public class MenuInputViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long PermissionId { get; set; }

        public virtual PermissionResultViewModel Permission { get; set; }
    }

    public class AccessMenuViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public long Order { get; set; }
    }
    
}

public class MenuTreeItemViewModel
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public long Order { get; set; }
    
    public long? ParentId { get; set; }
    public IEnumerable<MenuTreeItemViewModel> Children { get; set; }
}
