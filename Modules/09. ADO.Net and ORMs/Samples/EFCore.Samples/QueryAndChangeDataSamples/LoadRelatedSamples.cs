using Microsoft.EntityFrameworkCore;
using QueryAndChangeDataSamples.Model;
using System.Linq;

namespace QueryAndChangeDataSamples
{
    internal class LoadRelatedSamples
    {
        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";

        [Test]
        public void DefaultMode()
        {
            using (var db = new NorthwindContext())
            {
                var firstProduct = db.Products.First();

                Console.WriteLine(
                    firstProduct.Category?.CategoryName ?? "Не загружен");

                var allCategories = db.Categories.ToList();

                Console.WriteLine(
                    firstProduct.Category?.CategoryName ?? "Не загружен");
            }
        }


        [Test]
        public void EagerLoading()
        {
            using (var db = new NorthwindContext())
            {
                var products = db.Products
                    .Where(p => p.Discontinued)
                    .Include(p => p.Category)
                    .Include(p => p.Category.Products)
                    .ToList();

                foreach (var p in products)
                {
                    Console.WriteLine(
                        p.Category?.CategoryName ?? "Не загружен");

                }
            }
        }


        [Test]
        public void ExplicitReferenceLoading()
        {
            using (var db = new NorthwindContext())
            {
                foreach (var p in db.Products.ToList())
                {
                    if (p.Discontinued)
                        db.Entry(p)
                            .Reference(p => p.Category)
                            .Load();
                }
            }
        }

        [Test]
        public void ExplicitCollectionsLoading()
        {
            using (var db = new NorthwindContext())
            {
                foreach (var c in db.Categories.ToList())
                {
                    db.Entry(c)
                        .Collection(c => c.Products)
                        .Load();
                }
            }
        }

        [Test]
        public void ExplicitCollectionsQuery()
        {
            using (var db = new NorthwindContext())
            {
                foreach (var c in db.Categories.ToList())
                {
                    var count = db.Entry(c)
                        .Collection(c => c.Products)
                        .Query()
                        .Count();

                    db.Entry(c)
                        .Collection(c => c.Products)
                        .Query()
                        .Where(p => p.Discontinued)
                        .Load();
                }
            }
        }

        [Test]
        public void LazzyLoading()
        {
            var options = new DbContextOptionsBuilder<NorthwindContext>()
                .UseSqlServer(ConnectionString)
                .UseLazyLoadingProxies()
                .Options;

            using (var db = new NorthwindContext(options))
            {
                foreach (var c in db.Categories.ToList())
                {
                    Console.WriteLine(c.CategoryName);
                    Console.WriteLine(string.Join(" | ",
                        c.Products.Select(p => p.ProductName)));
                }
            }
        }
    }
}
