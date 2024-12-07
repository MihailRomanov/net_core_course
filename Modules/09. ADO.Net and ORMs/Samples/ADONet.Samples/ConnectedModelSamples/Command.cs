using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Xml.Linq;

namespace ConnectedModelSamples
{
    public class Command
    {
        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";

        [Test]
        public void Sample1_CreateCommand()
        {
            // From connection object
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
            }

            // Manual
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                IDbCommand command = new SqlCommand();
                command.Connection = connection;
            }
        }

        [Test]
        public void Sample2_PrepareAndExecuteCommand()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "select count(*) from Northwind.Customers";
                command.CommandType = CommandType.Text;
                var customersCount = command.ExecuteScalar();

                Console.WriteLine(customersCount);
            }
        }

        [Test]
        public void Sample3_DifferentComandTypes()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                // Text
                var command1 = connection.CreateCommand();
                command1.CommandText = "SELECT * FROM Northwind.Products";
                command1.CommandType = CommandType.Text;

                var command2 = connection.CreateCommand();
                command2.CommandText = "exec sp_helpdb";
                command2.CommandType = CommandType.Text;

                // Stored procedure
                var command3 = connection.CreateCommand();
                command3.CommandText = "sp_helpdb";
                command3.CommandType = CommandType.StoredProcedure;
            }

            using (IDbConnection connection = new OleDbConnection(
                "Provider=SQLOLEDB;Data Source=(local);Database=Northwind;Integrated Security=SSPI"))
            {
                var command = connection.CreateCommand();
                command.CommandText = "Northwind.Customers";
                command.CommandType = CommandType.TableDirect;
            }
        }

        [Test]
        public void Sample4_1_ExecuteCommandAsDataReader()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT CompanyName FROM Northwind.Customers";

                connection.Open();
                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine(reader["CompanyName"]);
                }
            }

        }

        [Test]
        public void Sample4_2_ExecuteCommandAsScalar()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT count(*) FROM Northwind.Customers";

                connection.Open();
                int customerCount = (int)command.ExecuteScalar();

                Console.WriteLine(customerCount);
            }
        }

        [Test]
        public void Sample4_3_ExecuteCommandNonQuery()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE Northwind.Products SET UnitPrice = UnitPrice - 0.0002";

                connection.Open();
                int updatedRows = command.ExecuteNonQuery();

                Console.WriteLine(updatedRows);
            }
        }

        [Test]
        public void Sample4_4_ExecuteCommandXmlReader()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Northwind.Customers FOR XML AUTO, ROOT('Customers')";

                connection.Open();
                XmlReader xmlReader = command.ExecuteXmlReader();

                var xmlDocument = XDocument.Load(xmlReader);

                Console.WriteLine(xmlDocument.ToString(SaveOptions.OmitDuplicateNamespaces));
            }
        }

        [Test]
        public void Sample5_CommandWithParameters()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT count(*) FROM Northwind.Products WHERE UnitPrice >= @minPrice";

                var minPrice = command.CreateParameter();
                minPrice.ParameterName = "@minPrice";
                minPrice.DbType = DbType.Decimal;
                minPrice.Value = 50;
                command.Parameters.Add(minPrice);

                var count = command.ExecuteScalar();
                Console.WriteLine(count);
            }

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT count(*) FROM Northwind.Products WHERE UnitPrice >= @minPrice";

                command.Parameters.AddWithValue("@minPrice", 50m);

                var count = command.ExecuteScalar();
                Console.WriteLine(count);
            }
        }

        [Test]
        public void Sample6_CallStoredProcedure()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "[Northwind].[CustOrdersStatistic]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@CustomerID", "BONAP");
                var all = command.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@All",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Output
                    });

                var shipped = command.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@Shipped",
                        DbType = DbType.Int32,
                        Direction = ParameterDirection.Output
                    });

                command.ExecuteNonQuery();

                Console.WriteLine("{0} {1}", all.Value, shipped.Value);
            }
        }

    }
}
