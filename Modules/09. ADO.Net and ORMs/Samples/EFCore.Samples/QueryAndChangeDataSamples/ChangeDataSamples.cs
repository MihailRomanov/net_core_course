using Microsoft.EntityFrameworkCore;
using QueryAndChangeDataSamples.Model;

namespace QueryAndChangeDataSamples
{
    internal class ChangeDataSamples
    {
        [Test]
        public void UnitOfWorkSample()
        {
            using (var db = new NorthwindContext())
            {
                var productsForSpliting = db
                    .Products
                    .Where(p => !p.Discontinued
                        && p.UnitsInStock > 100)
                    .ToList();

                var dscCategory = new Category
                {
                    CategoryName = "Dsc",
                    Description = "Discontinued"
                };

                foreach (var product in productsForSpliting)
                {
                    var newProduct = (Product)product.Clone();
                    newProduct.Category = dscCategory;
                    newProduct.ProductId = 0;
                    newProduct.Discontinued = true;
                    newProduct.ProductName += " (Dsc)";
                    newProduct.UnitsInStock /= 2;
                    
                    product.UnitsInStock -= newProduct.UnitsInStock;

                    db.Products.Add(newProduct);
                }

                db.SaveChanges();
            }
        }

        [Test]
        public void ExecuteDelete()
        {
            using (var db = new NorthwindContext())
            {
                var deletedCount = db.Products
                    .Where(p => p.UnitsInStock < 10)
                    .ExecuteDelete();

                Console.WriteLine(deletedCount);
            }
        }

        [Test]
        public void ExecuteUpdate()
        {
            using (var db = new NorthwindContext())
            {
                var updatedCount = db.Products
                    .Where(p => p.Discontinued 
                        && p.UnitsInStock > 100)
                    .ExecuteUpdate(setter =>
                        setter.SetProperty(
                            p => p.UnitsInStock,
                            p => p.UnitsInStock - 3));

                Console.WriteLine(updatedCount);
            }
        }
    }
}
