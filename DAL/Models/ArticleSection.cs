using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ArticleSection
    {
        public long Id { get; set; }
        public long ArticleId { get; set; }
        public int SectionId { get; set; }

        public virtual Article Article { get; set; }
    }
}
