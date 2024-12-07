using LinqToDB;
using LinqToDBSamples.Model;

namespace LinqToDBSamples
{
    public class UpdateSamples
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
        public void UpdateByOneEntity()
        {
            int productId;

            using (var db = new Northwind(dataOptions))
            {
                var p = new Product()
                {
                    Name = "New product",
                    CategoryID = 1
                };

                productId = Convert.ToInt32(db.InsertWithIdentity(p));
            }

            using (var db = new Northwind(dataOptions))
            {
                var p = db.Products.Single(t => t.Id == productId);
                p.UnitPrice++;
                db.Update(p);
            }

            using (var db = new Northwind(dataOptions))
            {
                var p = db.Products.Single(t => t.Id == productId);
                db.Delete(p);
            }
        }


        [Test]
        public void UpdateConstructQuery()
        {
            using (var db = new Northwind(dataOptions))
            {
                db.Products
                  .Where(p => p.UnitsInStock == 0)
                  .Set(p => p.Discontinued, true)
                  .Update();
            }

            using (var db = new Northwind(dataOptions))
            {
                db.Products
                  .Where(p => p.Discontinued)
                  .Delete();
            }
        }
    }
}
