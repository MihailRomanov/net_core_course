using System.Reflection.Emit;
using System.Reflection;
using FluentIL;

namespace Reflection
{
    public interface IHello
    {
        void SayHello(string toWhom);
    }

    public class Hello : IHello
    {
        public void SayHello(string toWhom)
        {
            Console.WriteLine(
                string.Format("Hello, {0} World!", toWhom));
        }
    }

    internal class EmitSamples
    {
        [Test]
        public void RawEmit()
        {
            var asmName = new AssemblyName("HelloWorld");

            var asmBuilder = AssemblyBuilder
                .DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);
            var moduleBuilder = asmBuilder.DefineDynamicModule("HelloWorld");

            TypeBuilder typeBuilder = moduleBuilder.DefineType(
                "Hello",
                TypeAttributes.Public,
                typeof(object),
                [typeof(IHello)]);

            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                "SayHello",
                 MethodAttributes.Private | MethodAttributes.Virtual,
                 typeof(void),
                 [typeof(string)]);

            typeBuilder.DefineMethodOverride(methodBuilder,
                typeof(IHello).GetMethod("SayHello")!);

            ILGenerator il = methodBuilder.GetILGenerator();

            // string.Format("Hello, {0} World!", toWhom)
            il.Emit(OpCodes.Ldstr, "Hello, {0} World!");
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Call,
                typeof(string).GetMethod("Format", [typeof(string), typeof(object)])!);

            // Console.WriteLine("Hello, World!");
            il.Emit(OpCodes.Call,
                typeof(Console).GetMethod("WriteLine", [typeof(string)])!);
            il.Emit(OpCodes.Ret);

            Type type = typeBuilder.CreateType();

            IHello hello = (IHello)Activator.CreateInstance(type)!;

            hello.SayHello("Emit");
        }

        [Test]
        public void FluentIL()
        {
            var typeBuilder = TypeFactory
                .Default
                .NewType("Hello")
                .Implements<IHello>();

            typeBuilder
                .NewMethod("SayHello")
                .Public()
                .Virtual()
                .Param<string>("toWhom")
                .Body()
                // string.Format("Hello, {0} World!", toWhom)
                .LdStr("Hello, {0} World!")
                .LdArg1()
                .Call(typeof(string), "Format", typeof(string), typeof(object))
                // Console.WriteLine("Hello, World!");
                .Call(typeof(Console), "WriteLine", typeof(string))
                .Ret();

            var type = typeBuilder.CreateType();
            IHello hello = (IHello)Activator.CreateInstance(type)!;
            hello.SayHello("FluentIL");
        }
    }
}
