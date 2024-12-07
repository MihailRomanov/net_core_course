using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TestingAndDebuggingSamples.Model;

namespace TestingAndDebuggingSamples
{
    public static class NorthwindContextHelpers
    {
        public static NorthwindContext GetInMemoryContext()
        {
            var contextOptions = new DbContextOptionsBuilder<NorthwindContext>()
                .UseInMemoryDatabase("TestDb")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var context = new NorthwindContext(contextOptions);

            context.Database.EnsureCreated();

            return context;
        }

        public static NorthwindContext GetSqliteContext()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            var contextOptions = new DbContextOptionsBuilder<NorthwindContext>()
                .UseSqlite(connection)
                .Options;

            var context = new NorthwindContext(contextOptions);

            context.Database.EnsureCreated();

            return context;
        }
    }
}