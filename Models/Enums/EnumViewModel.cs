using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Enums
{
    public class EnumViewModel
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCaption { get; set; }
        public int EnumId { get; set; }
        public string EnumCaption { get; set; }
        public byte? Order { get; set; }
        public byte? IsEnable { get; set; }
        public string Description { get; set; }
        
        public string Name { get; set; }
        public string Value { get; set; }
       
    }
    
    public class BodyNoDamageDiscountYearOutPutViewModel
    {
        public int EnumId { get; set; }
        public string EnumCaption { get; set; }
    }
    public class ThirdInsuranceCreditMonthViewModel
    {
        public int EnumId { get; set; }
        public string EnumCaption { get; set; }
    }
    public class ThirdMaxFinancialCoverViewModel
    {
        public int EnumId { get; set; }
        public string EnumCaption { get; set; }
    }
}