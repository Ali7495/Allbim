using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbimTest.Models
{
    class ProtoTypes
    {
        public static class NumericTypes
        {
            public static Type TypeName { get; set; }
            public static List<Type> RetuenNoTypes()
            {
                List<Type> types = new List<Type>();
                types.Add(typeof(int));
                types.Add(typeof(Byte));
                types.Add(typeof(SByte));
                types.Add(typeof(UInt16));
                types.Add(typeof(UInt32));
                types.Add(typeof(UInt64));
                types.Add(typeof(Int16));
                types.Add(typeof(Int32));
                types.Add(typeof(Int64));
                types.Add(typeof(Decimal));
                types.Add(typeof(Double));
                types.Add(typeof(Single));
                return types;
            }
        }


    }
}
