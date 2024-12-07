using TestingAndDebuggingSamples.TestDataGenerators;

namespace TestingAndDebuggingSamples
{
    public class InMemoryTestsSamples
    {
        [Test]
        [TestCase("InMemory")]
        [TestCase("Sqlite")]
        public void InMemoryEFTests(string type)
        {
            using var context = type == "InMemory"
                ? NorthwindContextHelpers.GetInMemoryContext()
                : NorthwindContextHelpers.GetSqliteContext();

            new CategoryGenerator(context)
                .WithProduct(new ProductGenerator(context))
                .Generate(10)
                .ToList();

            context.SaveChanges();

            foreach (var c in context.Categories.Take(2))
                Console.WriteLine(c.CategoryName);
        }
    }
}
