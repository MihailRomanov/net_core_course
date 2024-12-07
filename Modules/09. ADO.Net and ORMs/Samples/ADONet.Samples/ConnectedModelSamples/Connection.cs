using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace ConnectedModelSamples
{
    public class Connection
    {
        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";

        [Test]
        public void Sample1_CreateOpenAndCloseConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            // Some command

            connection.Close();
        }

        [Test]
        public void Sample2_CloseConnectionThroughUsingScope()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                // Some command
            }
        }

        [Test]
        public void Sample3_UsingConnectionStringBuilder()
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "(localdb)\\MSSQLLocalDB",
                InitialCatalog = "Northwind",
                IntegratedSecurity = true
            };

            Console.WriteLine(connectionStringBuilder.ConnectionString);

            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
            }
        }

        [Test]
        public void Sample3_1_ChangeConnectionString()
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(ConnectionString);

            Console.WriteLine(connectionStringBuilder.ConnectionString);

            connectionStringBuilder.IntegratedSecurity = false;
            connectionStringBuilder.UserID = "mihail_romanov";
            connectionStringBuilder.Password = "123";

            Console.WriteLine(connectionStringBuilder.ConnectionString);
        }
    }
}
