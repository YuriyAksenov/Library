using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialTestLibrary.Test
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class TestClassAttribute : System.Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class PrelaunchExecution : System.Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class TestMethod : System.Attribute
    {
    }

    public static class Test
    {
        public static void AreEqual(object expected, object actual)
        {
            if (!Object.Equals(expected, actual)) throw new Exception("Объекты не равны");
        }

        public static void IsEmpty(System.Collections.IEnumerable collection)
        {
            int k = 0;
            foreach (var item in collection)
            {
                if (item != null) k++;
            }
            if (k != 0) throw new Exception("Коллекция не пуста");
        }

        public static void Null(object anObject)
        {
            if (anObject != null) throw new Exception("Не Null");
        }
    }
}
