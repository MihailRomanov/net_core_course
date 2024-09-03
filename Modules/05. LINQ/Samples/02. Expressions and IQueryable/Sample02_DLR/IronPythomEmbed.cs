using System;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using NUnit.Framework;

namespace Sample02_DLR
{
    public class IronPythomEmbed
    {
        [Test]
        public void RunFromFile()
        {
            var setup = Python.CreateRuntimeSetup(null);
            var env = new ScriptRuntime(setup);

            var scope = env.Globals;
            scope.SetVariable("b", 7);

            scope = env.ExecuteFile("IronPython.py");

            var a = scope.GetVariable<int>("a");
            Console.WriteLine(a);
        }
    }
}
