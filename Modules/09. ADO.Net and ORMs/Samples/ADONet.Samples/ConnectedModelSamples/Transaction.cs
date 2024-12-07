using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace ConnectedModelSamples
{
    public class Transaction
    {
        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";
        private const string ConnectionString1 = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind1;Integrated Security=True";
        private int orderId;

        void PrintOrdersStatus(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var server = connection.DataSource;
                var db = connection.Database;

                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "select count(*) from Northwind.Orders";

                Console.WriteLine("{0}/{1} Orders = {2}", server, db, command.ExecuteScalar());
            }
        }

        [SetUp]
        public void Init()
        {
            TransactionManager.ImplicitDistributedTransactions = true;

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "select min(OrderId) from Northwind.Orders";
                orderId = (int)command.ExecuteScalar();
                Console.WriteLine("OrderId = {0}", orderId);
            }

            PrintOrdersStatus(ConnectionString);
            PrintOrdersStatus(ConnectionString1);
        }

        [TearDown]
        public void Clean()
        {
            PrintOrdersStatus(ConnectionString);
            PrintOrdersStatus(ConnectionString1);
        }

        public int DeleteOrder(IDbConnection connection, int orderId, IDbTransaction transaction)
        {
            var command1 = connection.CreateCommand();
            command1.CommandText = "delete from Northwind.Orders where OrderID = @orderId";
            command1.Transaction = transaction;

            var orderIdParam1 = command1.CreateParameter();
            orderIdParam1.ParameterName = "@orderId";
            orderIdParam1.Value = orderId;
            command1.Parameters.Add(orderIdParam1);

            var command2 = connection.CreateCommand();
            command2.CommandText = "delete from Northwind.[Order Details] where OrderID = @orderId";
            command2.Transaction = transaction;

            var orderIdParam2 = command2.CreateParameter();
            orderIdParam2.ParameterName = "@orderId";
            orderIdParam2.Value = orderId;
            command2.Parameters.Add(orderIdParam2);

            command2.ExecuteNonQuery();

            return command1.ExecuteNonQuery();
        }


        [Test]
        public void Sample1_LocalTransactionBaseScenario()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var command = connection.CreateCommand();
                    command.CommandText = "delete from Northwind.[Order Details] where OrderID = @orderId;";
                    command.CommandText += "delete from Northwind.Orders where OrderID = @orderId;";

                    var orderIdParam = command.CreateParameter();
                    orderIdParam.ParameterName = "@orderId";
                    orderIdParam.Value = orderId;
                    command.Parameters.Add(orderIdParam);

                    command.Transaction = transaction;
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
            }
        }


        [Test]
        public void Sample2_CommitedLocalTransaction()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    Console.WriteLine(DeleteOrder(connection, orderId, transaction));
                    transaction.Commit();
                }
            }

        }


        [Test]
        public void Sample3_RollbackedLocalTransaction()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    Console.WriteLine(DeleteOrder(connection, orderId, transaction));
                    transaction.Rollback();
                }
            }

        }


        [Test]
        public void Sample4_NoExplicitEndLocalTransaction()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    Console.WriteLine(DeleteOrder(connection, orderId, transaction));
                }
            }

        }

        [Test]
        public void Sample5_NoSetTransaction()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                Console.WriteLine(DeleteOrder(connection, orderId, null));
            }

        }

        [Test]
        public void Sample6_SetIsolationLevel()
        {
            using (IDbConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    Console.WriteLine(DeleteOrder(connection, orderId, transaction));
                    transaction.Commit();
                }
            }

        }

        [Test]
        public void Sample7_SystemTransactions_Commit()
        {
            using (var transactionScope = new TransactionScope())
            {
                IDbConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                Console.WriteLine(DeleteOrder(connection, orderId, null));

                IDbConnection connection1 = new SqlConnection(ConnectionString1);
                connection1.Open();
                Console.WriteLine(DeleteOrder(connection1, orderId, null));
                                
                transactionScope.Complete();
            }
        }

        [Test]
        public void Sample7_SystemTransactions_NoCommit()
        {
            using (var transactionScope = new TransactionScope())
            {
                IDbConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                Console.WriteLine(DeleteOrder(connection, orderId, null));

                IDbConnection connection1 = new SqlConnection(ConnectionString1);
                connection1.Open();
                Console.WriteLine(DeleteOrder(connection1, orderId, null));
            }
        }

        [Test]
        public void Sample7_SystemTransactions_Error()
        {
            using (var transactionScope = new TransactionScope())
            {
                IDbConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                Console.WriteLine(DeleteOrder(connection, orderId, null));

                IDbConnection connection1 = new SqlConnection(ConnectionString1);
                connection1.Open();
                Console.WriteLine(DeleteOrder(connection1, orderId, null));

                var command1 = connection1.CreateCommand();
                command1.CommandText = "Insert into Northwind.Orders (OrderId) Values (1)";
                command1.ExecuteNonQuery();


                transactionScope.Complete();
            }
        }
    }
}
