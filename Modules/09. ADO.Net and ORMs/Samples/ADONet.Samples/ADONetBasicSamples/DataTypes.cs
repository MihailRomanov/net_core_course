using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace ADONetBasicSamples
{
    public class DataTypes
    {
        private const string SqlConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";
        private const string NpgConnectionString = "Host=localhost;Port=5432;Database=Test;Username=postgres;Password=123";

        [OneTimeSetUp]
        public void Setup()
        {
            DbProviderFactories.RegisterFactory(
                "Microsoft.Data.SqlClient", typeof(SqlClientFactory));
            DbProviderFactories.RegisterFactory(
                "NpgSql", typeof(Npgsql.NpgsqlFactory));
        }

        [Test]
        [TestCase("Microsoft.Data.SqlClient", SqlConnectionString)]
        [TestCase("NpgSql", NpgConnectionString)]
        public void Sample1_LimitStringIssues(string provider, 
            string connectionString)
        {
            var factory = DbProviderFactories.GetFactory(provider)!;
            var inString = "1234567890";
            var query = """
                select @in, CAST(@in as VARCHAR(5)), CAST(@in as VARCHAR(50));
                """;

            using (var dataSource = factory.CreateDataSource(connectionString))
            {
                var command = dataSource.CreateCommand(query);
                command.AddParamAndValue("@in", inString);

                using (var res = command.ExecuteReader())
                {
                    res.Read();

                    Console.WriteLine($"Original input '{res.GetValue(0)}'");
                    Console.WriteLine($"Varchar(5) '{res.GetValue(1)}'");
                    Console.WriteLine($"Varchar(50) '{res.GetValue(2)}'");
                }
            }
        }

        [Test]
        [TestCase("Microsoft.Data.SqlClient", SqlConnectionString)]
        [TestCase("NpgSql", NpgConnectionString)]
        public void Sample2_FixedStringIssues(string provider,
            string connectionString)
        {
            var factory = DbProviderFactories.GetFactory(provider)!;

            var inString = "1234567890";

            var query =
                "select @in, CAST(@in as CHAR(5)), CAST(@in as CHAR(50));";

            using (var dataSource = factory.CreateDataSource(connectionString))
            {
                var command = dataSource.CreateCommand(query);
                command.AddParamAndValue("@in", inString);

                using (var res = command.ExecuteReader())
                {
                    res.Read();

                    Console.WriteLine($"Original input '{res.GetValue(0)}'");
                    Console.WriteLine($"Char(5) '{res.GetValue(1)}'");
                    Console.WriteLine($"Char(50) '{res.GetValue(2)}'");
                }
            }
        }

        [Test]
        [TestCase("Microsoft.Data.SqlClient", SqlConnectionString)]
        [TestCase("NpgSql", NpgConnectionString)]
        public void Sample3_InsertToLimitString(string provider,
            string connectionString)
        {
            var factory = DbProviderFactories.GetFactory(provider)!;

            var inString = "1234567890";

            var query = """
                DROP TABLE if exists StrTest;
                CREATE TABLE StrTest(s VARCHAR(5));
                
                INSERT into StrTest(s) VALUES(@in);
                """;

            using (var dataSource = factory.CreateDataSource(connectionString))
            {
                var command = dataSource.CreateCommand(query);
                command.AddParamAndValue("@in", inString);
                command.ExecuteNonQuery();
            }
        }

        [Test]
        [TestCase("Microsoft.Data.SqlClient", SqlConnectionString, "DATETIME")]
        [TestCase("NpgSql", NpgConnectionString, "timestamp")]
        public void Sample4_DateTime(string provider,
            string connectionString, string dateTimeName)
        {
            var factory = DbProviderFactories.GetFactory(provider)!;

            var d = DateTime.Now;

            var query =
                $"select @in, CAST(@in as DATE), CAST(@in as TIME), CAST(@in as {dateTimeName})";

            using (var dataSource = factory.CreateDataSource(connectionString))
            {
                var command = dataSource.CreateCommand(query);
                command.AddParamAndValue("@in", d);

                using (var res = command.ExecuteReader())
                {
                    res.Read();

                    var v1 = res.GetValue(0);
                    var v2 = res.GetValue(1);
                    var v3 = res.GetValue(2);
                    var v4 = res.GetValue(3);

                    Console.WriteLine("GetValue");
                    Console.WriteLine($"Original {v1} -- {v1.GetType()}");
                    Console.WriteLine($"DATE {v2} -- {v2.GetType()}");
                    Console.WriteLine($"TIME {v3} -- {v3.GetType()}");
                    Console.WriteLine($"DATETIME {v4} -- {v4.GetType()}");
                }
            }
        }

        [Test]
        [TestCase("Microsoft.Data.SqlClient", SqlConnectionString)]
        [TestCase("NpgSql", NpgConnectionString)]
        public void Sample5_Decimal(string provider,
            string connectionString)
        {
            var factory = DbProviderFactories.GetFactory(provider)!;

            var query =
                "select CAST(2088310350.43000000000000000001 as DECIMAL(30,20))";

            using (var dataSource = factory.CreateDataSource(connectionString))
            {
                var command = dataSource.CreateCommand(query);

                using (var res = command.ExecuteReader())
                {
                    res.Read();

                    Console.WriteLine($"{res.GetValue(0)}"); 
                    Console.WriteLine($"{res.GetProviderSpecificValue(0)}");   
                }
            }
        }

        [Test]
        [TestCase("Microsoft.Data.SqlClient", SqlConnectionString)]
        [TestCase("NpgSql", NpgConnectionString)]
        public void Sample6_ReadNullable(string provider,
            string connectionString)
        {
            var factory = DbProviderFactories.GetFactory(provider)!;

            using (var dataSource = factory.CreateDataSource(connectionString))
            {
                var command = dataSource.CreateCommand("select NULL");

                var res1 = command.ExecuteScalar();
                Console.WriteLine($"{res1.GetType()}");

                using (var res2 = command.ExecuteReader())
                {
                    res2.Read();
                    var value = res2.GetValue(0);
                    var value2 = res2.GetProviderSpecificValue(0);
                    var isNull = res2.IsDBNull(0);
                    Console.WriteLine($"{value.GetType()} {value2.GetType()} {isNull}");
                }
            }
        }
    }
}
