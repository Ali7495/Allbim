using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Info
{
    public class InfoInputViewModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Slug { get; set; }
    }
    public class InfoResultViewModel
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Slug { get; set; }
    }
}
