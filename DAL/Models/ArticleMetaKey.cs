using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ArticleMetaKey
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Article Article { get; set; }
    }
}
