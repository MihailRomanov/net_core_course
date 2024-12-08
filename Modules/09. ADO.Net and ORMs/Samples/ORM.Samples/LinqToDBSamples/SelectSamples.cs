using LinqToDB;
using LinqToDBSamples.Model;

namespace LinqToDBSamples
{
    public class SelectSamples
    {
        private const string ConnectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";

        private DataOptions dataOptions;

        [SetUp]
        public void SetUp()
        {
            dataOptions = new DataOptions().UseSqlServer(ConnectionString);
        }

        [Test]
        public void JoinQuery()
        {
            var categoryName = "Beverages";
            using (var db = new Northwind(dataOptions))
            {
                var products = from p in db.Products
                               join c in db.Categories on p.CategoryID equals c.Id
                               where c.Name == categoryName
                               select p;

                foreach (var p in products)
                    Console.WriteLine(p.Name);
            }
        }


        [Test]
        public void AssociationQuery()
        {
            var categoryName = "Beverages";
            using (var db = new Northwind(dataOptions))
            {
                var products = from p in db.Products
                               where p.Category.Name == categoryName
                               select p;

                foreach (var p in products)
                    Console.WriteLine(p.Name);
            }
        }

        [Test]
        public void NavigationSample()
        {
            var categoryName = "Beverages";
            using (var db = new Northwind(dataOptions))
            {
                var category = db.Categories.Single(c => c.Name == categoryName);
                Console.WriteLine(category.Description);

                var products = db.Products.Where(p => p.CategoryID == category.Id);

                foreach (var p in products)
                    Console.WriteLine(p.Name);
            }
        }

    }
}
