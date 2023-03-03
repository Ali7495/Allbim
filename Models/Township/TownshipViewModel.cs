using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Township
{
    public class TownshipResultViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProvinceId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
    public class TownshipInputViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProvinceId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
