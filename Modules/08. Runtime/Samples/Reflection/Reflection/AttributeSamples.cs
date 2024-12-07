using Reflection;
using System.Reflection;

// К сборке
[assembly: CodeAuthor("Larry")]

namespace Reflection
{

    [AttributeUsage(AttributeTargets.All,
        AllowMultiple = false, Inherited = false)]
    public class CodeAuthorAttribute : Attribute
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Contact { get; set; } = "";

        public CodeAuthorAttribute(string name, string comment)
        {
            Name = name;
            Comment = comment;
        }

        public CodeAuthorAttribute(string name)
          : this(name, "")
        {
        }
    }

    // К классу
    [CodeAuthor("Moe", "Hi")]
    class MyClass
    {
        // К методу и параметру
        [CodeAuthor("Curly", Contact = "curly@stooges.com")]
        public void M([CodeAuthor("Ivan")] int a) { }

        // К возвращаемому значению
        [return: CodeAuthor("Ivan")]
        public string M() => "";
    }


    internal class AttributeSamples
    {
        [Test]
        public void ReadAttributes()
        {
            Type attrType = typeof(CodeAuthorAttribute);

            var asm = GetType().Assembly;
            if (asm.IsDefined(attrType, false))
            {
                Console.WriteLine("Assembly has an author");
            }

            List<CodeAuthorAttribute> codeAutorAtributes =
                    [asm.GetCustomAttribute<CodeAuthorAttribute>()];

            codeAutorAtributes.AddRange(
                asm.GetTypes()
                    .Where(t => t.GetCustomAttribute<CodeAuthorAttribute>() != null)
                    .Select(t => t.GetCustomAttribute<CodeAuthorAttribute>()!));

            foreach (CodeAuthorAttribute author in codeAutorAtributes)
            {
                Console.WriteLine("Name:         " + author.Name);
                Console.WriteLine("Notes:        " + author.Comment);
                Console.WriteLine("Contact info: " + author.Contact);
            }
        }
    }
}