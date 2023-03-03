using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Permission
{
    public class PermissionResultViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }  
    public class PermissionInputViewModel
    {
        public string Name { get; set; }
    } 

}
