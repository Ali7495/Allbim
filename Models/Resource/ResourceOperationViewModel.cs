using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Models.Permission;

namespace Models.Resource
{
    public class ResourceOperationViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Class { get; set; }
        public string Key { get; set; }
        public long? ResourceId { get; set; }
        public long? PermissionId { get; set; }

        public virtual PermissionResultViewModel Permission { get; set; }
        public virtual ResourceViewModel Resource { get; set; }



    }
    public class ResourceOperationInputViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Class { get; set; }
        public string Key { get; set; }
        public long? ResourceId { get; set; }
        public long? PermissionId { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
