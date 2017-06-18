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
                if ((type.CustomAttributes.FirstOrDefault() != null) ? type.CustomAttributes.FirstOrDefault().ToString().Contains("TestClassAttribute") : false)
                {
                    SpecialTypeTestLaunch(type);
                }
            }
        }

        private static void SpecialTypeTestLaunch(Type classTest)
        {
            object obj = Activator.CreateInstance(classTest);

            MethodInfo[] ClassTestMethods = classTest.GetMethods();

            foreach (var method in ClassTestMethods)
            {
                try
                {
                    if (method.GetCustomAttributes().FirstOrDefault().ToString().Contains("PrelaunchExecution"))
                    {
                        method.Invoke(obj, null);

                        TestPassed.Add(method.Name);

                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    TestFailed.Add(method.Name);
                    break;
                }

            }

            foreach (var method in ClassTestMethods)
            {
                try
                {
                    if (method.GetCustomAttributes().FirstOrDefault().ToString().Contains("TestMethod"))
                    {
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
                if (type.CustomAttributes.FirstOrDefault().ToString().Contains("TestFixtureAttribute"))
                {
                    TypeTestLaunch(type);
                }
            }
        }

        private static void TypeTestLaunch(Type classTest)
        {
            object obj = Activator.CreateInstance(classTest);

            MethodInfo[] ClassTestMethods = classTest.GetMethods();

            foreach (var method in ClassTestMethods)
            {
                try
                {
                    if (method.GetCustomAttributes().FirstOrDefault().ToString().Contains("SetUpAttribute"))
                    {
                        method.Invoke(obj, null);

                        TestPassed.Add(method.Name);

                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    TestFailed.Add(method.Name);
                    break;
                }
                
            }

            foreach (var method in ClassTestMethods)
            {
                try
                {
                    if (method.GetCustomAttributes().FirstOrDefault().ToString().Contains("TestAttribute"))
                    {
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
