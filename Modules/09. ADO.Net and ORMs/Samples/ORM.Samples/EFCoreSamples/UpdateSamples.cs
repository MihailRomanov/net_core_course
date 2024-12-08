using EFCoreSamples.Model;
using Microsoft.EntityFrameworkCore;

namespace EFSamples
{
    public class UpdateSamples
    {
        private const string ConnectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";


        [Test]
        public void SaveChanges()
        {
            using (var db = new Northwind(new DbContextOptionsBuilder()
                .UseSqlServer(ConnectionString)
                .Options))
            {
                var category = new Category
                {
                    Name = "New category",
                    Description = "New category description"
                };
                db.Categories.Add(category);

                db.Products.Add(new Product
                {
                    Name = "New product1",
                    Category = category
                });
                db.Products.Add(new Product
                {
                    Name = "New product2",
                    Category = category
                });

                var beveragesProducts = db.Products
                    .Where(p => p.Category.Name == "Beverages");
                foreach (var bp in beveragesProducts)
                    bp.Category = category;

                Console.WriteLine(db.SaveChanges());
            }
        }
    }
}
