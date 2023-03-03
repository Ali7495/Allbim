using AlbimTest.Models;
using FakeItEasy;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static AlbimTest.Models.ProtoTypes;
using static AlbimTest.Models.TestingModel;

namespace AlbimTest.Methodes
{
    public class CmnMethodes
    {
        public GetServiceMethodesResult GetServicemethodesData(MethodInfo[] _methodInfos)
        {
            int MethodeID = 1;
            GetServiceMethodesResult MethodListResult = new GetServiceMethodesResult();
            foreach (MethodInfo m in _methodInfos)
            {
                if (m.IsPublic)
                {
                    if (typeof(Task).IsAssignableFrom(m.ReturnParameter.ParameterType))
                    {
                        MethodListResult.MethodListResult.Add(new TestingModel.Methodes { MethodeID = MethodeID, MethodeName = m.Name });
                        ParameterInfo[] Parameters = m.GetParameters();
                        foreach (var item in Parameters)
                            MethodListResult.MethodParametersResult.Add(new TestingModel.MethodeParameters { MethodeID = MethodeID, Parameter = item.ParameterType });
                        MethodeID++;
                    }
                }
            }
            return MethodListResult;
        }
        public object?[] GenerateFakeData(List<object> _listData)
        {
            object Output = new object();
            List<object> OutputList = new List<object>();
            var rz = new GenericMockData.Randomizer();
            var rnd = new Random();
            for (int i = 0; i < _listData.Count; i++)
            {
                Type paramType = (System.Type)_listData[i];
                OutputList.Add(RandomizeMethod_V1(paramType));

                //Type _Parametertype = _listData[i].GetType();
                //if (NumericTypes.RetuenNoTypes().Contains(_Parametertype))
                //{
                //    rz.Add(() => rnd.Next());
                //    Output = rz.RandomizeParamValue<long>();
                //}
                //else if(paramType.FullName == "System.String")
                //{
                //    rz.Add(() => rnd.Next());
                //    rz.Add(() => new[] { "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"}.ElementAt(rnd.Next(0, 26)));
                //    Output = rz.RandomizeParamValue<string>();
                //}
            }
            return OutputList.ToArray();
        }
        public object RandomizeMethod_V1(Type paramType)
        {
            var rz = new GenericMockData.Randomizer();
            var rnd = new Random();
            Type _Parametertype = paramType.GetType();
            if (NumericTypes.RetuenNoTypes().Contains(_Parametertype))
            {
                rz.Add(() => rnd.Next());
                return rz.RandomizeParamValue<long>();
            }
            else if (paramType.FullName == "System.String")
            {
                rz.Add(() => rnd.Next());
                rz.Add(() => new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" }.ElementAt(rnd.Next(0, 26)));
                return rz.RandomizeParamValue<string>();
            }
            else
            {
                object o = Activator.CreateInstance(paramType);
                return o;
            }
        }
    }
}
