using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbimTest.Methodes
{
    //class GenericMockData<TEntity> where TEntity : class
    //{
    //    public TEntity GetByID(List<object> MethodeParameter)
    //    {

    //        return 
    //    }

    //}

    public class GenericMockData
    {
        public class Randomizer
        {
            private Dictionary<Type, Delegate> _randoms
                = new Dictionary<Type, Delegate>();

            public void Add<T>(Func<T> generate)
            {
                _randoms.Add(typeof(T), generate);
            }

            public T RandomizeParamValue<T>()
            {
                return ((Func<T>)_randoms[typeof(T)])();
            }
        }
    }
}
