using LoadSampleLib;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace LoadSample
{
    public class Test { public int A; public string B; }

    internal class Program
    {
        static void Main(string[] args)
        {
            AssemblyLoadContext.Default.Resolving += Resolving;

            var t = new Test { A = 5, B = "qqqqqqqqqqq" };

            Console.WriteLine(JsonHelper.GetJson(t));
        }

        private static Assembly Resolving(
            AssemblyLoadContext arg1,
            AssemblyName arg2)
        {
            var path = Path.Combine(AppContext.BaseDirectory,
                "lib", arg2.Name + ".dll");
            if (File.Exists(path))
                return arg1.LoadFromAssemblyPath(path);
            return null;
        }
    }
}
