using EFCoreSamples.Model;
using Microsoft.EntityFrameworkCore;

namespace EFSamples
{
    public class SelectSamples
    {
        private const string ConnectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;MultipleActiveResultSets=True";

        DbContextOptions options;

        [SetUp]
        public void SetUp()
        {
            options = new DbContextOptionsBuilder()
                .UseSqlServer(ConnectionString)
                .Options;
        }

        [Test]
        public void LazyLoading()
        {
            options = new DbContextOptionsBuilder()
                .UseLazyLoadingProxies()
                .UseSqlServer(ConnectionString)
                .Options;

            using (var db = new Northwind(options))
            {
                foreach (var p in db.Products)
                {
                    Console.WriteLine(p.Name + " | " + p.Category.Name);
                }
            }
        }

        [Test]
        public void ExplicitlyLoading()
        {
            using (var db = new Northwind(options))
            {
                foreach (var p in db.Products)
                {
                    db.Entry(p).Reference(t => t.Category).Load();
                    Console.WriteLine(p.Name + " | " + p.Category.Name);
                }
            }
        }

        [Test]
        public void EagerlyLoading()
        {
            using (var db = new Northwind(options))
            {
                foreach (var p in db.Products.Include(t => t.Category))
                {
                    Console.WriteLine(p.Name + " | " + p.Category.Name);
                }
            }
        }

        [Test]
        public void NavigationSample()
        {
            var categoryName = "Beverages";
            using (var db = new Northwind(options))
            {
                var category = db.Categories.Single(c => c.Name == categoryName);
                Console.WriteLine(category.Description);

                foreach (var p in category.Products)
                    Console.WriteLine(p.Name);
            }
        }

    }
}
