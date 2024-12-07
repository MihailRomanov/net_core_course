using Dapper;
using DapperSamples.Model;
using Microsoft.Data.SqlClient;

namespace DapperSamples
{
    public class SelectSamples
    {
        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";

        [Test]
        public void FilteredSelect()
        {
            var categoryName = "Beverages";
            var query = "select p.* " +
                        "from Northwind.Products p " +
                        "join Northwind.Categories c on c.CategoryID = p.CategoryID " +
                        "where c.CategoryName = @categoryName";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var products = connection.Query<Products>(query, new { categoryName });

                foreach (var p in products)
                    Console.WriteLine(p.ProductName);
            }
        }
    }
}
