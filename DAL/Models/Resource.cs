using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Resource
    {
        public Resource()
        {
            ResourceOperations = new HashSet<ResourceOperation>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<ResourceOperation> ResourceOperations { get; set; }
    }
}
