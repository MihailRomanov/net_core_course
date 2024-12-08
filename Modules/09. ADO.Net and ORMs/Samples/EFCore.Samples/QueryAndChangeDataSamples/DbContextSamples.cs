using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QueryAndChangeDataSamples.Model;

namespace QueryAndChangeDataSamples
{
    public class DbContextSamples
    {
        const string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True";

        [Test]
        public void DirectContextCreation()
        {
            var options =
                new DbContextOptionsBuilder<NorthwindContext>()
                    .UseSqlServer(connectionString)
                    .EnableDetailedErrors()
                    .Options;

            using (var context = new NorthwindContext(options))
            {
                var firstProducts =
                    context.Products.Take(10).ToList();

                // Эквивалент context.Categories.ToList();
                var allCategories = context.Set<Category>().ToList();
            }
        }

        public class CategoryService(NorthwindContext context)
        {
            public IEnumerable<Category> GetAll()
                => context.Categories.ToList();
        }

        [Test]
        public void UsingMsDI()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<NorthwindContext>(
                    optionsBuilder => optionsBuilder.UseSqlServer(connectionString));

            serviceCollection.AddTransient<CategoryService>();


            var serviceProvider = serviceCollection.BuildServiceProvider();

            var categoryService = serviceProvider.GetService<CategoryService>()!;
            Console.WriteLine(categoryService.GetAll().Count());
        }
    }
}
