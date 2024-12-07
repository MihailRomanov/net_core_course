using NHibernate;
using NHibernate.Criterion;
using NHibernateSamples.Model;

namespace NHibernateSamples
{
    public class SelectSamples
    {
        ISessionFactory sessionFactory;

        [SetUp]
        public void SetUp()
        {
            var assembly = typeof(Product).Assembly;
            var config = new NHibernate.Cfg
                .Configuration()
                .Configure(assembly, "NHibernateSamples.nhibernate.cfg");
            sessionFactory = config.BuildSessionFactory();
        }

        [TearDown]
        public void TearDown()
        {
            sessionFactory.Dispose();
        }

        [Test]
        public void TextQuery()
        {
            var categoryName = "Beverages";

            using (var session = sessionFactory.OpenSession())
            {
                var query = session.CreateQuery("from Product p where p.Category.Name = :categoryName");
                query.SetString("categoryName", categoryName);

                foreach (var c in query.List<Product>())
                    Console.WriteLine(c.Name + " | " + c.Category.Name);
            }
        }

        [Test]
        public void CriteriaAPIQuery()
        {
            var categoryName = "Beverages";

            using (var session = sessionFactory.OpenSession())
            {
                var query = session.CreateCriteria<Product>();
                query.CreateAlias("Category", "c").Add(Restrictions.Eq("c.Name", categoryName));

                foreach (var c in query.List<Product>())
                    Console.WriteLine(c.Name + " | " + c.Category.Name);
            }
        }


        [Test]
        public void StructuredCriteriaAPIQuery()
        {
            var categoryName = "Beverages";

            using (var session = sessionFactory.OpenSession())
            {
                var query = session.QueryOver<Product>();
                query.JoinQueryOver(p => p.Category).Where(c => c.Name == categoryName);

                foreach (var c in query.List())
                    Console.WriteLine(c.Name + " | " + c.Category.Name);
            }
        }

        [Test]
        public void SQLQuery()
        {
            var categoryName = "Beverages";
            var queryString = "select p.* " +
                        "from Northwind.Products p " +
                        "join Northwind.Categories c on c.CategoryID = p.CategoryID " +
                        "where c.CategoryName = :categoryName";

            using (var session = sessionFactory.OpenSession())
            {
                var query = session.CreateSQLQuery(queryString);
                query.SetString("categoryName", categoryName);
                query.AddEntity("p", typeof(Product));

                foreach (var c in query.List<Product>())
                    Console.WriteLine(c.Name + " | " + c.Category.Name);
            }
        }

        [Test]
        public void LINQQuery()
        {
            var categoryName = "Beverages";

            using (var session = sessionFactory.OpenSession())
            {
                var query = session.Query<Product>()
                    .Where(p => p.Category.Name == categoryName);

                foreach (var c in query)
                    Console.WriteLine(c.Name + " | " + c.Category.Name);
            }
        }
    }
}
