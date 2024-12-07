using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.Common;

namespace ADONetBasicSamples
{
    public class ProviderInvariantProgramming
    {
        // Чтобы не сбрасывалась на время тестов in-memory база
        SqliteConnection sharedConnection;

        [SetUp]
        public void Setup()
        {
            DbProviderFactories.RegisterFactory(
                "Microsoft.Data.SqlClient", typeof(SqlClientFactory));
            DbProviderFactories.RegisterFactory(
                "NpgSql", typeof(Npgsql.NpgsqlFactory));
            DbProviderFactories.RegisterFactory(
                "Microsoft.Data.Sqlite", typeof(SqliteFactory));

            sharedConnection = new SqliteConnection(SqLiteConnectionString);
            sharedConnection.Open();
        }

        [TearDown]
        public void TearDown()
        {
            sharedConnection.Close();
        }

        private const string SqlConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";
        private const string SqLiteConnectionString = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";
        private const string NpgConnectionString = "Host=localhost;Port=5432;Database=Test;Username=postgres;Password=123";


        [Test]
        public void Sample1_GetAllAvailableDrivers()
        {
            // Get drivers list as DataTable
            DataTable drivers = DbProviderFactories.GetFactoryClasses();

            foreach (DataRow driver in drivers.Rows)
            {
                Console.WriteLine("{0} | {1} | {2} | {3}",
                    driver["Name"],
                    driver["Description"],
                    driver["InvariantName"],
                    driver["AssemblyQualifiedName"]);
            }
        }

        [Test]
        public void Sample2_UseSqlClientFactoryForCreateCommonADONetObjects()
        {
            // Use invarinat name
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory("Microsoft.Data.SqlClient")!;

            IDbConnection connection = providerFactory.CreateConnection()!;
            IDbCommand command = providerFactory.CreateCommand()!;
            IDbDataAdapter dataAdapter = providerFactory.CreateDataAdapter()!;


            // Some object can create only through other
            connection.ConnectionString = SqlConnectionString;
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
        }


        const string initQuery = """
                DROP TABLE if exists StrTest;
                CREATE TABLE StrTest(s TEXT);
                
                INSERT into StrTest(s) VALUES('1234567890a');
                """;

        const string readQuery = "select * from StrTest";

        [Test]
        [TestCase("Microsoft.Data.SqlClient", SqlConnectionString)]
        [TestCase("NpgSql", NpgConnectionString)]
        [TestCase("Microsoft.Data.Sqlite", SqLiteConnectionString)]
        public void Sample3_DataSource(string provider,
            string connectionString)
        {
            var factory = DbProviderFactories.GetFactory(provider)!;
            using (var dataSource = factory.CreateDataSource(connectionString))
            {
                var initCommand = dataSource.CreateCommand(initQuery);
                initCommand.ExecuteNonQuery();

                var command = dataSource.CreateCommand(readQuery);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString("s"));
                    }
                }
            }
        }

        [Test]
        [TestCase("Microsoft.Data.SqlClient", SqlConnectionString)]
        [TestCase("NpgSql", NpgConnectionString)]
        [TestCase("Microsoft.Data.Sqlite", SqLiteConnectionString)]
        public void Sample3_DirectFacory(string provider,
            string connectionString)
        {
            var factory = DbProviderFactories.GetFactory(provider)!;

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var initCommand = factory.CreateCommand();
                initCommand.CommandText = initQuery;
                initCommand.Connection = connection;
                initCommand.ExecuteNonQuery();

                var command = factory.CreateCommand();
                command.CommandText = readQuery;
                command.Connection = connection;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString("s"));
                    }
                }

            }
        }
    }
}
