using Bogus;
using TestingAndDebuggingSamples.Model;

namespace TestingAndDebuggingSamples.TestDataGenerators
{
    public class ProductGenerator : ITestDataGenerator<Product>
    {
        private readonly NorthwindContext northwindContext;
        private Faker<Product> faker;

        public ProductGenerator(NorthwindContext northwindContext)
        {
            this.northwindContext = northwindContext;
            faker = new Faker<Product>()
                .StrictMode(false)
                .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
                .RuleFor(p => p.UnitPrice, f => f.Random.Decimal())
                .RuleFor(p => p.UnitsInStock, f => f.Random.Short());
        }

        public Product Generate()
        {
            var product = faker.Generate();
            northwindContext.Add(product);
            return product;
        }

        public IEnumerable<Product> Generate(int count)
        {
            var products = faker.Generate(count);
            northwindContext.AddRange(products);
            return products;
        }
    }

}