using NUnit.Framework;
using SpecialTestLibrary.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static List<string> TestPassed = new List<string>();
        static List<string> TestFailed = new List<string>();

        static void Main(string[] args)
        {
            //DllTestLaunch("TestLibrary.Test.dll");
            SpecialDllTestLaunch("SpecialTestLibrary.Test.dll");




            Console.WriteLine("Количество пройденных тестов: " + TestPassed.Count + "\n" + "Количество не пройденных тестов: " + TestFailed.Count);

            Console.WriteLine("Пройденные тесты: ");
            foreach (var item in TestPassed)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Не пройденные тесты: ");
            foreach (var item in TestFailed)
            {
                Console.WriteLine(item);
            }
        }

        static void SpecialDllTestLaunch(string pathToLoadDll)
        {
            new NUnit.Framework.Internal.TestExecutionContext().EstablishExecutionEnvironment();

            Assembly testingDll = Assembly.LoadFrom(pathToLoadDll);

            Type[] typesOfDll = testingDll.GetTypes();

            foreach (var type in typesOfDll)
            {
                if (type.IsDefined(typeof(TestClassAttribute)))
                {
                    SpecialTypeTestLaunch(type);
                }
            }
        }

        private static void SpecialTypeTestLaunch(Type classTest)
        {
            object obj = Activator.CreateInstance(classTest);

            MethodInfo[] ClassTestMethods = classTest.GetMethods();

            MethodInfo StartUp = null;

            foreach (var method in ClassTestMethods)
            {
                if (method.IsDefined(typeof(PrelaunchExecution)))
                {
                    StartUp = method;

                    break;
                }
            }

            foreach (var method in ClassTestMethods)
            {
                try
                {
                    if (method.IsDefined(typeof(TestMethod)))
                    {
                        StartUp?.Invoke(obj, null);
                        method.Invoke(obj, null);

                        TestPassed.Add(method.Name);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    TestFailed.Add(method.Name);
                }

            }
        }

        static void DllTestLaunch(string pathToLoadDll)
        {
            new NUnit.Framework.Internal.TestExecutionContext().EstablishExecutionEnvironment();

            Assembly testingDll = Assembly.LoadFrom(pathToLoadDll);

            Type[] typesOfDll = testingDll.GetTypes();

            foreach (var type in typesOfDll)
            {
                if (type.IsDefined(typeof(TestFixtureAttribute)))
                {
                    TypeTestLaunch(type);
                }
            }
        }

        private static void TypeTestLaunch(Type classTest)
        {
            object obj = Activator.CreateInstance(classTest);

            MethodInfo[] ClassTestMethods = classTest.GetMethods();

            MethodInfo StartUp = null;

            foreach (var method in ClassTestMethods)
            {
                if (method.IsDefined(typeof(SetUpAttribute)))
                {
                    StartUp = method;
                    break;
                }
            }

            foreach (var method in ClassTestMethods)
            {
                try
                {
                    if (method.IsDefined(typeof(TestAttribute)))
                    {
                        StartUp?.Invoke(obj, null);
                        method.Invoke(obj, null);

                        TestPassed.Add(method.Name);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    TestFailed.Add(method.Name);
                }

            }
        }
    }
}
