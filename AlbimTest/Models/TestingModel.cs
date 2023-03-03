using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbimTest.Models
{
    public class TestingModel
    {
        public class Methodes
        {
            public int MethodeID { get; set; }
            public string MethodeName { get; set; }
        }
        public class MethodeParameters
        {
            public int MethodeID { get; set; }
            public object Parameter { get; set; }
        }
        public class ClassPropertes
        {
            public Type PropertyType { get; set; }
            public string PropertyName { get; set; }
        }
        /// <summary>
        /// obj 0 : methodesList
        /// obj 1 : ParametersList
        /// </summary>
        public class GetServiceMethodesResult
        {
            public List<Methodes> MethodListResult { get; set; } = new List<Methodes>();
            public List<MethodeParameters> MethodParametersResult { get; set; } = new List<MethodeParameters>();
        }
        
        public class GetPropDetailesResult
        {
            public List<ClassPropertes> ClassPropertesData { get; set; } = new List<ClassPropertes>();
        }

    }
}
