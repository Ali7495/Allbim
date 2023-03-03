using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ResourceOperation
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Class { get; set; }
        public string Key { get; set; }
        public long? ResourceId { get; set; }
        public long? PermissionId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual Resource Resource { get; set; }
    }
}
