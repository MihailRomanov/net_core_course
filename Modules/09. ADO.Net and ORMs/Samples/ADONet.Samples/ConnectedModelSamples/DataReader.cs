using Microsoft.Data.SqlClient;
using System.Data;

namespace ConnectedModelSamples
{
    public class DataReader
    {
        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";

        [Test]
        public void Sample1_SimpleRead()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT CompanyName, City, Region FROM Northwind.Customers";

                connection.Open();
                using (IDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Console.WriteLine("{0} - {1}, {2}",
                            reader["CompanyName"],
                            reader["City"],
                            reader["Region"]);
                    }

                }
            }
        }

        [Test]
        public void Sample2_ReadSeveralRowSets()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    "SELECT * " +
                    "FROM Northwind.Orders " +
                    "where OrderID = @orderId;" +

                    "SELECT p.ProductName, ods.UnitPrice, ods.Quantity " +
                    "FROM Northwind.[Order Details] ods " +
                    "LEFT JOIN Northwind.Products p ON p.ProductID = ods.ProductID " +
                    "WHERE ods.OrderID = @orderId;";

                command.Parameters.AddWithValue("@orderId", 10262);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    Console.WriteLine("{0} ({1})", reader["OrderID"], reader["OrderDate"]);

                    reader.NextResult();

                    while (reader.Read())
                    {
                        Console.WriteLine("\t{0} - {1}", reader["ProductName"], reader["UnitPrice"]);
                    }
                }

            }

        }
    }
}
